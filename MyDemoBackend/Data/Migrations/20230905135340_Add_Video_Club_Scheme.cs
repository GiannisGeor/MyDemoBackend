using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddVideoClubScheme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pelatis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Onoma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tilefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pelatis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sintelestis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Onoma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sintelestis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tainia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titlos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Xronia = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tainia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kasetes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Posotita = table.Column<int>(type: "int", nullable: false),
                    Timi = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IDTainias = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kasetes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kasetes_Tainia_IDTainias",
                        column: x => x.IDTainias,
                        principalTable: "Tainia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tn_sn",
                columns: table => new
                {
                    IDTainias = table.Column<int>(type: "int", nullable: false),
                    IDSintelesti = table.Column<int>(type: "int", nullable: false),
                    Rolos = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tn_sn", x => new { x.IDTainias, x.IDSintelesti });
                    table.ForeignKey(
                        name: "FK_Tn_sn_Sintelestis_IDSintelesti",
                        column: x => x.IDSintelesti,
                        principalTable: "Sintelestis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tn_sn_Tainia_IDTainias",
                        column: x => x.IDTainias,
                        principalTable: "Tainia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enoikiasi",
                columns: table => new
                {
                    IDPelati = table.Column<int>(type: "int", nullable: false),
                    IDKasetas = table.Column<int>(type: "int", nullable: false),
                    Apo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Eos = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enoikiasi", x => new { x.IDKasetas, x.IDPelati });
                    table.ForeignKey(
                        name: "FK_Enoikiasi_Kasetes_IDKasetas",
                        column: x => x.IDKasetas,
                        principalTable: "Kasetes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enoikiasi_Pelatis_IDPelati",
                        column: x => x.IDPelati,
                        principalTable: "Pelatis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enoikiasi_IDPelati",
                table: "Enoikiasi",
                column: "IDPelati");

            migrationBuilder.CreateIndex(
                name: "IX_Kasetes_IDTainias",
                table: "Kasetes",
                column: "IDTainias");

            migrationBuilder.CreateIndex(
                name: "IX_Tn_sn_IDSintelesti",
                table: "Tn_sn",
                column: "IDSintelesti");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enoikiasi");

            migrationBuilder.DropTable(
                name: "Tn_sn");

            migrationBuilder.DropTable(
                name: "Kasetes");

            migrationBuilder.DropTable(
                name: "Pelatis");

            migrationBuilder.DropTable(
                name: "Sintelestis");

            migrationBuilder.DropTable(
                name: "Tainia");
        }
    }
}
