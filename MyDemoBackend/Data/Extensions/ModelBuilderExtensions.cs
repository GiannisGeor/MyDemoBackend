using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.Interfaces;

namespace Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Sets the appropriate HasQueryFilter ( .HasQueryFilter(x => x.DeletedBy == null && x.Deleted == null); )
        /// to all the entities that implements the ISoftDeletable Interface
        /// This will exlude Deleted Entries on all queries (Also works when we include a table).
        /// If you want for some reason to bypass this (because for example you need the deleted entries to be returned from the query)
        /// you need to use : .IgnoreQueryFilters() on your repository query.
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void SetQueryFilterToByDefaultExcludeDeletedEntries(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType))
                {
                    // Construct the predicate to add the following where clause :
                    // .where(softDeletableEntity => softDeletableEntity.DeletedBy == null && softDeletableEntity.Deleted == null)
                    var parameter = Expression.Parameter(entityType.ClrType, "softDeletableEntity");
                    var propertyDeleted = Expression.Property(parameter, "Deleted");
                    var propertyDeletedBy = Expression.Property(parameter, "DeletedBy");
                    var nullConstant = Expression.Constant(null);
                    var predicate = Expression.Lambda(
                        Expression.AndAlso(
                            Expression.Equal(propertyDeleted, nullConstant),
                            Expression.Equal(propertyDeletedBy, nullConstant)),
                        parameter);

                    // Add the predicate using HasQueryFilter on the entity
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(predicate);
                }
            }
        }
    }
}
