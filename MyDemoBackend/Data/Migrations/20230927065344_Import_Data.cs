using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ImportData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
                INSERT INTO Addresses(PostalCode, FullAddress, Phone, Type, isActive)
                VALUES
                (67100, 'Panormou 20', '6978654354', 0, 1),
                (67131, 'Skra 5', '6996554354', 0, 1),
                (67100, 'Perikleous 9', '6970286354', 0, 1);

                INSERT INTO StoreCategories(Name, IsActive)
                VALUES
                ('Cafe', 1),
                ('Gyradiko', 1),
                ('Pizzeria', 1);

                INSERT INTO Stores(StoreCategoryId, AddressId, Name, IsOpen, IsActive)
                VALUES
                (2, 1, 'Thraka', 1, 1),
                (3, 2, 'Giovanni', 1, 1),
                (1, 3, 'KOFI', 1, 1);

                INSERT INTO ProductCategories(StoreId, Name, IsActive)
                VALUES
                (1, 'Meat', 1),
                (2, 'Pizza', 1),
                (3, 'Coffee', 1);

                INSERT INTO Products(ProductCategoryId, Name, IsAvailable, Price, Description, IsActive)
                VALUES
                (1, 'Souvlaki', 1, 2.00, 'Pita with Souvlaki, Fries, Ketchup and Mustard', 1),
                (1, 'Gyros', 1, 2.00, 'Pita with Gyros, Fries, Ketchup and Mustard', 1),
                (2, 'Special Pizza', 1, 6.00, 'Pizza with Peppers, Mushrooms, Tomato sauce, Cheese and Ham', 1),
                (2, 'BBQ Pizza', 1, 6.00, 'Pizza with Bacon, tomato, BBQ sauce, Cheese, Eggs, Chicken ', 1),
                (3, 'Freddo Espresso', 1, 1.50, 'Freddo Espresso with no sugar', 1),
                (3, 'Cappuccino', 1, 1.50, 'Cappuccino with no sugar', 1);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
