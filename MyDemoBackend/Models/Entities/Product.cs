namespace Models.Entities
{
    public class Product : EntityBase
    {
        /// <summary>
        /// The Id of the Product table
        /// </summary>
        public int ProductCategoryId { get; set; }

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

        /// <summary>
        /// Many to many connection from Product to OrderLines table
        /// </summary>
        public List<OrderLines> OrderLines { get; set; } = new List<OrderLines>();

        /// <summary>
        /// 1 to 1 connection from ProductCategory to Product table
        /// </summary>
        public ProductCategory ProductCategory { get; set; }

        /// <summary>
        /// Many to many connection from Product to ProductOptionsGroup table
        /// </summary>
        public List<ProductOptionsGroup> ProductOptionsGroups { get; set; } = new List<ProductOptionsGroup> { };
    }
}
