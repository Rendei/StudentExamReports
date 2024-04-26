using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UTMNStudentsExamAnalysis.Models;

namespace UTMNStudentsExamAnalysis;

public partial class StudentExamResultsContext : IdentityDbContext<ApplicationUser>
{
    private readonly IConfiguration _configuration;
    public StudentExamResultsContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public StudentExamResultsContext(DbContextOptions<StudentExamResultsContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<Competention> Competentions { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<ReportsData> ReportsData { get; set; }

    public virtual DbSet<Result> Results { get; set; }

    public virtual DbSet<School> Schools { get; set; }

    public virtual DbSet<SchoolKind> SchoolKinds { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentCategory> StudentCategories { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<TaskCompetention> TaskCompetentions { get; set; }

    public virtual DbSet<TestTemplate> TestTemplates { get; set; }

    public virtual DbSet<TestType> TestTypes { get; set; }

    public virtual DbSet<TownType> TownTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql(_configuration.GetConnectionString("ExamDatabase"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("answers_pkey");

            entity.ToTable("answers");

            entity.Property(e => e.AnswerId)
                .ValueGeneratedNever()
                .HasColumnName("answer_id");
            entity.Property(e => e.NumberInPart)
                .HasMaxLength(15)
                .HasColumnName("number_in_part");
            entity.Property(e => e.PartNumber)
                .HasMaxLength(15)
                .HasColumnName("part_number");
            entity.Property(e => e.Points).HasColumnName("points");
            entity.Property(e => e.ResultId).HasColumnName("result_id");
        });

        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasKey(e => e.AreaId).HasName("areas_pkey");

            entity.ToTable("areas");

            entity.Property(e => e.AreaId)
                .HasDefaultValueSql("nextval('areas_area_code_seq'::regclass)")
                .HasColumnName("area_id");
            entity.Property(e => e.AreaName)
                .HasMaxLength(255)
                .HasColumnName("area_name");
        });

        modelBuilder.Entity<Competention>(entity =>
        {
            entity.HasKey(e => e.CompetentionId).HasName("competentions_pkey");

            entity.ToTable("competentions");

            entity.Property(e => e.CompetentionId).HasColumnName("competention_id");
            entity.Property(e => e.CompetitionText)
                .HasMaxLength(511)
                .HasColumnName("competition_text");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("reports_pkey");

            entity.ToTable("reports");

            entity.Property(e => e.ReportId).HasColumnName("report_id");
            entity.Property(e => e.AreaId).HasColumnName("area_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.ReportDataId).HasColumnName("report_data_id");
            entity.Property(e => e.SchoolCode).HasColumnName("school_code");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Area).WithMany(p => p.Reports)
                .HasForeignKey(d => d.AreaId)
                .HasConstraintName("reports_area_code_fkey");

            entity.HasOne(d => d.ReportData).WithMany(p => p.Reports)
                .HasForeignKey(d => d.ReportDataId)
                .HasConstraintName("reports_report_data_id_fkey");

            entity.HasOne(d => d.SchoolCodeNavigation).WithMany(p => p.Reports)
                .HasForeignKey(d => d.SchoolCode)
                .HasConstraintName("reports_school_code_fkey");
        });

        modelBuilder.Entity<ReportsData>(entity =>
        {
            entity.HasKey(e => e.ReportsDataId).HasName("reports_data_pkey");

            entity.ToTable("reports_data");

            entity.Property(e => e.ReportsDataId).HasColumnName("reports_data_id");
            entity.Property(e => e.ClassIds).HasColumnName("class_ids");
            entity.Property(e => e.SchoolIds).HasColumnName("school_ids");
            entity.Property(e => e.SubjectIds).HasColumnName("subject_ids");
            entity.Property(e => e.TypeIds).HasColumnName("type_ids");
        });

        modelBuilder.Entity<Result>(entity =>
        {
            entity.HasKey(e => e.ResultId).HasName("results_pkey");

            entity.ToTable("results");

            entity.Property(e => e.ResultId).HasColumnName("result_id");
            entity.Property(e => e.CompletionPercent).HasColumnName("completion_percent");
            entity.Property(e => e.FirstPartAnswers)
                .HasMaxLength(255)
                .HasColumnName("first_part_answers");
            entity.Property(e => e.FirstPartPrimaryPoints).HasColumnName("first_part_primary_points");
            entity.Property(e => e.Mark).HasColumnName("mark");
            entity.Property(e => e.PrimaryPoints).HasColumnName("primary_points");
            entity.Property(e => e.SecondPartAnswers)
                .HasMaxLength(255)
                .HasColumnName("second_part_answers");
            entity.Property(e => e.SecondPartPrimaryPoints).HasColumnName("second_part_primary_points");
            entity.Property(e => e.SecondaryPoints).HasColumnName("secondary_points");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.TestTemplateId).HasColumnName("test_template_id");
            entity.Property(e => e.ThirdPartAnswers)
                .HasMaxLength(255)
                .HasColumnName("third_part_answers");
            entity.Property(e => e.ThirdPartPrimaryPoints).HasColumnName("third_part_primary_points");

            entity.HasOne(d => d.Student).WithMany(p => p.Results)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("results_student_id_fkey");

            entity.HasOne(d => d.TestTemplate).WithMany(p => p.Results)
                .HasForeignKey(d => d.TestTemplateId)
                .HasConstraintName("results_test_template_id_fkey");
        });

        modelBuilder.Entity<School>(entity =>
        {
            entity.HasKey(e => e.SchoolCode).HasName("schools_pkey");

            entity.ToTable("schools");

            entity.Property(e => e.SchoolCode).HasColumnName("school_code");
            entity.Property(e => e.AreaId).HasColumnName("area_id");
            entity.Property(e => e.LawAddress)
                .HasMaxLength(255)
                .HasColumnName("law_address");
            entity.Property(e => e.SchoolKindId).HasColumnName("school_kind_id");
            entity.Property(e => e.ShortName)
                .HasMaxLength(255)
                .HasColumnName("short_name");
            entity.Property(e => e.TownTypeId).HasColumnName("town_type_id");

            entity.HasOne(d => d.Area).WithMany(p => p.Schools)
                .HasForeignKey(d => d.AreaId)
                .HasConstraintName("schools_area_id_fkey");

            entity.HasOne(d => d.SchoolKind).WithMany(p => p.Schools)
                .HasForeignKey(d => d.SchoolKindId)
                .HasConstraintName("schools_school_kind_id_fkey");

            entity.HasOne(d => d.TownType).WithMany(p => p.Schools)
                .HasForeignKey(d => d.TownTypeId)
                .HasConstraintName("schools_town_type_id_fkey");
        });

        modelBuilder.Entity<SchoolKind>(entity =>
        {
            entity.HasKey(e => e.SchoolKindId).HasName("school_kinds_pkey");

            entity.ToTable("school_kinds");

            entity.Property(e => e.SchoolKindId)
                .HasDefaultValueSql("nextval('school_kinds_school_kind_code_seq'::regclass)")
                .HasColumnName("school_kind_id");
            entity.Property(e => e.SchoolKindName)
                .HasMaxLength(255)
                .HasColumnName("school_kind_name");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("students_pkey");

            entity.ToTable("students");

            entity.Property(e => e.StudentId)
                .ValueGeneratedNever()
                .HasColumnName("student_id");
            entity.Property(e => e.Class)
                .HasColumnType("character varying")
                .HasColumnName("class");
            entity.Property(e => e.SchoolCode).HasColumnName("school_code");
            entity.Property(e => e.Sex)
                .HasMaxLength(255)
                .HasColumnName("sex");
            entity.Property(e => e.StudentCategory).HasColumnName("student_category");

            entity.HasOne(d => d.SchoolCodeNavigation).WithMany(p => p.Students)
                .HasForeignKey(d => d.SchoolCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("students_school_code_fkey");

            entity.HasOne(d => d.StudentCategoryNavigation).WithMany(p => p.Students)
                .HasForeignKey(d => d.StudentCategory)
                .HasConstraintName("students_student_category_fkey");
        });

        modelBuilder.Entity<StudentCategory>(entity =>
        {
            entity.HasKey(e => e.StudentCategoryId).HasName("student_categories_pkey");

            entity.ToTable("student_categories");

            entity.Property(e => e.StudentCategoryId)
                .HasDefaultValueSql("nextval('student_categories_student_categories_code_seq'::regclass)")
                .HasColumnName("student_category_id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("category_name");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("subjects_pkey");

            entity.ToTable("subjects");

            entity.Property(e => e.SubjectId).HasColumnName("subject_id");
            entity.Property(e => e.SubjectName)
                .HasMaxLength(255)
                .HasColumnName("subject_name");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("tasks_pkey");

            entity.ToTable("tasks");

            entity.Property(e => e.TaskId).HasColumnName("task_id");
            entity.Property(e => e.CompetitionId).HasColumnName("competition_id");
            entity.Property(e => e.Criteria)
                .HasMaxLength(511)
                .HasColumnName("criteria");
            entity.Property(e => e.Difficulty)
                .HasMaxLength(15)
                .HasColumnName("difficulty");
            entity.Property(e => e.NumberInPart)
                .HasMaxLength(15)
                .HasColumnName("number_in_part");
            entity.Property(e => e.PartNumber)
                .HasMaxLength(15)
                .HasColumnName("part_number");
            entity.Property(e => e.TaskNumber)
                .HasMaxLength(15)
                .HasColumnName("task_number");
            entity.Property(e => e.TestTemplateId).HasColumnName("test_template_id");
            entity.Property(e => e.TotalPoints).HasColumnName("total_points");

            entity.HasOne(d => d.Competition).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.CompetitionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tasks_competition_id_fkey");

            entity.HasOne(d => d.TestTemplate).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.TestTemplateId)
                .HasConstraintName("tasks_test_template_id_fkey");
        });

        modelBuilder.Entity<TaskCompetention>(entity =>
        {
            entity.HasKey(e => new { e.TaskId, e.CompetentionId }).HasName("task_competentions_pkey");

            entity.ToTable("task_competentions");

            entity.Property(e => e.TaskId).HasColumnName("task_id");
            entity.Property(e => e.CompetentionId).HasColumnName("competention_id");
        });

        modelBuilder.Entity<TestTemplate>(entity =>
        {
            entity.HasKey(e => e.TestTemplateId).HasName("test_templates_pkey");

            entity.ToTable("test_templates");

            entity.Property(e => e.TestTemplateId).HasColumnName("test_template_id");
            entity.Property(e => e.SubjectId).HasColumnName("subject_id");
            entity.Property(e => e.TestTypeId).HasColumnName("test_type_id");
            entity.Property(e => e.Year)
                .HasMaxLength(10)
                .HasColumnName("year");

            entity.HasOne(d => d.Subject).WithMany(p => p.TestTemplates)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("test_templates_subject_id_fkey");

            entity.HasOne(d => d.TestType).WithMany(p => p.TestTemplates)
                .HasForeignKey(d => d.TestTypeId)
                .HasConstraintName("test_templates_test_type_id_fkey");
        });

        modelBuilder.Entity<TestType>(entity =>
        {
            entity.HasKey(e => e.TestTypeId).HasName("test_type_pkey");

            entity.ToTable("test_type");

            entity.Property(e => e.TestTypeId).HasColumnName("test_type_id");
            entity.Property(e => e.TestTypeName)
                .HasMaxLength(255)
                .HasColumnName("test_type_name");
        });

        modelBuilder.Entity<TownType>(entity =>
        {
            entity.HasKey(e => e.TownTypeId).HasName("town_types_pkey");

            entity.ToTable("town_types");

            entity.Property(e => e.TownTypeId)
                .HasDefaultValueSql("nextval('town_types_town_type_code_seq'::regclass)")
                .HasColumnName("town_type_id");
            entity.Property(e => e.TownTypeName)
                .HasMaxLength(255)
                .HasColumnName("town_type_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
