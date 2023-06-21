using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableToDoAddParentId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "ToDos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ToDos_ParentId",
                table: "ToDos",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDos_ToDos_ParentId",
                table: "ToDos",
                column: "ParentId",
                principalTable: "ToDos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDos_ToDos_ParentId",
                table: "ToDos");

            migrationBuilder.DropIndex(
                name: "IX_ToDos_ParentId",
                table: "ToDos");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "ToDos");
        }
    }
}
