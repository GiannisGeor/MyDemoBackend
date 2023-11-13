using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Enums;

namespace Services.Dtos
{
    public class NewOrderResponseDto
    {
        public int Id { get; set; }

        /// <summary>
        /// The Id of the Store table
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// The comments of each order
        /// </summary>
        public string OrderComments { get; set; }        

        /// <summary>
        /// 1 to many connection to OrderLines table
        /// </summary>
        public List<NewOrderLinesResponseDto> OrderLines { get; set; } = new List<NewOrderLinesResponseDto>();

    }

    public class NewOrderLinesResponseDto
    {
        /// <summary>
        /// The Id of the Product table
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// The quantity of the OrderLines
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// The comments of each product
        /// </summary>
        public string Comments { get; set; }
      
    }

    public class AddressResponseDto
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
}

