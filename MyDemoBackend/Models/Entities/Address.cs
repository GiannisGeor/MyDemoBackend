using Models.Enums;

namespace Models.Entities
{
    public class Address : EntityBase
    {
        /// <summary>
        /// The Id of Customer
        /// </summary>
        public int? CustomerId { get; set; }
        /// <summary>
        /// The postalCode of the store or customer
        /// </summary>
        public int PostalCode { get; set; }

        /// <summary>
        /// The address and number of the store or customer
        /// </summary>
        public string FullAddress { get; set; }
        
        /// <summary>
        /// The floor that the customer lives on
        /// </summary>
        public string Floor { get; set; }

        /// <summary>
        /// The name on the Doorbell that the customer lives on
        /// </summary>
        public string DoorbellName { get; set; }

        /// <summary>
        /// Type of address
        /// </summary>
        public AddressType Type { get; set; }

        /// <summary>
        /// 1 to 1 connection from address to store
        /// </summary>
        public Store Store { get; set; }


        /// <summary>
        /// 1 to 1 connection from address to order
        /// </summary>
        public List<Order> Order { get; set; } = new List<Order>();

        /// <summary>
        /// 1 to many connection from 
        /// </summary>
        public Customer Customer { get; set; }
    }
}
