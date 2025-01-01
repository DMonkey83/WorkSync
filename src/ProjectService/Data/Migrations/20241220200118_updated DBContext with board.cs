using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectService.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedDBContextwithboard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BoardId",
                table: "Issues",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    BoardName = table.Column<string>(type: "text", nullable: true),
                    BoardType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Issues_BoardId",
                table: "Issues",
                column: "BoardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Boards_BoardId",
                table: "Issues",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Boards_BoardId",
                table: "Issues");

            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DropIndex(
                name: "IX_Issues_BoardId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "BoardId",
                table: "Issues");
        }
    }
}
