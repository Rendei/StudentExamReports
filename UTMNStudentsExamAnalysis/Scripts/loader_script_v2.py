#Теперь скрипт принимает два аргумента - путь к файлу excel и учетные данные postgres
#также добавлена функция main() (просто для красоты)

import pandas as pd
from sqlalchemy.exc import IntegrityError
from sqlalchemy import create_engine, text
import time
import sys
import io


sys.stdout = io.TextIOWrapper(sys.stdout.buffer, encoding='utf-8')

#COMPLETION_PERCENT = "complition_percent" #для старого дампа БД
COMPLETION_PERCENT = "completion_percent"


def first_db_insert(credentials='postgresql://username:password@localhost:5432/mydatabase'):
    engine = create_engine(credentials)
    subject_codes = {
        "subject_id": [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 18, 22, 25],
        "subject_name": ["Русский язык", "Математика профильная", "Физика", "Химия", "Информатика и ИКТ",
                         "Биология", "История", "География", "Английский язык", "Немецкий язык",
                         "Французский язык", "Обществознание", "Испанский язык", "Китайский язык",
                         "Литература", "Математика (базовый)", "Информатика и ИКТ (КЕГЭ)"]
    }
    subjects_df = pd.DataFrame.from_dict(subject_codes)

    subjects_upsert_query = '''
        INSERT INTO subjects (subject_id, subject_name)
        VALUES (:subject_id, :subject_name)
        ON CONFLICT (subject_id) DO UPDATE
        SET subject_name = EXCLUDED.subject_name;
        '''
    test_types = {
        "test_type_id": [1, 2, 3],
        "test_type_name": ["ЕГЭ", "ОГЭ", "ВПР"]
    }

    test_types_df = pd.DataFrame.from_dict(test_types)

    test_type_upsert_query = '''
        INSERT INTO test_type (test_type_id, test_type_name)
        VALUES (:test_type_id, :test_type_name)
        ON CONFLICT (test_type_id) DO UPDATE
        SET test_type_name = EXCLUDED.test_type_name;
        '''

    with engine.connect() as conn:
        conn.execute(text(subjects_upsert_query), subjects_df.to_dict(orient="records"))
        conn.execute(text(test_type_upsert_query), test_types_df.to_dict(orient="records"))
        conn.commit()


def get_test_template_id(year: str, test_type_id: int, subject_id,
                         engine) -> int:
    test_template_where_query = '''
        SELECT test_template_id
        FROM test_templates
        WHERE (test_templates.year = :year 
        AND test_templates.test_type_id = :test_type_id
        AND test_templates.subject_id = :subject_id)
    '''

    with engine.connect() as conn:
        result = conn.execute(text(test_template_where_query), {'year': year,
                                                                'test_type_id': test_type_id,
                                                                'subject_id': subject_id})
    return result.fetchone()[0]


def get_test_templates_values(fname: str):
    fname = fname.replace("/", '\\')
    data_str = fname.split('\\')[-1]
    data = data_str.split('_')
    year = data[0]
    test_type = data[1]
    match test_type:
        case "ЕГЭ":
            test_type = 1
        case "ОГЭ":
            test_type = 2
        case "ВПР":
            test_type = 3
    return year, test_type


