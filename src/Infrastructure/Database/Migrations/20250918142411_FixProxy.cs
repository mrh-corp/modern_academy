using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class FixProxy : Migration
{
    private static readonly string[] columns = new[] { "academy_id", "label" };
    private static readonly string[] columnsArray = new[] { "academy_id", "name" };
    private static readonly string[] columnsArray0 = new[] { "student_id", "academy_id", "current_school_year_id" };

    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_courses_class_courses_class_course_id",
            schema: "public",
            table: "courses");

        migrationBuilder.DropTable(
            name: "todo_item",
            schema: "public");

        migrationBuilder.DropIndex(
            name: "ix_courses_class_course_id",
            schema: "public",
            table: "courses");

        migrationBuilder.DropIndex(
            name: "ix_classes_academy_id",
            schema: "public",
            table: "classes");

        migrationBuilder.DropColumn(
            name: "class_course_id",
            schema: "public",
            table: "courses");

        migrationBuilder.AddColumn<Guid>(
            name: "course_id1",
            schema: "public",
            table: "course_credits",
            type: "uuid",
            nullable: true);

        migrationBuilder.AddColumn<Guid>(
            name: "school_year_id",
            schema: "public",
            table: "course_credits",
            type: "uuid",
            nullable: false,
            defaultValue: Guid.Empty);

        migrationBuilder.CreateTable(
            name: "class_course_course",
            schema: "public",
            columns: table => new
            {
                class_courses_id = table.Column<Guid>(type: "uuid", nullable: false),
                courses_id = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_class_course_course", x => new { x.class_courses_id, x.courses_id });
                table.ForeignKey(
                    name: "fk_class_course_course_class_courses_class_courses_id",
                    column: x => x.class_courses_id,
                    principalSchema: "public",
                    principalTable: "class_courses",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_class_course_course_courses_courses_id",
                    column: x => x.courses_id,
                    principalSchema: "public",
                    principalTable: "courses",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "students",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                full_name = table.Column<string>(type: "text", nullable: false),
                birth_date = table.Column<DateOnly>(type: "date", nullable: false),
                birth_place = table.Column<string>(type: "text", nullable: false),
                email = table.Column<string>(type: "text", nullable: true),
                gender = table.Column<char>(type: "character(1)", nullable: false),
                contact = table.Column<string>(type: "text", nullable: true),
                current_address = table.Column<string>(type: "text", nullable: false),
                father_name = table.Column<string>(type: "text", nullable: false),
                mother_name = table.Column<string>(type: "text", nullable: false),
                father_job = table.Column<string>(type: "text", nullable: false),
                mother_job = table.Column<string>(type: "text", nullable: false),
                father_contact = table.Column<string>(type: "text", nullable: false),
                mother_contact = table.Column<string>(type: "text", nullable: false),
                tutor_name = table.Column<string>(type: "text", nullable: true),
                tutor_contact = table.Column<string>(type: "text", nullable: true),
                custom_fields = table.Column<string>(type: "text", nullable: true),
                created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                update_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table => table.PrimaryKey("pk_students", x => x.id));

        migrationBuilder.CreateTable(
            name: "registration",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                student_id = table.Column<Guid>(type: "uuid", nullable: false),
                academy_id = table.Column<Guid>(type: "uuid", nullable: false),
                current_school_year_id = table.Column<Guid>(type: "uuid", nullable: false),
                current_class_id = table.Column<Guid>(type: "uuid", nullable: false),
                created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                update_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_registration", x => x.id);
                table.ForeignKey(
                    name: "fk_registration_academies_academy_id",
                    column: x => x.academy_id,
                    principalSchema: "public",
                    principalTable: "academies",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_registration_classes_current_class_id",
                    column: x => x.current_class_id,
                    principalSchema: "public",
                    principalTable: "classes",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_registration_school_years_current_school_year_id",
                    column: x => x.current_school_year_id,
                    principalSchema: "public",
                    principalTable: "school_years",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_registration_students_student_id",
                    column: x => x.student_id,
                    principalSchema: "public",
                    principalTable: "students",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "ix_course_credits_course_id1",
            schema: "public",
            table: "course_credits",
            column: "course_id1",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_course_credits_school_year_id",
            schema: "public",
            table: "course_credits",
            column: "school_year_id");

        migrationBuilder.CreateIndex(
            name: "ix_classes_academy_id_label",
            schema: "public",
            table: "classes",
            columns: columns,
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_classes_academy_id_name",
            schema: "public",
            table: "classes",
            columns: columnsArray,
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_class_courses_academy_id",
            schema: "public",
            table: "class_courses",
            column: "academy_id");

        migrationBuilder.CreateIndex(
            name: "ix_class_courses_school_year_id",
            schema: "public",
            table: "class_courses",
            column: "school_year_id");

        migrationBuilder.CreateIndex(
            name: "ix_class_course_course_courses_id",
            schema: "public",
            table: "class_course_course",
            column: "courses_id");

        migrationBuilder.CreateIndex(
            name: "ix_registration_academy_id",
            schema: "public",
            table: "registration",
            column: "academy_id");

        migrationBuilder.CreateIndex(
            name: "ix_registration_current_class_id",
            schema: "public",
            table: "registration",
            column: "current_class_id");

        migrationBuilder.CreateIndex(
            name: "ix_registration_current_school_year_id",
            schema: "public",
            table: "registration",
            column: "current_school_year_id");

        migrationBuilder.CreateIndex(
            name: "ix_registration_student_id_academy_id_current_school_year_id",
            schema: "public",
            table: "registration",
            columns: columnsArray0,
            unique: true);

        migrationBuilder.AddForeignKey(
            name: "fk_class_courses_academies_academy_id",
            schema: "public",
            table: "class_courses",
            column: "academy_id",
            principalSchema: "public",
            principalTable: "academies",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "fk_class_courses_school_years_school_year_id",
            schema: "public",
            table: "class_courses",
            column: "school_year_id",
            principalSchema: "public",
            principalTable: "school_years",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "fk_course_credits_courses_course_id1",
            schema: "public",
            table: "course_credits",
            column: "course_id1",
            principalSchema: "public",
            principalTable: "courses",
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "fk_course_credits_school_years_school_year_id",
            schema: "public",
            table: "course_credits",
            column: "school_year_id",
            principalSchema: "public",
            principalTable: "school_years",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_class_courses_academies_academy_id",
            schema: "public",
            table: "class_courses");

        migrationBuilder.DropForeignKey(
            name: "fk_class_courses_school_years_school_year_id",
            schema: "public",
            table: "class_courses");

        migrationBuilder.DropForeignKey(
            name: "fk_course_credits_courses_course_id1",
            schema: "public",
            table: "course_credits");

        migrationBuilder.DropForeignKey(
            name: "fk_course_credits_school_years_school_year_id",
            schema: "public",
            table: "course_credits");

        migrationBuilder.DropTable(
            name: "class_course_course",
            schema: "public");

        migrationBuilder.DropTable(
            name: "registration",
            schema: "public");

        migrationBuilder.DropTable(
            name: "students",
            schema: "public");

        migrationBuilder.DropIndex(
            name: "ix_course_credits_course_id1",
            schema: "public",
            table: "course_credits");

        migrationBuilder.DropIndex(
            name: "ix_course_credits_school_year_id",
            schema: "public",
            table: "course_credits");

        migrationBuilder.DropIndex(
            name: "ix_classes_academy_id_label",
            schema: "public",
            table: "classes");

        migrationBuilder.DropIndex(
            name: "ix_classes_academy_id_name",
            schema: "public",
            table: "classes");

        migrationBuilder.DropIndex(
            name: "ix_class_courses_academy_id",
            schema: "public",
            table: "class_courses");

        migrationBuilder.DropIndex(
            name: "ix_class_courses_school_year_id",
            schema: "public",
            table: "class_courses");

        migrationBuilder.DropColumn(
            name: "course_id1",
            schema: "public",
            table: "course_credits");

        migrationBuilder.DropColumn(
            name: "school_year_id",
            schema: "public",
            table: "course_credits");

        migrationBuilder.AddColumn<Guid>(
            name: "class_course_id",
            schema: "public",
            table: "courses",
            type: "uuid",
            nullable: true);

        migrationBuilder.CreateTable(
            name: "todo_item",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                completed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                description = table.Column<string>(type: "text", nullable: false),
                due_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                is_completed = table.Column<bool>(type: "boolean", nullable: false),
                labels = table.Column<List<string>>(type: "text[]", nullable: false),
                priority = table.Column<int>(type: "integer", nullable: false),
                update_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                user_id = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_todo_item", x => x.id);
                table.ForeignKey(
                    name: "fk_todo_item_users_user_id",
                    column: x => x.user_id,
                    principalSchema: "public",
                    principalTable: "users",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "ix_courses_class_course_id",
            schema: "public",
            table: "courses",
            column: "class_course_id");

        migrationBuilder.CreateIndex(
            name: "ix_classes_academy_id",
            schema: "public",
            table: "classes",
            column: "academy_id");

        migrationBuilder.CreateIndex(
            name: "ix_todo_item_user_id",
            schema: "public",
            table: "todo_item",
            column: "user_id");

        migrationBuilder.AddForeignKey(
            name: "fk_courses_class_courses_class_course_id",
            schema: "public",
            table: "courses",
            column: "class_course_id",
            principalSchema: "public",
            principalTable: "class_courses",
            principalColumn: "id");
    }
}
