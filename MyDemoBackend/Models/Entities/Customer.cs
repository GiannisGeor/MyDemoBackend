using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Entities.Auth;

namespace Models.Entities
{
    public class Customer : EntityBase
    {
        /// <summary>
        /// The name of the customer
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The phone of the customer
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// The Email of the customer
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 1 to many connection to Address table
        /// </summary>
        public List<Address> Addresses { get; set; } = new List<Address>();

        /// <summary>
        /// 1 to many connection to Order table
        /// </summary>
        public List<Order> Orders { get; set; } = new List<Order>();

    }
}
