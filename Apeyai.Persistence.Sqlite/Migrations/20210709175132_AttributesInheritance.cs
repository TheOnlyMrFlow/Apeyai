using Microsoft.EntityFrameworkCore.Migrations;

namespace Apeyai.Persistence.Sqlite.Migrations
{
    public partial class AttributesInheritance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TextAttributes_Schemas_SchemaId",
                table: "TextAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TextAttributes",
                table: "TextAttributes");

            migrationBuilder.RenameTable(
                name: "TextAttributes",
                newName: "BaseAttributeDbEntity");

            migrationBuilder.RenameIndex(
                name: "IX_TextAttributes_SchemaId",
                table: "BaseAttributeDbEntity",
                newName: "IX_BaseAttributeDbEntity_SchemaId");

            migrationBuilder.RenameIndex(
                name: "IX_TextAttributes_Name",
                table: "BaseAttributeDbEntity",
                newName: "IX_BaseAttributeDbEntity_Name");

            migrationBuilder.AlterColumn<int>(
                name: "MinLength",
                table: "BaseAttributeDbEntity",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "MaxLength",
                table: "BaseAttributeDbEntity",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "BaseAttributeDbEntity",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseAttributeDbEntity",
                table: "BaseAttributeDbEntity",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UniqueAttributeNameAmongSchemaConstraint",
                table: "BaseAttributeDbEntity",
                columns: new[] { "Name", "SchemaId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseAttributeDbEntity_Schemas_SchemaId",
                table: "BaseAttributeDbEntity",
                column: "SchemaId",
                principalTable: "Schemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseAttributeDbEntity_Schemas_SchemaId",
                table: "BaseAttributeDbEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseAttributeDbEntity",
                table: "BaseAttributeDbEntity");

            migrationBuilder.DropIndex(
                name: "IX_UniqueAttributeNameAmongSchemaConstraint",
                table: "BaseAttributeDbEntity");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "BaseAttributeDbEntity");

            migrationBuilder.RenameTable(
                name: "BaseAttributeDbEntity",
                newName: "TextAttributes");

            migrationBuilder.RenameIndex(
                name: "IX_BaseAttributeDbEntity_SchemaId",
                table: "TextAttributes",
                newName: "IX_TextAttributes_SchemaId");

            migrationBuilder.RenameIndex(
                name: "IX_BaseAttributeDbEntity_Name",
                table: "TextAttributes",
                newName: "IX_TextAttributes_Name");

            migrationBuilder.AlterColumn<int>(
                name: "MinLength",
                table: "TextAttributes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MaxLength",
                table: "TextAttributes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TextAttributes",
                table: "TextAttributes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TextAttributes_Schemas_SchemaId",
                table: "TextAttributes",
                column: "SchemaId",
                principalTable: "Schemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
