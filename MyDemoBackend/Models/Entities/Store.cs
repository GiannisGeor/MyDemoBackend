namespace Models.Entities
{
    public class Store : EntityBase
    {
        /// <summary>
        /// The Id of the StoreCategory table
        /// </summary>
        public int StoreCategoryId { get; set; }

        /// <summary>
        /// The Id of the Address table
        /// </summary>
        public int AddressId { get; set; }

        /// <summary>
        /// The names of the stores 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A bool to that indicates if a store is open or not
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// 1 to 1 connection to Address table
        /// </summary>
        public Address Address { get; set; } = new Address();

        /// <summary>
        /// 1 to 1 connection to StoreCategory table
        /// </summary>
        public StoreCategory StoreCategory { get; set; } = new StoreCategory();

        /// <summary>
        /// 1 to many connection to Order table
        /// </summary>
        public List<Order> Orders { get; set; } = new List<Order>();

        /// <summary>
        /// 1 to many connection to ProductCategory table
        /// </summary>
        public List<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
    }
}
