using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Trelo1.Migrations
{
    public partial class AddTaskChangesLogTableToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskChangesLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    ChangeData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChangeTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskChangesLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskChangesLogs_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskChangesLogs_TaskId",
                table: "TaskChangesLogs",
                column: "TaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskChangesLogs");
        }
    }
}
