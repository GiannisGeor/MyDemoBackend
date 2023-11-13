using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Application");

            migrationBuilder.CreateTable(
                name: "EmailTemplate",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextString = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasHtml = table.Column<bool>(type: "bit", nullable: false),
                    EmailType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Translations",
                schema: "Application",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LanguageIdentifier = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    TranslatedText = table.Column<string>(type: "nvarchar(max)", maxLength: 8000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translations", x => new { x.Key, x.LanguageIdentifier });
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplate_EmailType",
                schema: "Application",
                table: "EmailTemplate",
                column: "EmailType",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailTemplate",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "Translations",
                schema: "Application");
        }
    }
}
