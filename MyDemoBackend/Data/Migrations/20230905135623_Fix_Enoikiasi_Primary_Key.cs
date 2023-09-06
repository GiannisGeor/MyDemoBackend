using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class FixEnoikiasiPrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Enoikiasi",
                table: "Enoikiasi");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enoikiasi",
                table: "Enoikiasi",
                columns: new[] { "IDKasetas", "IDPelati", "Apo" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Enoikiasi",
                table: "Enoikiasi");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enoikiasi",
                table: "Enoikiasi",
                columns: new[] { "IDKasetas", "IDPelati" });
        }
    }
}
