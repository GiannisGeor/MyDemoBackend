using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Configuration;
using Models.Interfaces;

namespace Models.Entities
{
    public class EntityBase : ISoftDeletable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public bool IsActive { get; set; }

        public string? DeletedBy { get; set; }

        public DateTime? Deleted { get; set; }

        /// <summary>
        /// User who created the entity. Defaults to 'Application'.
        /// </summary>
        public string? CreatedBy { get; set; } = GlobalConstants.Database.DefaultMarkUser;

        /// <summary>
        /// Entity's creation date. Defaults to DateTime.Now.
        /// </summary>
        public DateTime? Created { get; set; } = DateTime.Now;

        public string? ModifiedBy { get; set; }

        public DateTime? Modified { get; set; }


        /// <summary>
        /// Marks the entity as being new setting Id to '0', Created and Modified to 'DateTime.Now' as well as CreatedBy and ModifiedBy to the user set in the parameter.
        /// </summary>
        /// <param name="user">The user currently logged.</param>
        public void MarkNew(string user = GlobalConstants.Database.DefaultMarkUser)
        {
            Id = 0;
            CreatedBy = user;
            Created = DateTime.Now;
            IsActive = true;
            MarkDirty(user);
        }

        /// <summary>
        /// Marks the entity as having been modified setting Modified to 'DateTime.Now' and ModifiedBy to the user set in the parameter.
        /// </summary>
        /// <param name="user">The user currently logged.</param>
        public void MarkDirty(string user = GlobalConstants.Database.DefaultMarkUser)
        {
            ModifiedBy = user;
            Modified = DateTime.Now;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        public void MarkDeleted(string user = GlobalConstants.Database.DefaultMarkUser)
        {
            DeletedBy = user;
            Deleted = DateTime.Now;
        }
        
    }
}
