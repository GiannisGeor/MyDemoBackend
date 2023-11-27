using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class ProductOptionsGroup : EntityBase
    {
        /// <summary>
        /// The Id of the Product table
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// The Id of the OptionsGroup table
        /// </summary>
        public int OptionsGroupId { get; set; }

        /// <summary>
        /// 1 to many connection from product to ProductOptionsGroup table
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// 1 to many connection from OptionsGroup to ProductOptionsGroup table
        /// </summary>
        public OptionsGroup OptionsGroup { get; set; }

        /// <summary>
        /// 1 to many connection from Options to ProductOptionsGroup table
        /// </summary>
        public List<Options> Options { get; set; } = new List<Options> { }; 
    }
}
