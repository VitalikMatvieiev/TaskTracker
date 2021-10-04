using Microsoft.EntityFrameworkCore.Migrations;

namespace Trelo1.Migrations
{
    public partial class AddForingKeysToTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Organizations_OrganizationId",
                table: "Boards");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Boards_BoardId",
                table: "Tasks");

            migrationBuilder.AlterColumn<int>(
                name: "BoardId",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrganizationId",
                table: "Boards",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Boards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_Organizations_OrganizationId",
                table: "Boards",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Boards_BoardId",
                table: "Tasks",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Organizations_OrganizationId",
                table: "Boards");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Boards_BoardId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Boards");

            migrationBuilder.AlterColumn<int>(
                name: "BoardId",
                table: "Tasks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizationId",
                table: "Boards",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_Organizations_OrganizationId",
                table: "Boards",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Boards_BoardId",
                table: "Tasks",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
