using Microsoft.EntityFrameworkCore.Migrations;

namespace Apeyai.Persistence.Sqlite.Migrations
{
    public partial class IsRequiredTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRequired",
                table: "TextAttributes",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRequired",
                table: "TextAttributes");
        }
    }
}
