using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Options : EntityBase
    {
        // <summary>
        /// The Id of the ProductOptionsGroup table
        /// </summary>
        public int ProductOptionsGroupId { get; set; }

        /// <summary>
        /// The Id of the BaseOptions table
        /// </summary>
        public int BaseOptionsId { get; set; }

        /// <summary>
        /// The Extra Cost of each option
        /// </summary>
        public decimal ExtraCost { get; set; }

        /// <summary>
        /// 1 to many connection to OptionsGroup table
        /// </summary>
        public List<OrderLinesOptions> OrderLinesOptions { get; set; } = new List<OrderLinesOptions>();

        /// <summary>
        /// 1 to many connection to BaseOptions table
        /// </summary>
        public BaseOptions BaseOptions { get; set; } 

        /// <summary>
        /// 1 to many connection to ProductOptionsGroup table
        /// </summary>
        public ProductOptionsGroup ProductOptionsGroup { get; set; }
    }
}
