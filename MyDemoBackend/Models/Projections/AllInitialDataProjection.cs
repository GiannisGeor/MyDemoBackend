using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Entities;
using Models.Enums;

namespace Models.Projections
{
    public class AllInitialDataProjection
    {
        /// <summary>
        /// The Id of the stores
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The names of the stores 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A bool to that indicates if a store is open or not
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// 1to1 connection to Address table
        /// </summary>
        public AddressProjection Address { get; set; } = new AddressProjection();

        /// <summary>
        /// 1to1 connection to StoreCategory table
        /// </summary>
        public StoreCategoryProjection StoreCategory { get; set; } = new StoreCategoryProjection();

        /// <summary>
        /// 1 to many connection to ProductCategory table
        /// </summary>
        public List<ProductCategoryProjection> ProductCategories { get; set; } = new List<ProductCategoryProjection>();
    }

    public class ProductCategoryProjection
    {
        /// <summary>
        /// The Id of the Product Categories
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The names of the product categories
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 1 to many connection from ProductCategory to Product table
        /// </summary>
        public List<ProductProjection> Products { get; set; } = new List<ProductProjection>();
    }

    public class ProductProjection
    {
        /// <summary>
        /// The Id of the Products
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The names of the products
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A bool that indicates if a product is available or not
        /// </summary>
        public bool IsAvailable { get; set; }

        /// <summary>
        /// the prices of the products
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The descriptions of the products
        /// </summary>
        public string Description { get; set; }
    }

}

