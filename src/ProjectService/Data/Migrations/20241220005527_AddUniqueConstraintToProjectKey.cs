using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectService.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraintToProjectKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectKey",
                table: "Projects",
                column: "ProjectKey",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Projects_ProjectKey",
                table: "Projects");
        }
    }
}
