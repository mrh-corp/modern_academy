using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class NoteTestTrimester : Migration
{
    private static readonly string[] columns = new[] { "student_id", "course_id", "test_id" };
    private static readonly string[] columnsArray = new[] { "name", "trimester_id" };
    private static readonly string[] columnsArray0 = new[] { "name", "school_year_id" };

    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_registration_academies_academy_id",
            schema: "public",
            table: "registration");

        migrationBuilder.DropForeignKey(
            name: "fk_registration_classes_current_class_id",
            schema: "public",
            table: "registration");

        migrationBuilder.DropForeignKey(
            name: "fk_registration_school_years_current_school_year_id",
            schema: "public",
            table: "registration");

        migrationBuilder.DropForeignKey(
            name: "fk_registration_students_student_id",
            schema: "public",
            table: "registration");

        migrationBuilder.DropPrimaryKey(
            name: "pk_registration",
            schema: "public",
            table: "registration");

        migrationBuilder.RenameTable(
            name: "registration",
            schema: "public",
            newName: "registrations",
            newSchema: "public");

        migrationBuilder.RenameIndex(
            name: "ix_registration_student_id_academy_id_current_school_year_id",
            schema: "public",
            table: "registrations",
            newName: "ix_registrations_student_id_academy_id_current_school_year_id");

        migrationBuilder.RenameIndex(
            name: "ix_registration_current_school_year_id",
            schema: "public",
            table: "registrations",
            newName: "ix_registrations_current_school_year_id");

        migrationBuilder.RenameIndex(
            name: "ix_registration_current_class_id",
            schema: "public",
            table: "registrations",
            newName: "ix_registrations_current_class_id");

        migrationBuilder.RenameIndex(
            name: "ix_registration_academy_id",
            schema: "public",
            table: "registrations",
            newName: "ix_registrations_academy_id");

        migrationBuilder.AddPrimaryKey(
            name: "pk_registrations",
            schema: "public",
            table: "registrations",
            column: "id");

        migrationBuilder.CreateTable(
            name: "trimester",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "text", nullable: false),
                school_year_id = table.Column<Guid>(type: "uuid", nullable: false),
                percentage = table.Column<double>(type: "double precision", nullable: false),
                created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                update_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_trimester", x => x.id);
                table.ForeignKey(
                    name: "fk_trimester_school_years_school_year_id",
                    column: x => x.school_year_id,
                    principalSchema: "public",
                    principalTable: "school_years",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "test",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "text", nullable: false),
                percentage = table.Column<double>(type: "double precision", nullable: false),
                trimester_id = table.Column<Guid>(type: "uuid", nullable: false),
                created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                update_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_test", x => x.id);
                table.ForeignKey(
                    name: "fk_test_trimester_trimester_id",
                    column: x => x.trimester_id,
                    principalSchema: "public",
                    principalTable: "trimester",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "note",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                value = table.Column<double>(type: "double precision", nullable: false),
                student_id = table.Column<Guid>(type: "uuid", nullable: false),
                course_id = table.Column<Guid>(type: "uuid", nullable: false),
                test_id = table.Column<Guid>(type: "uuid", nullable: false),
                created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                update_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_note", x => x.id);
                table.ForeignKey(
                    name: "fk_note_courses_course_id",
                    column: x => x.course_id,
                    principalSchema: "public",
                    principalTable: "courses",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_note_students_student_id",
                    column: x => x.student_id,
                    principalSchema: "public",
                    principalTable: "students",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_note_test_test_id",
                    column: x => x.test_id,
                    principalSchema: "public",
                    principalTable: "test",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "ix_note_course_id",
            schema: "public",
            table: "note",
            column: "course_id");

        migrationBuilder.CreateIndex(
            name: "ix_note_student_id_course_id_test_id",
            schema: "public",
            table: "note",
            columns: columns,
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_note_test_id",
            schema: "public",
            table: "note",
            column: "test_id");

        migrationBuilder.CreateIndex(
            name: "ix_test_name_trimester_id",
            schema: "public",
            table: "test",
            columns: columnsArray);

        migrationBuilder.CreateIndex(
            name: "ix_test_trimester_id",
            schema: "public",
            table: "test",
            column: "trimester_id");

        migrationBuilder.CreateIndex(
            name: "ix_trimester_name_school_year_id",
            schema: "public",
            table: "trimester",
            columns: columnsArray0);

        migrationBuilder.CreateIndex(
            name: "ix_trimester_school_year_id",
            schema: "public",
            table: "trimester",
            column: "school_year_id");

        migrationBuilder.AddForeignKey(
            name: "fk_registrations_academies_academy_id",
            schema: "public",
            table: "registrations",
            column: "academy_id",
            principalSchema: "public",
            principalTable: "academies",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "fk_registrations_classes_current_class_id",
            schema: "public",
            table: "registrations",
            column: "current_class_id",
            principalSchema: "public",
            principalTable: "classes",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "fk_registrations_school_years_current_school_year_id",
            schema: "public",
            table: "registrations",
            column: "current_school_year_id",
            principalSchema: "public",
            principalTable: "school_years",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "fk_registrations_students_student_id",
            schema: "public",
            table: "registrations",
            column: "student_id",
            principalSchema: "public",
            principalTable: "students",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_registrations_academies_academy_id",
            schema: "public",
            table: "registrations");

        migrationBuilder.DropForeignKey(
            name: "fk_registrations_classes_current_class_id",
            schema: "public",
            table: "registrations");

        migrationBuilder.DropForeignKey(
            name: "fk_registrations_school_years_current_school_year_id",
            schema: "public",
            table: "registrations");

        migrationBuilder.DropForeignKey(
            name: "fk_registrations_students_student_id",
            schema: "public",
            table: "registrations");

        migrationBuilder.DropTable(
            name: "note",
            schema: "public");

        migrationBuilder.DropTable(
            name: "test",
            schema: "public");

        migrationBuilder.DropTable(
            name: "trimester",
            schema: "public");

        migrationBuilder.DropPrimaryKey(
            name: "pk_registrations",
            schema: "public",
            table: "registrations");

        migrationBuilder.RenameTable(
            name: "registrations",
            schema: "public",
            newName: "registration",
            newSchema: "public");

        migrationBuilder.RenameIndex(
            name: "ix_registrations_student_id_academy_id_current_school_year_id",
            schema: "public",
            table: "registration",
            newName: "ix_registration_student_id_academy_id_current_school_year_id");

        migrationBuilder.RenameIndex(
            name: "ix_registrations_current_school_year_id",
            schema: "public",
            table: "registration",
            newName: "ix_registration_current_school_year_id");

        migrationBuilder.RenameIndex(
            name: "ix_registrations_current_class_id",
            schema: "public",
            table: "registration",
            newName: "ix_registration_current_class_id");

        migrationBuilder.RenameIndex(
            name: "ix_registrations_academy_id",
            schema: "public",
            table: "registration",
            newName: "ix_registration_academy_id");

        migrationBuilder.AddPrimaryKey(
            name: "pk_registration",
            schema: "public",
            table: "registration",
            column: "id");

        migrationBuilder.AddForeignKey(
            name: "fk_registration_academies_academy_id",
            schema: "public",
            table: "registration",
            column: "academy_id",
            principalSchema: "public",
            principalTable: "academies",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "fk_registration_classes_current_class_id",
            schema: "public",
            table: "registration",
            column: "current_class_id",
            principalSchema: "public",
            principalTable: "classes",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "fk_registration_school_years_current_school_year_id",
            schema: "public",
            table: "registration",
            column: "current_school_year_id",
            principalSchema: "public",
            principalTable: "school_years",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "fk_registration_students_student_id",
            schema: "public",
            table: "registration",
            column: "student_id",
            principalSchema: "public",
            principalTable: "students",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);
    }
}
