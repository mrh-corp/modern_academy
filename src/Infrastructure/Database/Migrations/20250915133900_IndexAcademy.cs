using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class IndexAcademy : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateIndex(
            name: "ix_academies_name",
            schema: "public",
            table: "academies",
            column: "name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_academies_tenant_name",
            schema: "public",
            table: "academies",
            column: "tenant_name",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "ix_academies_name",
            schema: "public",
            table: "academies");

        migrationBuilder.DropIndex(
            name: "ix_academies_tenant_name",
            schema: "public",
            table: "academies");
    }
}
