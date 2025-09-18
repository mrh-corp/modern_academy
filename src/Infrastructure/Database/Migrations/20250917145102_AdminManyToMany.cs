using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class AdminManyToMany : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_users_academies_academy_id",
            schema: "public",
            table: "users");

        migrationBuilder.DropIndex(
            name: "ix_users_academy_id",
            schema: "public",
            table: "users");

        migrationBuilder.DropColumn(
            name: "academy_id",
            schema: "public",
            table: "users");

        migrationBuilder.CreateTable(
            name: "academy_user",
            schema: "public",
            columns: table => new
            {
                academies_id = table.Column<Guid>(type: "uuid", nullable: false),
                administrators_id = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_academy_user", x => new { x.academies_id, x.administrators_id });
                table.ForeignKey(
                    name: "fk_academy_user_academies_academies_id",
                    column: x => x.academies_id,
                    principalSchema: "public",
                    principalTable: "academies",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_academy_user_users_administrators_id",
                    column: x => x.administrators_id,
                    principalSchema: "public",
                    principalTable: "users",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "ix_academy_user_administrators_id",
            schema: "public",
            table: "academy_user",
            column: "administrators_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "academy_user",
            schema: "public");

        migrationBuilder.AddColumn<Guid>(
            name: "academy_id",
            schema: "public",
            table: "users",
            type: "uuid",
            nullable: true);

        migrationBuilder.CreateIndex(
            name: "ix_users_academy_id",
            schema: "public",
            table: "users",
            column: "academy_id");

        migrationBuilder.AddForeignKey(
            name: "fk_users_academies_academy_id",
            schema: "public",
            table: "users",
            column: "academy_id",
            principalSchema: "public",
            principalTable: "academies",
            principalColumn: "id");
    }
}
