using Models.Enums;

namespace Services.Dtos
{
    public class StoreStoreCategoryAddressDto
    {
        /// <summary>
        /// The Id of the stores
        /// </summary>
        public string Id { get; set; }

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
        public AddressDto Address { get; set; }

        /// <summary>
        /// 1to1 connection to StoreCategory table
        /// </summary>
        public StoreCategoryDto StoreCategory { get; set; }
    }

    public class AddressDto
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
        /// The floor that the customer lives on
        /// </summary>
        public string Floor { get; set; }

        /// <summary>
        /// The name on the Doorbell that the customer lives on
        /// </summary>
        public string DoorbellName { get; set; }


    }

    public class StoreCategoryDto
    {
        /// <summary>
        /// The name of the Store Category
        /// </summary>
        public string Name { get; set; }

    }
}
