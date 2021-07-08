using Microsoft.EntityFrameworkCore.Migrations;

namespace Apeyai.Persistence.Sqlite.Migrations
{
    public partial class RenameSchemaAndAttributeTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseAttributeDbEntity_Schemas_SchemaId",
                table: "BaseAttributeDbEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Schemas",
                table: "Schemas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseAttributeDbEntity",
                table: "BaseAttributeDbEntity");

            migrationBuilder.RenameTable(
                name: "Schemas",
                newName: "Schema");

            migrationBuilder.RenameTable(
                name: "BaseAttributeDbEntity",
                newName: "Attribute");

            migrationBuilder.RenameIndex(
                name: "IX_Schemas_Name",
                table: "Schema",
                newName: "IX_Schema_Name");

            migrationBuilder.RenameIndex(
                name: "IX_BaseAttributeDbEntity_SchemaId",
                table: "Attribute",
                newName: "IX_Attribute_SchemaId");

            migrationBuilder.RenameIndex(
                name: "IX_BaseAttributeDbEntity_Name",
                table: "Attribute",
                newName: "IX_Attribute_Name");

            migrationBuilder.AddColumn<string>(
                name: "ForeignSchemaName",
                table: "Attribute",
                type: "TEXT",
                nullable: true);

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schema",
                table: "Schema",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attribute",
                table: "Attribute",
                column: "Id");

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
                name: "FK_Attribute_Schema_SchemaId",
                table: "Attribute",
                column: "SchemaId",
                principalTable: "Schema",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attribute_Schema_TextAttributeDbEntity_SchemaDbEntityId",
                table: "Attribute",
                column: "TextAttributeDbEntity_SchemaDbEntityId",
                principalTable: "Schema",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attribute_Schema_SchemaDbEntityId",
                table: "Attribute");

            migrationBuilder.DropForeignKey(
                name: "FK_Attribute_Schema_SchemaId",
                table: "Attribute");

            migrationBuilder.DropForeignKey(
                name: "FK_Attribute_Schema_TextAttributeDbEntity_SchemaDbEntityId",
                table: "Attribute");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Schema",
                table: "Schema");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attribute",
                table: "Attribute");

            migrationBuilder.DropIndex(
                name: "IX_Attribute_SchemaDbEntityId",
                table: "Attribute");

            migrationBuilder.DropIndex(
                name: "IX_Attribute_TextAttributeDbEntity_SchemaDbEntityId",
                table: "Attribute");

            migrationBuilder.DropColumn(
                name: "ForeignSchemaName",
                table: "Attribute");

            migrationBuilder.DropColumn(
                name: "SchemaDbEntityId",
                table: "Attribute");

            migrationBuilder.DropColumn(
                name: "TextAttributeDbEntity_SchemaDbEntityId",
                table: "Attribute");

            migrationBuilder.RenameTable(
                name: "Schema",
                newName: "Schemas");

            migrationBuilder.RenameTable(
                name: "Attribute",
                newName: "BaseAttributeDbEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Schema_Name",
                table: "Schemas",
                newName: "IX_Schemas_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Attribute_SchemaId",
                table: "BaseAttributeDbEntity",
                newName: "IX_BaseAttributeDbEntity_SchemaId");

            migrationBuilder.RenameIndex(
                name: "IX_Attribute_Name",
                table: "BaseAttributeDbEntity",
                newName: "IX_BaseAttributeDbEntity_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schemas",
                table: "Schemas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseAttributeDbEntity",
                table: "BaseAttributeDbEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseAttributeDbEntity_Schemas_SchemaId",
                table: "BaseAttributeDbEntity",
                column: "SchemaId",
                principalTable: "Schemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
