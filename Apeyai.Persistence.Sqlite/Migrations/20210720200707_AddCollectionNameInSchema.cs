using Microsoft.EntityFrameworkCore.Migrations;

namespace Apeyai.Persistence.Sqlite.Migrations
{
    public partial class AddCollectionNameInSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attribute_Schema_SchemaDbEntityId",
                table: "Attribute");

            migrationBuilder.DropForeignKey(
                name: "FK_Attribute_Schema_TextAttributeDbEntity_SchemaDbEntityId",
                table: "Attribute");

            migrationBuilder.DropIndex(
                name: "IX_Attribute_SchemaDbEntityId",
                table: "Attribute");

            migrationBuilder.DropIndex(
                name: "IX_Attribute_TextAttributeDbEntity_SchemaDbEntityId",
                table: "Attribute");

            migrationBuilder.DropColumn(
                name: "SchemaDbEntityId",
                table: "Attribute");

            migrationBuilder.DropColumn(
                name: "TextAttributeDbEntity_SchemaDbEntityId",
                table: "Attribute");

            migrationBuilder.AddColumn<string>(
                name: "CollectionName",
                table: "Schema",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CollectionName",
                table: "Schema");

            migrationBuilder.AddColumn<int>(
                name: "SchemaDbEntityId",
                table: "Attribute",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TextAttributeDbEntity_SchemaDbEntityId",
                table: "Attribute",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_SchemaDbEntityId",
                table: "Attribute",
                column: "SchemaDbEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_TextAttributeDbEntity_SchemaDbEntityId",
                table: "Attribute",
                column: "TextAttributeDbEntity_SchemaDbEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attribute_Schema_SchemaDbEntityId",
                table: "Attribute",
                column: "SchemaDbEntityId",
                principalTable: "Schema",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Attribute_Schema_TextAttributeDbEntity_SchemaDbEntityId",
                table: "Attribute",
                column: "TextAttributeDbEntity_SchemaDbEntityId",
                principalTable: "Schema",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