def insert_results(results: pd.DataFrame, year: str, test_type: int, engine):
    results['test_template_id'] = results.apply(lambda row: get_test_template_id(year, test_type, row['Предмет'], engine), axis=1)

    #костыль для старого дампа
    #results.drop(['first_part_answers', 'second_part_answers', 'third_part_answers'], axis=1, inplace=True)

    #TODO проверить, есть ли такая запись в БД (если есть, то её не надо вставлять), за признак равенства взять test_template_id и student_id
    # ~Done (очень долго вставляет, но вставляет)
    results_upsert_query = '''
           INSERT INTO results (student_id, test_template_id, primary_points, completion_percent, secondary_points, first_part_primary_points,
                                 first_part_answers, second_part_primary_points, second_part_answers, third_part_primary_points,
                                 third_part_answers)
           VALUES (:student_id, :test_template_id, :primary_points, :completion_percent, :secondary_points, :first_part_primary_points,
                   :first_part_answers, :second_part_primary_points, :second_part_answers, :third_part_primary_points,
                   :third_part_answers)
       '''

    #для старого дампа
    # results_upsert_query_OLD = '''
    #         ALTER TABLE results DROP CONSTRAINT IF EXISTS result_uniq;
    #         ALTER TABLE results ADD CONSTRAINT result_uniq UNIQUE (student_id, test_template_id);
    #         INSERT INTO results (student_id, test_template_id, primary_points, complition_percent, secondary_points, first_part_primary_points,
    #             second_part_primary_points, third_part_primary_points)
    #         VALUES (:student_id, :test_template_id, :primary_points, :complition_percent, :secondary_points, :first_part_primary_points,
    #                 :second_part_primary_points, :third_part_primary_points)
    #         ON CONFLICT ON CONSTRAINT result_uniq DO NOTHING;
    #     '''

    with engine.connect() as conn:
        conn.execute(text(results_upsert_query), results.to_dict(orient='records'))
        print("Успешно выполнена вставка результатов")
        conn.commit()


