using Models.Enums;

namespace Models.Entities
{
    public class Order : EntityBase
    {
        /// <summary>
        /// The Id of the Store table
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// The Id of the Address table
        /// </summary>
        public int AddressId { get; set; }

        /// <summary>
        /// Status of the order
        /// </summary>
        public OrderStatus orderStatus { get; set; }


        /// <summary>
        /// 1 to many connection to OrderLines table
        /// </summary>
        public List<OrderLines> OrderLines { get; set; } = new List<OrderLines>();

        /// <summary>
        /// 1 to many connection from Store to Order table
        /// </summary>
        public Store Store { get; set; }

        /// <summary>
        /// 1 to 1 connection to Address table
        /// </summary>
        public Address Address { get; set; }
    }
}
