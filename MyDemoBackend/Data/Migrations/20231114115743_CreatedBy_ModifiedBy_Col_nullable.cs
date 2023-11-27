using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class CreatedBy_ModifiedBy_Col_nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
            update OptionsGroup set CreatedBy = null;
            update Options set CreatedBy = null;
            update BaseOptions set CreatedBy = null;           
            update OptionsGroup set ModifiedBy = null;
            update Options set ModifiedBy = null;
            update BaseOptions set ModifiedBy = null;
            update OptionsGroup set DeletedBy = null;
            update Options set DeletedBy = null;
            update BaseOptions set DeletedBy = null;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
