using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class OrderLinesOptions : EntityBase
    {
        // <summary>
        /// The Id of the OrderLine table
        /// </summary>
        public int OrderLineId { get; set; }

        /// <summary>
        /// The Id of the Options table
        /// </summary>
        public int OptionsId { get; set; }

        /// <summary>
        /// The Extra Cost of each option
        /// </summary>
        public decimal OptionExtraCost { get; set; }

        /// <summary>
        /// 1 to many connection to OrderLines table
        /// </summary>
        public OrderLines OrderLines { get; set; }

        public Options Options { get; set; }
    }
}
