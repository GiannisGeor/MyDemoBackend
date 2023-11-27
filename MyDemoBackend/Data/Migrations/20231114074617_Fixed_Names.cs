using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Fixed_Names : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isMulti",
                table: "OptionsGroup",
                newName: "IsMulti");

            migrationBuilder.RenameColumn(
                name: "isAvailable",
                table: "BaseOptions",
                newName: "IsAvailable");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsMulti",
                table: "OptionsGroup",
                newName: "isMulti");

            migrationBuilder.RenameColumn(
                name: "IsAvailable",
                table: "BaseOptions",
                newName: "isAvailable");
        }
    }
}