def excel_to_dataframe(fname, credentials='postgresql://username:password@localhost:5432/mydatabase'):
    try:
        engine = create_engine(credentials)

        sheet0_df = pd.read_excel(fname, sheet_name=0)
        sheet1_df = pd.read_excel(fname, sheet_name=1)

        town_types_df = sheet1_df.loc[:, 'TownTypeCode': 'TownTypeName'].drop_duplicates()
        town_types_df.rename(columns={'TownTypeCode': 'town_type_id', 'TownTypeName': 'town_type_name'}, inplace=True)

        school_kinds_df = sheet1_df.loc[:, 'SchoolKindCode': 'SchoolKindName'].drop_duplicates()
        school_kinds_df.rename(columns={'SchoolKindCode': "school_kind_id", 'SchoolKindName': 'school_kind_name'},
                               inplace=True)

        areas_df = sheet1_df.loc[:, 'AreaCode': 'AreaName'].drop_duplicates()
        areas_df.rename(columns={'AreaCode': 'area_id', 'AreaName': 'area_name'}, inplace=True)

        schools_df = sheet1_df.loc[:,
                     ['SchoolCode', 'LawAddress', 'ShortName', 'SchoolKindCode', 'TownTypeCode', 'AreaCode']]
        schools_df.rename(columns={'SchoolCode': 'school_code', 'LawAddress': 'law_address', 'ShortName': 'short_name',
                                   'SchoolKindCode': 'school_kind_id', 'TownTypeCode': 'town_type_id',
                                   'AreaCode': 'area_id'}, inplace=True)

        students_df = sheet0_df.loc[:, 'ID': 'Класс']
        students_df['ID'].drop_duplicates()
        students_df.rename(columns={'ID': 'student_id', 'Код школы': 'school_code', 'Класс': 'class'}, inplace=True)

        test_templates_df = sheet0_df.loc[:, ['Предмет']].drop_duplicates()
        year, test_type = get_test_templates_values(fname)
        test_templates_df['year'] = year
        test_templates_df['test_type_id'] = test_type
        test_templates_df.rename(columns={'Предмет': 'subject_id'}, inplace=True)

        results_df = sheet0_df.loc[:, ['ID', 'Первичный балл', 'Процент выполнения',
                                       '100 балльная шкала', 'Первичный балл за часть с кратким ответом',
                                       'Оценка кратких ответов', 'Первичный балл за часть с развернутым ответом',
                                       'Оценка развернутых ответов', 'Первичный балл за усную часть',
                                       'Оценка устных ответов', 'Предмет']]
        results_df.rename(columns={'ID': 'student_id', 'Первичный балл': 'primary_points',
                                   'Процент выполнения': COMPLETION_PERCENT,
                                   '100 балльная шкала': 'secondary_points',
                                   'Первичный балл за часть с кратким ответом': 'first_part_primary_points',
                                   'Оценка кратких ответов': 'first_part_answers',
                                   'Первичный балл за часть с развернутым ответом': 'second_part_primary_points',
                                   'Оценка развернутых ответов': 'second_part_answers',
                                   'Первичный балл за усную часть': 'third_part_primary_points',
                                   'Оценка устных ответов': 'third_part_answers'}, inplace=True)

        town_types_upsert_query = '''
            INSERT INTO town_types (town_type_id, town_type_name)
            VALUES (:town_type_id, :town_type_name)
            ON CONFLICT (town_type_id) DO UPDATE
            SET town_type_name = EXCLUDED.town_type_name;
        '''

        school_kinds_upsert_query = '''
            INSERT INTO school_kinds (school_kind_id, school_kind_name)
            VALUES (:school_kind_id, :school_kind_name)
            ON CONFLICT (school_kind_id) DO UPDATE
            SET school_kind_name = EXCLUDED.school_kind_name;
        '''

        areas_upsert_query = '''
            INSERT INTO areas (area_id, area_name)
            VALUES (:area_id, :area_name)
            ON CONFLICT (area_id) DO UPDATE
            SET area_name = EXCLUDED.area_name;
        '''

        schools_upsert_query = '''
            INSERT INTO schools (school_code, law_address, short_name, school_kind_id, town_type_id, area_id)
            VALUES (:school_code, :law_address, :short_name, :school_kind_id, :town_type_id, :area_id)
            ON CONFLICT (school_code) DO UPDATE
            SET law_address = EXCLUDED.law_address,
                short_name = EXCLUDED.short_name,
                school_kind_id = EXCLUDED.school_kind_id,
                town_type_id = EXCLUDED.town_type_id,
                area_id = EXCLUDED.area_id;
        '''

        students_upsert_query = '''
            INSERT INTO students (student_id, school_code, class)
            VALUES (:student_id, :school_code, :class)
            ON CONFLICT (student_id) DO NOTHING;
        '''

        test_templates_upsert_query = '''
            ALTER TABLE test_templates DROP CONSTRAINT IF EXISTS test_unique;
            ALTER TABLE test_templates ADD CONSTRAINT test_unique UNIQUE (year, test_type_id, subject_id);
            INSERT INTO test_templates (year, test_type_id, subject_id)
            VALUES (:year, :test_type_id, :subject_id)
        '''

        with engine.connect() as conn:
            conn.execute(text(town_types_upsert_query), town_types_df.to_dict(orient='records'))
            #print("Успешно выполнена вставка типов городов")
            conn.execute(text(school_kinds_upsert_query), school_kinds_df.to_dict(orient='records'))
            #print("Успешно выполнена вставка типов школ")
            conn.execute(text(areas_upsert_query), areas_df.to_dict(orient='records'))
            #print("Успешно выполнена вставка типов местности")
            conn.execute(text(schools_upsert_query), schools_df.to_dict(orient='records'))
            #print("Успешно выполнена вставка школ")
            conn.execute(text(students_upsert_query), students_df.to_dict(orient='records'))
            #print("Успешно выполнена вставка обучающихся")
            conn.execute(text(test_templates_upsert_query), test_templates_df.to_dict(orient='records'))
            #print("Успешно выполнена вставка шаблонов тестов")
            conn.commit()

        insert_results(results_df, year, test_type, engine)

    except IntegrityError as ex:
        print("Данные из этой таблицы уже существуют в БД, все данные, кроме результатов были обновлены в "
              "соответствии с этой таблицой")
        return -1
    except Exception as ex:
        print(f"Произошла ошибка {ex}")
        return -2
    else:
       print(f"excel_to_dataframe({fname}): success")


def main():
    MY_EXCEL_FILE = ""
    if len(sys.argv) > 1:
        MY_EXCEL_FILE = sys.argv[1]
    
    start_time = time.time()
    CREDENTIALS = ""
    if len(sys.argv) > 2:
        CREDENTIALS = sys.argv[2]
    #print("Файл:", MY_EXCEL_FILE, "База:", CREDENTIALS)
    #first_db_insert(credentials=CREDENTIALS)
    return excel_to_dataframe(MY_EXCEL_FILE, credentials=CREDENTIALS)
    #print(f"Время выполнения скрипта вставки всех данных {time.time() - start_time}")


if __name__ == "__main__":
    print(main())

