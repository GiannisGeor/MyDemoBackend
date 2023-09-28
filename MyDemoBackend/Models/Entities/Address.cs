using Models.Enums;

namespace Models.Entities
{
    public class Address : EntityBase
    {
        /// <summary>
        /// The postalCode of the store or customer
        /// </summary>
        public int PostalCode { get; set; }

        /// <summary>
        /// The address and number of the store or customer
        /// </summary>
        public string FullAddress { get; set; }

        /// <summary>
        /// The Phone the store or customer
        /// </summary>
        public string Phone { get; set; }

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
        public Order Order { get; set; }
    }
}
