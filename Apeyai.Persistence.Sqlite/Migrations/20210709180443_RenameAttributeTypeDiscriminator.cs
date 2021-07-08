using Microsoft.EntityFrameworkCore.Migrations;

namespace Apeyai.Persistence.Sqlite.Migrations
{
    public partial class RenameAttributeTypeDiscriminator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Discriminator",
                table: "BaseAttributeDbEntity",
                newName: "Type");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "BaseAttributeDbEntity",
                newName: "Discriminator");
        }
    }
}
