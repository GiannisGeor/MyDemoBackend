using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dtos
{
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
