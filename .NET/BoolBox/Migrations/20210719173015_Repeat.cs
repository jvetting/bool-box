using Microsoft.EntityFrameworkCore.Migrations;

namespace BoolBox.Migrations
{
    public partial class Repeat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Repeat",
                table: "Bool",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Repeat",
                table: "Bool");
        }
    }
}
