using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations;
/// <inheritdoc />
public partial class Academy : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_todo_items_users_user_id",
            schema: "public",
            table: "todo_items");

        migrationBuilder.DropPrimaryKey(
            name: "pk_todo_items",
            schema: "public",
            table: "todo_items");

        migrationBuilder.RenameTable(
            name: "todo_items",
            schema: "public",
            newName: "todo_item",
            newSchema: "public");

        migrationBuilder.RenameIndex(
            name: "ix_todo_items_user_id",
            schema: "public",
            table: "todo_item",
            newName: "ix_todo_item_user_id");

        migrationBuilder.AddColumn<Guid>(
            name: "academy_id",
            schema: "public",
            table: "users",
            type: "uuid",
            nullable: true);

        migrationBuilder.AddPrimaryKey(
            name: "pk_todo_item",
            schema: "public",
            table: "todo_item",
            column: "id");

        migrationBuilder.CreateTable(
            name: "academies",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "text", nullable: false),
                description = table.Column<string>(type: "text", nullable: false),
                email = table.Column<string>(type: "text", nullable: false),
                contact = table.Column<string>(type: "text", nullable: false),
                created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                update_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
#pragma warning disable IDE0053
            constraints: table =>
#pragma warning restore IDE0053
            {
                table.PrimaryKey("pk_academies", x => x.id);
            });

        migrationBuilder.CreateIndex(
            name: "ix_users_academy_id",
            schema: "public",
            table: "users",
            column: "academy_id");

        migrationBuilder.AddForeignKey(
            name: "fk_todo_item_users_user_id",
            schema: "public",
            table: "todo_item",
            column: "user_id",
            principalSchema: "public",
            principalTable: "users",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "fk_users_academies_academy_id",
            schema: "public",
            table: "users",
            column: "academy_id",
            principalSchema: "public",
            principalTable: "academies",
            principalColumn: "id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_todo_item_users_user_id",
            schema: "public",
            table: "todo_item");

        migrationBuilder.DropForeignKey(
            name: "fk_users_academies_academy_id",
            schema: "public",
            table: "users");

        migrationBuilder.DropTable(
            name: "academies",
            schema: "public");

        migrationBuilder.DropIndex(
            name: "ix_users_academy_id",
            schema: "public",
            table: "users");

        migrationBuilder.DropPrimaryKey(
            name: "pk_todo_item",
            schema: "public",
            table: "todo_item");

        migrationBuilder.DropColumn(
            name: "academy_id",
            schema: "public",
            table: "users");

        migrationBuilder.RenameTable(
            name: "todo_item",
            schema: "public",
            newName: "todo_items",
            newSchema: "public");

        migrationBuilder.RenameIndex(
            name: "ix_todo_item_user_id",
            schema: "public",
            table: "todo_items",
            newName: "ix_todo_items_user_id");

        migrationBuilder.AddPrimaryKey(
            name: "pk_todo_items",
            schema: "public",
            table: "todo_items",
            column: "id");

        migrationBuilder.AddForeignKey(
            name: "fk_todo_items_users_user_id",
            schema: "public",
            table: "todo_items",
            column: "user_id",
            principalSchema: "public",
            principalTable: "users",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);
    }
}
