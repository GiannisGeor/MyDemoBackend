using System.Collections.Generic;

namespace Models.Entities
{
    public class StoreCategory : EntityBase
    {
        /// <summary>
        /// The names of the store categories
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 1 to 1 connection from StoreCategory to Store table
        /// </summary>
        public Store Store { get; set; }
    }
}

