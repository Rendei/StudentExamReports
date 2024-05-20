using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UTMNStudentsExamAnalysis.Migrations.StudentExamResults
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "answers",
                columns: table => new
                {
                    answer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    result_id = table.Column<int>(type: "integer", nullable: false),
                    part_number = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    number_in_part = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    points = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("answers_pkey", x => x.answer_id);
                });

            migrationBuilder.CreateTable(
                name: "areas",
                columns: table => new
                {
                    area_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    area_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("areas_pkey", x => x.area_id);
                });

            migrationBuilder.CreateTable(
                name: "competentions",
                columns: table => new
                {
                    competention_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    competition_text = table.Column<string>(type: "character varying(511)", maxLength: 511, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("competentions_pkey", x => x.competention_id);
                });

            migrationBuilder.CreateTable(
                name: "reports_data",
                columns: table => new
                {
                    reports_data_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    school_ids = table.Column<int[]>(type: "integer[]", nullable: true),
                    class_ids = table.Column<int[]>(type: "integer[]", nullable: true),
                    type_ids = table.Column<int[]>(type: "integer[]", nullable: true),
                    subject_ids = table.Column<int[]>(type: "integer[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("reports_data_pkey", x => x.reports_data_id);
                });

            migrationBuilder.CreateTable(
                name: "school_kinds",
                columns: table => new
                {
                    school_kind_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    school_kind_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("school_kinds_pkey", x => x.school_kind_id);
                });

            migrationBuilder.CreateTable(
                name: "student_categories",
                columns: table => new
                {
                    student_category_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    category_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("student_categories_pkey", x => x.student_category_id);
                });

            migrationBuilder.CreateTable(
                name: "subjects",
                columns: table => new
                {
                    subject_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    subject_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("subjects_pkey", x => x.subject_id);
                });

            migrationBuilder.CreateTable(
                name: "task_competentions",
                columns: table => new
                {
                    task_id = table.Column<int>(type: "integer", nullable: false),
                    competention_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("task_competentions_pkey", x => new { x.task_id, x.competention_id });
                });

            migrationBuilder.CreateTable(
                name: "test_type",
                columns: table => new
                {
                    test_type_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    test_type_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("test_type_pkey", x => x.test_type_id);
                });

            migrationBuilder.CreateTable(
                name: "town_types",
                columns: table => new
                {
                    town_type_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    town_type_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("town_types_pkey", x => x.town_type_id);
                });

            migrationBuilder.CreateTable(
                name: "test_templates",
                columns: table => new
                {
                    test_template_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    year = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    test_type_id = table.Column<int>(type: "integer", nullable: false),
                    subject_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("test_templates_pkey", x => x.test_template_id);
                    table.ForeignKey(
                        name: "test_templates_subject_id_fkey",
                        column: x => x.subject_id,
                        principalTable: "subjects",
                        principalColumn: "subject_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "test_templates_test_type_id_fkey",
                        column: x => x.test_type_id,
                        principalTable: "test_type",
                        principalColumn: "test_type_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "schools",
                columns: table => new
                {
                    school_code = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    law_address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    short_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    town_type_id = table.Column<int>(type: "integer", nullable: true),
                    area_id = table.Column<int>(type: "integer", nullable: true),
                    school_kind_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("schools_pkey", x => x.school_code);
                    table.ForeignKey(
                        name: "schools_area_id_fkey",
                        column: x => x.area_id,
                        principalTable: "areas",
                        principalColumn: "area_id");
                    table.ForeignKey(
                        name: "schools_school_kind_id_fkey",
                        column: x => x.school_kind_id,
                        principalTable: "school_kinds",
                        principalColumn: "school_kind_id");
                    table.ForeignKey(
                        name: "schools_town_type_id_fkey",
                        column: x => x.town_type_id,
                        principalTable: "town_types",
                        principalColumn: "town_type_id");
                });

            migrationBuilder.CreateTable(
                name: "tasks",
                columns: table => new
                {
                    task_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    task_number = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    criteria = table.Column<string>(type: "character varying(511)", maxLength: 511, nullable: true),
                    difficulty = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    total_points = table.Column<int>(type: "integer", nullable: false),
                    part_number = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    number_in_part = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    competition_id = table.Column<int>(type: "integer", nullable: false),
                    test_template_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tasks_pkey", x => x.task_id);
                    table.ForeignKey(
                        name: "tasks_competition_id_fkey",
                        column: x => x.competition_id,
                        principalTable: "competentions",
                        principalColumn: "competention_id");
                    table.ForeignKey(
                        name: "tasks_test_template_id_fkey",
                        column: x => x.test_template_id,
                        principalTable: "test_templates",
                        principalColumn: "test_template_id");
                });

            migrationBuilder.CreateTable(
                name: "reports",
                columns: table => new
                {
                    report_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateOnly>(type: "date", nullable: true, defaultValueSql: "now()"),
                    user_id = table.Column<int>(type: "integer", nullable: true),
                    school_code = table.Column<int>(type: "integer", nullable: true),
                    area_id = table.Column<int>(type: "integer", nullable: true),
                    report_data_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("reports_pkey", x => x.report_id);
                    table.ForeignKey(
                        name: "reports_area_code_fkey",
                        column: x => x.area_id,
                        principalTable: "areas",
                        principalColumn: "area_id");
                    table.ForeignKey(
                        name: "reports_report_data_id_fkey",
                        column: x => x.report_data_id,
                        principalTable: "reports_data",
                        principalColumn: "reports_data_id");
                    table.ForeignKey(
                        name: "reports_school_code_fkey",
                        column: x => x.school_code,
                        principalTable: "schools",
                        principalColumn: "school_code");
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    student_id = table.Column<Guid>(type: "uuid", nullable: false),
                    @class = table.Column<string>(name: "class", type: "character varying", nullable: false),
                    sex = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    school_code = table.Column<int>(type: "integer", nullable: false),
                    student_category = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("students_pkey", x => x.student_id);
                    table.ForeignKey(
                        name: "students_school_code_fkey",
                        column: x => x.school_code,
                        principalTable: "schools",
                        principalColumn: "school_code");
                    table.ForeignKey(
                        name: "students_student_category_fkey",
                        column: x => x.student_category,
                        principalTable: "student_categories",
                        principalColumn: "student_category_id");
                });

            migrationBuilder.CreateTable(
                name: "results",
                columns: table => new
                {
                    result_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    primary_points = table.Column<int>(type: "integer", nullable: false),
                    first_part_primary_points = table.Column<int>(type: "integer", nullable: false),
                    second_part_primary_points = table.Column<int>(type: "integer", nullable: false),
                    third_part_primary_points = table.Column<int>(type: "integer", nullable: false),
                    mark = table.Column<int>(type: "integer", nullable: true),
                    completion_percent = table.Column<int>(type: "integer", nullable: false),
                    secondary_points = table.Column<int>(type: "integer", nullable: false),
                    student_id = table.Column<Guid>(type: "uuid", nullable: false),
                    test_template_id = table.Column<int>(type: "integer", nullable: true),
                    first_part_answers = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    second_part_answers = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    third_part_answers = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("results_pkey", x => x.result_id);
                    table.ForeignKey(
                        name: "results_student_id_fkey",
                        column: x => x.student_id,
                        principalTable: "students",
                        principalColumn: "student_id");
                    table.ForeignKey(
                        name: "results_test_template_id_fkey",
                        column: x => x.test_template_id,
                        principalTable: "test_templates",
                        principalColumn: "test_template_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_reports_area_id",
                table: "reports",
                column: "area_id");

            migrationBuilder.CreateIndex(
                name: "IX_reports_report_data_id",
                table: "reports",
                column: "report_data_id");

            migrationBuilder.CreateIndex(
                name: "IX_reports_school_code",
                table: "reports",
                column: "school_code");

            migrationBuilder.CreateIndex(
                name: "IX_results_student_id",
                table: "results",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_results_test_template_id",
                table: "results",
                column: "test_template_id");

            migrationBuilder.CreateIndex(
                name: "IX_schools_area_id",
                table: "schools",
                column: "area_id");

            migrationBuilder.CreateIndex(
                name: "IX_schools_school_kind_id",
                table: "schools",
                column: "school_kind_id");

            migrationBuilder.CreateIndex(
                name: "IX_schools_town_type_id",
                table: "schools",
                column: "town_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_students_school_code",
                table: "students",
                column: "school_code");

            migrationBuilder.CreateIndex(
                name: "IX_students_student_category",
                table: "students",
                column: "student_category");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_competition_id",
                table: "tasks",
                column: "competition_id");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_test_template_id",
                table: "tasks",
                column: "test_template_id");

            migrationBuilder.CreateIndex(
                name: "IX_test_templates_subject_id",
                table: "test_templates",
                column: "subject_id");

            migrationBuilder.CreateIndex(
                name: "IX_test_templates_test_type_id",
                table: "test_templates",
                column: "test_type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "answers");

            migrationBuilder.DropTable(
                name: "reports");

            migrationBuilder.DropTable(
                name: "results");

            migrationBuilder.DropTable(
                name: "task_competentions");

            migrationBuilder.DropTable(
                name: "tasks");

            migrationBuilder.DropTable(
                name: "reports_data");

            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "competentions");

            migrationBuilder.DropTable(
                name: "test_templates");

            migrationBuilder.DropTable(
                name: "schools");

            migrationBuilder.DropTable(
                name: "student_categories");

            migrationBuilder.DropTable(
                name: "subjects");

            migrationBuilder.DropTable(
                name: "test_type");

            migrationBuilder.DropTable(
                name: "areas");

            migrationBuilder.DropTable(
                name: "school_kinds");

            migrationBuilder.DropTable(
                name: "town_types");
        }
    }
}
