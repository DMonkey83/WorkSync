using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectService.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IssuePriorities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PriorityName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssuePriorities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IssueStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StatusName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IssueTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IssueTypeName = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectName = table.Column<string>(type: "text", nullable: true),
                    ProjectKey = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    LeadUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    ComponentName = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    LeadUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Components_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sprints",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    SprintName = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Goal = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sprints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sprints_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Issues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    IssueTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReporterId = table.Column<Guid>(type: "uuid", nullable: false),
                    AssigneeId = table.Column<Guid>(type: "uuid", nullable: false),
                    PriorityId = table.Column<Guid>(type: "uuid", nullable: false),
                    StatusId = table.Column<Guid>(type: "uuid", nullable: false),
                    ComponentId = table.Column<Guid>(type: "uuid", nullable: false),
                    SprintId = table.Column<Guid>(type: "uuid", nullable: false),
                    Summary = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    OriginalEstimate = table.Column<int>(type: "integer", nullable: false),
                    RemainingEstimate = table.Column<int>(type: "integer", nullable: false),
                    TimeSpent = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Issues_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Issues_IssuePriorities_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "IssuePriorities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Issues_IssueStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "IssueStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Issues_IssueTypes_IssueTypeId",
                        column: x => x.IssueTypeId,
                        principalTable: "IssueTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Issues_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Issues_Sprints_SprintId",
                        column: x => x.SprintId,
                        principalTable: "Sprints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IssueComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IssueId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CommentText = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueComments_Issues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IssueCustomFields",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IssueId = table.Column<Guid>(type: "uuid", nullable: false),
                    FieldName = table.Column<string>(type: "text", nullable: true),
                    FieldValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueCustomFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueCustomFields_Issues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IssueHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IssueId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChangedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    FieldName = table.Column<string>(type: "text", nullable: true),
                    OldValue = table.Column<string>(type: "text", nullable: true),
                    NewValue = table.Column<string>(type: "text", nullable: true),
                    ChangedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueHistories_Issues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IssueLabels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IssueId = table.Column<Guid>(type: "uuid", nullable: false),
                    LabelName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueLabels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueLabels_Issues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Components_ProjectId",
                table: "Components",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueComments_IssueId",
                table: "IssueComments",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueCustomFields_IssueId",
                table: "IssueCustomFields",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueHistories_IssueId",
                table: "IssueHistories",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueLabels_IssueId",
                table: "IssueLabels",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_ComponentId",
                table: "Issues",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_IssueTypeId",
                table: "Issues",
                column: "IssueTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_PriorityId",
                table: "Issues",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_ProjectId",
                table: "Issues",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_SprintId",
                table: "Issues",
                column: "SprintId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_StatusId",
                table: "Issues",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Sprints_ProjectId",
                table: "Sprints",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IssueComments");

            migrationBuilder.DropTable(
                name: "IssueCustomFields");

            migrationBuilder.DropTable(
                name: "IssueHistories");

            migrationBuilder.DropTable(
                name: "IssueLabels");

            migrationBuilder.DropTable(
                name: "Issues");

            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.DropTable(
                name: "IssuePriorities");

            migrationBuilder.DropTable(
                name: "IssueStatuses");

            migrationBuilder.DropTable(
                name: "IssueTypes");

            migrationBuilder.DropTable(
                name: "Sprints");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
