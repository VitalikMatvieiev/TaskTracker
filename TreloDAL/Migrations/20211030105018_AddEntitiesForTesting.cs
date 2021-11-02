using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Trelo1.Migrations
{
    public partial class AddEntitiesForTesting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestLogEntityForLog1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    ChangeData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChangeTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestLogEntityForLog1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestLogEntityForLog1_Tasks_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestLogEntityForLog2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    ChangeData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChangeTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestLogEntityForLog2", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestLogEntityForLog2_Tasks_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestLogEntityForLog1_EntityId",
                table: "TestLogEntityForLog1",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_TestLogEntityForLog2_EntityId",
                table: "TestLogEntityForLog2",
                column: "EntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestLogEntityForLog1");

            migrationBuilder.DropTable(
                name: "TestLogEntityForLog2");
        }
    }
}
