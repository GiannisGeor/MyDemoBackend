using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class test_test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OptionsGroup_Products_ProductId",
                table: "OptionsGroup");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "OptionsGroup",
                newName: "StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_OptionsGroup_ProductId",
                table: "OptionsGroup",
                newName: "IX_OptionsGroup_StoreId");

            migrationBuilder.AlterColumn<int>(
                name: "OptionsGroupId",
                table: "Options",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProductOptionsGroupId",
                table: "Options",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "BaseOptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProductOptionsGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    OptionsGroupId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOptionsGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductOptionsGroups_OptionsGroup_OptionsGroupId",
                        column: x => x.OptionsGroupId,
                        principalTable: "OptionsGroup",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductOptionsGroups_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Options_ProductOptionsGroupId",
                table: "Options",
                column: "ProductOptionsGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseOptions_StoreId",
                table: "BaseOptions",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOptionsGroups_OptionsGroupId",
                table: "ProductOptionsGroups",
                column: "OptionsGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOptionsGroups_ProductId",
                table: "ProductOptionsGroups",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseOptions_Stores_StoreId",
                table: "BaseOptions",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_ProductOptionsGroups_ProductOptionsGroupId",
                table: "Options",
                column: "ProductOptionsGroupId",
                principalTable: "ProductOptionsGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OptionsGroup_Stores_StoreId",
                table: "OptionsGroup",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseOptions_Stores_StoreId",
                table: "BaseOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Options_ProductOptionsGroups_ProductOptionsGroupId",
                table: "Options");

            migrationBuilder.DropForeignKey(
                name: "FK_OptionsGroup_Stores_StoreId",
                table: "OptionsGroup");

            migrationBuilder.DropTable(
                name: "ProductOptionsGroups");

            migrationBuilder.DropIndex(
                name: "IX_Options_ProductOptionsGroupId",
                table: "Options");

            migrationBuilder.DropIndex(
                name: "IX_BaseOptions_StoreId",
                table: "BaseOptions");

            migrationBuilder.DropColumn(
                name: "ProductOptionsGroupId",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "BaseOptions");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "OptionsGroup",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OptionsGroup_StoreId",
                table: "OptionsGroup",
                newName: "IX_OptionsGroup_ProductId");

            migrationBuilder.AlterColumn<int>(
                name: "OptionsGroupId",
                table: "Options",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OptionsGroup_Products_ProductId",
                table: "OptionsGroup",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
