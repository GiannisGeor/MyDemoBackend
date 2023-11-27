using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class BaseOptions : EntityBase
    {
        /// <summary>
        /// The id of the store table
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// The names of the BaseOptions 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A bool to that indicates if a BaseOption is available or not
        /// </summary>
        public bool IsAvailable { get; set; }

        /// <summary>
        /// 1 to many connection from store to BaseOptions table
        /// </summary>
        public Store Store { get; set; }

        /// <summary>
        /// 1 to many connection to Options table
        /// </summary>
        public List<Options> Options { get; set; } = new List<Options>();
    }
}
