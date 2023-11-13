using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Fix_Deleted_Col_In_All_Data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
            update Addresses set DeletedBy = null;
            update StoreCategories set DeletedBy = null;
            update Stores set DeletedBy = null;
            update ProductCategories set DeletedBy = null;
            update Products set DeletedBy = null;
            ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
