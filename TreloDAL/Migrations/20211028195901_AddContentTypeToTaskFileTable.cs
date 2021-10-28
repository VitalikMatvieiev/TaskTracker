using Microsoft.EntityFrameworkCore.Migrations;

namespace Trelo1.Migrations
{
    public partial class AddContentTypeToTaskFileTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "TaskFile",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "TaskFile");
        }
    }
}
