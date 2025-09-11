using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class ClassCourseMigration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<Guid>(
            name: "academy_id",
            schema: "public",
            table: "classes",
            type: "uuid",
            nullable: false,
            defaultValue: Guid.Empty);

        migrationBuilder.AddColumn<Guid>(
            name: "next_class_id",
            schema: "public",
            table: "classes",
            type: "uuid",
            nullable: true);

        migrationBuilder.AddColumn<Guid>(
            name: "previous_class_id",
            schema: "public",
            table: "classes",
            type: "uuid",
            nullable: true);

        migrationBuilder.AddColumn<int>(
            name: "section",
            schema: "public",
            table: "classes",
            type: "integer",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.CreateTable(
            name: "class_courses",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                academy_id = table.Column<Guid>(type: "uuid", nullable: false),
                class_id = table.Column<Guid>(type: "uuid", nullable: false),
                school_year_id = table.Column<Guid>(type: "uuid", nullable: false),
                created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                update_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_class_courses", x => x.id);
                table.ForeignKey(
                    name: "fk_class_courses_academies_academy_id",
                    column: x => x.academy_id,
                    principalSchema: "public",
                    principalTable: "academies",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_class_courses_classes_class_id",
                    column: x => x.class_id,
                    principalSchema: "public",
                    principalTable: "classes",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_class_courses_school_years_school_year_id",
                    column: x => x.school_year_id,
                    principalSchema: "public",
                    principalTable: "school_years",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "courses",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "text", nullable: false),
                label = table.Column<string>(type: "text", nullable: false),
                description = table.Column<string>(type: "text", nullable: false),
                class_course_id = table.Column<Guid>(type: "uuid", nullable: true),
                created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                update_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_courses", x => x.id);
                table.ForeignKey(
                    name: "fk_courses_class_courses_class_course_id",
                    column: x => x.class_course_id,
                    principalSchema: "public",
                    principalTable: "class_courses",
                    principalColumn: "id");
            });

        migrationBuilder.CreateTable(
            name: "course_credits",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                credit = table.Column<double>(type: "double precision", nullable: false),
                course_id = table.Column<Guid>(type: "uuid", nullable: false),
                created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                update_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_course_credits", x => x.id);
                table.ForeignKey(
                    name: "fk_course_credits_courses_course_id",
                    column: x => x.course_id,
                    principalSchema: "public",
                    principalTable: "courses",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "ix_classes_academy_id",
            schema: "public",
            table: "classes",
            column: "academy_id");

        migrationBuilder.CreateIndex(
            name: "ix_classes_next_class_id",
            schema: "public",
            table: "classes",
            column: "next_class_id",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_classes_previous_class_id",
            schema: "public",
            table: "classes",
            column: "previous_class_id",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_class_courses_academy_id",
            schema: "public",
            table: "class_courses",
            column: "academy_id");

        migrationBuilder.CreateIndex(
            name: "ix_class_courses_class_id",
            schema: "public",
            table: "class_courses",
            column: "class_id");

        migrationBuilder.CreateIndex(
            name: "ix_class_courses_school_year_id",
            schema: "public",
            table: "class_courses",
            column: "school_year_id");

        migrationBuilder.CreateIndex(
            name: "ix_course_credits_course_id",
            schema: "public",
            table: "course_credits",
            column: "course_id");

        migrationBuilder.CreateIndex(
            name: "ix_courses_class_course_id",
            schema: "public",
            table: "courses",
            column: "class_course_id");

        migrationBuilder.AddForeignKey(
            name: "fk_classes_academies_academy_id",
            schema: "public",
            table: "classes",
            column: "academy_id",
            principalSchema: "public",
            principalTable: "academies",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "fk_classes_classes_next_class_id",
            schema: "public",
            table: "classes",
            column: "next_class_id",
            principalSchema: "public",
            principalTable: "classes",
            principalColumn: "id",
            onDelete: ReferentialAction.Restrict);

        migrationBuilder.AddForeignKey(
            name: "fk_classes_classes_previous_class_id",
            schema: "public",
            table: "classes",
            column: "previous_class_id",
            principalSchema: "public",
            principalTable: "classes",
            principalColumn: "id",
            onDelete: ReferentialAction.Restrict);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_classes_academies_academy_id",
            schema: "public",
            table: "classes");

        migrationBuilder.DropForeignKey(
            name: "fk_classes_classes_next_class_id",
            schema: "public",
            table: "classes");

        migrationBuilder.DropForeignKey(
            name: "fk_classes_classes_previous_class_id",
            schema: "public",
            table: "classes");

        migrationBuilder.DropTable(
            name: "course_credits",
            schema: "public");

        migrationBuilder.DropTable(
            name: "courses",
            schema: "public");

        migrationBuilder.DropTable(
            name: "class_courses",
            schema: "public");

        migrationBuilder.DropIndex(
            name: "ix_classes_academy_id",
            schema: "public",
            table: "classes");

        migrationBuilder.DropIndex(
            name: "ix_classes_next_class_id",
            schema: "public",
            table: "classes");

        migrationBuilder.DropIndex(
            name: "ix_classes_previous_class_id",
            schema: "public",
            table: "classes");

        migrationBuilder.DropColumn(
            name: "academy_id",
            schema: "public",
            table: "classes");

        migrationBuilder.DropColumn(
            name: "next_class_id",
            schema: "public",
            table: "classes");

        migrationBuilder.DropColumn(
            name: "previous_class_id",
            schema: "public",
            table: "classes");

        migrationBuilder.DropColumn(
            name: "section",
            schema: "public",
            table: "classes");
    }
}
