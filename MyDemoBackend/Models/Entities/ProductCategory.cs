namespace Models.Entities
{
    public class ProductCategory : EntityBase
    {
        /// <summary>
        /// The Id of the Store table
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// The names of the product categories
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 1 to many connection from ProductCategory to Product table
        /// </summary>
        public List<Product> Products { get; set; } = new List<Product>();

        /// <summary>
        /// 1 to many connection from Store to ProductCategory table
        /// </summary>
        public Store Store { get; set; }
    }
}
