using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class SchoolYearMigration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "classes",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "text", nullable: false),
                label = table.Column<string>(type: "text", nullable: false),
                created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                update_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
#pragma warning disable IDE0053
            constraints: table =>
#pragma warning restore IDE0053
            {
                table.PrimaryKey("pk_classes", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "school_years",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                start_date = table.Column<DateOnly>(type: "date", nullable: false),
                end_date = table.Column<DateOnly>(type: "date", nullable: false),
                academy_id = table.Column<Guid>(type: "uuid", nullable: true),
                created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                update_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_school_years", x => x.id);
                table.ForeignKey(
                    name: "fk_school_years_academies_academy_id",
                    column: x => x.academy_id,
                    principalSchema: "public",
                    principalTable: "academies",
                    principalColumn: "id");
            });

        migrationBuilder.CreateIndex(
            name: "ix_school_years_academy_id",
            schema: "public",
            table: "school_years",
            column: "academy_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "classes",
            schema: "public");

        migrationBuilder.DropTable(
            name: "school_years",
            schema: "public");
    }
}
