using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateTodoListRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ToDoListRelationships",
                columns: table => new
                {
                    ParentListId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ChildListId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoListRelationships", x => new { x.ParentListId, x.ChildListId });
                    table.ForeignKey(
                        name: "FK_ToDoListRelationships_ToDoLists_ChildListId",
                        column: x => x.ChildListId,
                        principalTable: "ToDoLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ToDoListRelationships_ToDoLists_ParentListId",
                        column: x => x.ParentListId,
                        principalTable: "ToDoLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToDoListRelationships_ChildListId",
                table: "ToDoListRelationships",
                column: "ChildListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToDoListRelationships");
        }
    }
}
