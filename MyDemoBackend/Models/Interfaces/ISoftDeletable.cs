using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces
{
    public interface ISoftDeletable
    {
        /// <summary>
        /// User who last deleted the entity. Defaults to 'Application'.
        /// </summary>
        public string DeletedBy { get; set; }

        /// <summary>
        /// Entity's deletion date. Defaults to DateTime.Now.
        /// </summary>
        public DateTime? Deleted { get; set; }
    }
}
