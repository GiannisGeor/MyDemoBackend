using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class OptionsGroup : EntityBase
    {
        // <summary>
        /// The Id of the Store table
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// The names of the Options 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A bool to that indicates if an OptionGroup can be multiselected or not
        /// </summary>
        public bool IsMulti { get; set; }

        /// <summary>
        /// 1 to many connection to Product table
        /// </summary>
        public Store Store { get; set; }

        /// <summary>
        /// 1 to many connection to Options table
        /// </summary>
        public List<Options> Options { get; set; } = new List<Options>();

        /// <summary>
        /// Many to mnany connection from OptionsGroup to productOptionsGroups table
        /// </summary>
        public List<ProductOptionsGroup> productOptionsGroups { get; set; } = new List<ProductOptionsGroup> { };
    }
}
