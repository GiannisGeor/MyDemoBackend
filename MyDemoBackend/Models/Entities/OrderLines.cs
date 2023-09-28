namespace Models.Entities
{
    public class OrderLines : EntityBase
    {
        /// <summary>
        /// The Id of the Product table
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// The Id of the Order table
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// The quantity of the OrderLines
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// The Product prices
        /// </summary>
        public decimal ProductPrice { get; set; }

        /// <summary>
        /// The comments of each order
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// 1 to many connection from Order to OrderLines table
        /// </summary>
        public Order Order { get; set; }

        /// <summary>
        /// Many to many connection from OrderLines to Product table
        /// </summary>
        public Product Product { get; set; }
    }
}
