using Data.Interfaces;
using Messages.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories
{
    /// <summary>
    /// Genrerischer Repository für CRUD-Operationen
    /// </summary>
    public class GenericRepository : IGenericRepository
    {
        private DemoBackendDbContext context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public GenericRepository(DemoBackendDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Persists an object to the data store.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="candidate">The entity to be persisted.</param>
        /// <returns>The persisted entity.</returns>
        public async Task<T> Add<T>(T candidate) where T : class
        {
            return await AddOrUpdate(candidate, false);
        }

        public async Task AddRange<T>(List<T> candidates) where T : class
        {
            context.AddRange(candidates);
            await context.SaveChangesAsync();
        }

        public async Task<int> Count<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            int count = await context.Set<T>().CountAsync(predicate);
            return count;
        }

        public async Task Delete<T>(T candidate) where T : class
        {
            if (candidate == null)
            {
                return;
            }

            context.Attach(candidate);
            context.Entry(candidate).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }

        public async Task Delete<T>(int id) where T : class
        {
            T entity = context.Find<T>(id);
            if (entity != null)
            {
                await Delete(entity);
            }
        }

        public async Task DeleteRange<T>(List<T> candidates) where T : class
        {
            context.RemoveRange(candidates);
            await context.SaveChangesAsync();
        }

        public async Task<bool> Exists<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            int count = await Count<T>(predicate);
            return count > 0;
        }

        public async Task<T> First<T>(Expression<Func<T, bool>> predicate, params string[] includes) where T : class
        {
            DbSet<T> set = context.Set<T>();
            IQueryable<T> query = AttachIncludes(set, includes);
            T result = await query.FirstOrDefaultAsync(predicate);
            return result;
        }

        public async Task<int[]> GetIds<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            IQueryable<T> set = context.Set<T>().AsNoTracking();
            return await set.Where(predicate).Select(x => (int)x.GetType().GetProperty("Id").GetValue(x)).ToArrayAsync();
        }

        public async Task<List<T>> Get<T>(Expression<Func<T, bool>> predicate, params string[] includes) where T : class
        {
            IQueryable<T> set = context.Set<T>().AsNoTracking();
            IQueryable<T> query = AttachIncludes(set, includes);


            List<T> result = await query.Where(predicate).ToListAsync();
            return result;
        }


        public async Task<List<T>> Get<T>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> orderByFunc,
            Expression<Func<T, object>> orderByDescendingFunc,
            params string[] includes) where T : class
        {
            IQueryable<T> query = PrepareQuery(predicate, (orderByFunc ?? orderByDescendingFunc), (orderByFunc != null ? SortOrder.Asc : SortOrder.Desc), -1, -1, includes);

            List<T> result = await query.ToListAsync();
            return result;
        }

        public async Task<List<T>> Get<T>(Expression<Func<T, bool>> predicate,
            int skip,
            int take,
            Expression<Func<T, object>> orderByFunc,
            Expression<Func<T, object>> orderByDescendingFunc,
            params string[] includes) where T : class
        {
            IQueryable<T> query = PrepareQuery(predicate, (orderByFunc ?? orderByDescendingFunc), (orderByFunc != null ? SortOrder.Asc : SortOrder.Desc), take, skip, includes);

            List<T> result = await query.ToListAsync();
            return result;
        }

        public async Task<T> Single<T>(Expression<Func<T, bool>> predicate, params string[] includes) where T : class
        {

            IQueryable<T> set = context.Set<T>();
            IQueryable<T> query = AttachPredicate(set, predicate);
            query = AttachIncludes(query, includes);
            T result = await query.SingleOrDefaultAsync(predicate);
            return result;
        }

        public async Task<List<TResult>> Select<T, TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> predicate) where T : class
        {
            IQueryable<T> set = context.Set<T>().AsNoTracking();
            IQueryable<T> query = AttachPredicate(set, predicate);
            return await query.Select(selector).ToListAsync();
        }

        public async Task<T> Update<T>(T candidate, bool forceUpdate = false) where T : class
        {
            return await AddOrUpdate(candidate, forceUpdate);
        }

        private async Task<T> AddOrUpdate<T>(T candidate, bool forceUpdate) where T : class
        {
            int id = 0;
            try
            {
                var propertyInfo = candidate.GetType().GetProperty("Id");
                if (propertyInfo != null)
                {
                    var value = (int)propertyInfo.GetValue(candidate);
                    id = value;
                }
            }
            catch (Exception)
            {
                // Todo: Dieser Fehler muss zumindest geloggt werden. Ist eine weitere Behandlung nötig?
            }

            if (id == 0 && !forceUpdate)
            {
                context.Add(candidate);
                await context.SaveChangesAsync().ConfigureAwait(false);
            }
            else
            {
                if (context.Entry(candidate).State == EntityState.Modified)
                {
                    // Candidate is already tracked by EF --> SaveChanges is enough
                    await context.SaveChangesAsync().ConfigureAwait(false);
                }
                else
                {
                    context.Update(candidate);
                    await context.SaveChangesAsync().ConfigureAwait(false);
                }
            }

            return candidate;
        }

        public IQueryable<T> AsQueryable<T>() where T : class
        {
            IQueryable<T> set = context.Set<T>().AsQueryable();
            return set;
        }
        #region HELPERS

        private IQueryable<T> PrepareQuery<T>(Expression<Func<T, bool>> predicate = null,
            Expression<Func<T, object>> orderBy = null,
            SortOrder sortOrder = SortOrder.Asc,
            int take = -1,
            int skip = -1,
            params string[] include) where T : class
        {
            IQueryable<T> set = context.Set<T>().AsNoTracking();
            IQueryable<T> query = AttachIncludes(set, include);
            query = AttachPredicate(query, predicate);
            query = AttachOrderByFunc(query, orderBy, sortOrder);
            query = AttachPaging(query, take, skip);
            return query;
        }

        private IQueryable<T> AttachPredicate<T>(IQueryable<T> query, Expression<Func<T, bool>> predicate) where T : class
        {
            return predicate != null ? query.Where(predicate) : query;
        }

        private IQueryable<T> AttachIncludes<T>(IQueryable<T> query, params string[] include) where T : class
        {

            if (include == null || include.Length <= 0)
            {
                return query;
            }

            int length = include.Length;
            for (int i = 0; i < length; i++)
            {
                query = query.Include(include[i]);
            }


            return query;
        }

        private IQueryable<T> AttachOrderByFunc<T>(IQueryable<T> query, Expression<Func<T, object>> orderBy, SortOrder sortOrder) where T : class
        {
            if (orderBy != null)
            {
                query = sortOrder == SortOrder.Asc ? query.OrderBy(orderBy).AsQueryable() : query.OrderByDescending(orderBy).AsQueryable();
            }

            return query;
        }

        private IQueryable<T> AttachPaging<T>(IQueryable<T> query, int take, int skip)
        {
            return take < 0 || skip < 0 ? query : query.Skip(skip).Take(take);
        }


        #endregion
    }
}
