using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class FixEnoikiaseisTableEntriesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
            update [Enoikiasi]
            set IDPelati=1,IDKasetas=2
            where IDPelati=2 and IDKasetas=2;

              update [Enoikiasi]
            set IDPelati=2,IDKasetas=1
            where IDPelati=3 and IDKasetas=3;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
