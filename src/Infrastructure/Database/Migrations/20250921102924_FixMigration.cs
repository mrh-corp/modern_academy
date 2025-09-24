using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class FixMigration : Migration
{
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
