using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class TenantName : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_class_courses_academies_academy_id",
            schema: "public",
            table: "class_courses");

        migrationBuilder.DropForeignKey(
            name: "fk_class_courses_school_years_school_year_id",
            schema: "public",
            table: "class_courses");

        migrationBuilder.DropIndex(
            name: "ix_class_courses_academy_id",
            schema: "public",
            table: "class_courses");

        migrationBuilder.DropIndex(
            name: "ix_class_courses_school_year_id",
            schema: "public",
            table: "class_courses");

        migrationBuilder.AddColumn<string>(
            name: "tenant_name",
            schema: "public",
            table: "academies",
            type: "text",
            nullable: false,
            defaultValue: "");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "tenant_name",
            schema: "public",
            table: "academies");

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
    }
}
