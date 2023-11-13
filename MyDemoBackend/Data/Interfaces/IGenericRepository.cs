using System.Linq.Expressions;

namespace Data.Interfaces
{
    /// <summary>
    /// Generic repository used for CRUD operations an a data store for whichever entity.
    /// </summary>
    public interface IGenericRepository
    {
        /// <summary>
        /// Fetches the entities from the data store according to the predicate and includes the complex types supplied as a string array ind the form 'Child', 'Children', 'Children.Grandchildren'
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">The predicated expression used in the Where method</param>
        /// <param name="includes">The complex types to be included.</param>
        /// <returns></returns>
        Task<List<T>> Get<T>(Expression<Func<T, bool>> predicate, params string[] includes) where T : class;

        /// <summary>
        /// Fetches the entities from the data store according to the predicate and includes the complex types supplied as a string array ind the form 'Child', 'Children', 'Children.Grandchildren'
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">The predicated expression used in the Where method</param>
        /// <param name="orderByFunc">The sort function to be used ascending.</param>
        /// <param name="orderByDescendingFunc">The sort function to be used descending.</param>
        /// <param name="includes">The complex types to be included.</param>
        /// <returns></returns>
        Task<List<T>> Get<T>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> orderByFunc,
            Expression<Func<T, object>> orderByDescendingFunc,
            params string[] includes) where T : class;


        /// <summary>
        /// Fetches the entities from the data store according to the predicate and includes the complex types supplied as a string array ind the form 'Child', 'Children', 'Children.Grandchildren'
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="skip">The number of entities to be skipped.</param>
        /// <param name="take">The number of entities to be fetched.</param>
        /// <param name="predicate">The predicated expression used in the Where method</param>
        /// <param name="orderByFunc">The sort function to be used ascending.</param>
        /// <param name="orderByDescendingFunc">The sort function to be used descending.</param>
        /// <param name="includes">The complex types to be included.</param>
        /// <returns></returns>
        Task<List<T>> Get<T>(Expression<Func<T, bool>> predicate,
            int skip,
            int take,
            Expression<Func<T, object>> orderByFunc,
            Expression<Func<T, object>> orderByDescendingFunc,
            params string[] includes) where T : class;

        /// <summary>
        /// Get the ids of the objects
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">The predicated expression used in the Where method</param>
        /// <returns></returns>
        Task<int[]> GetIds<T>(Expression<Func<T, bool>> predicate) where T : class;

        /// <summary>
        /// Fetches the single object from the data store. The predicated supplied must ensure there is not more than one entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">The predicated expression used in the Where method</param>
        /// <param name="includes">The complex types to be included.</param>
        /// <returns></returns>
        Task<T> Single<T>(Expression<Func<T, bool>> predicate, params string[] includes) where T : class;

        /// <summary>
        /// Fetches the first object from the data store. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">The predicated expression used in the Where method</param>
        /// <param name="includes">The complex types to be included.</param>
        /// <returns></returns>
        Task<T> First<T>(Expression<Func<T, bool>> predicate, params string[] includes) where T : class;

        /// <summary>
        /// Persists an object to the data store.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="candidate">The entity to be persisted.</param>
        /// <returns>The persisted entity.</returns>
        Task<T> Add<T>(T candidate) where T : class;

        /// <summary>
        /// Persists a range of objects to the data store.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="candidates">The list containing the entities to be persisted.</param>
        /// <returns></returns>
        Task AddRange<T>(List<T> candidates) where T : class;

        /// <summary>
        /// Update the entity on the data store. All properties will be updated, including complex types.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="candidate">The entity to be updated.</param>
        /// <returns>The updated entity.</returns>
        Task<T> Update<T>(T candidate, bool forceUpdate = false) where T : class;

        /// <summary>
        /// Deletes the entity from the data store.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="candidate">The entity to be deleted.</param>
        /// <returns></returns>
        Task Delete<T>(T candidate) where T : class;

        /// <summary>
        /// Deletes the entity from the data store which has the supplied Id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The id of the entity to be deleted.</param>
        /// <returns></returns>
        Task Delete<T>(int id) where T : class;

        /// <summary>
        /// Deletes a list of entities from the data store which has the supplied Id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The list of entities to be deleted.</param>
        /// <returns></returns>
        Task DeleteRange<T>(List<T> candidates) where T : class;

        /// <summary>
        /// Count the number of entities in the data store matching the provided predicate expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">The predicate expression.</param>
        /// <returns></returns>
        Task<int> Count<T>(Expression<Func<T, bool>> predicate) where T : class;

        /// <summary>
        /// Ckecks if entities matching the predicate expression exist in the data store.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">The predicate expression.</param>
        /// <returns></returns>
        Task<bool> Exists<T>(Expression<Func<T, bool>> predicate) where T : class;

        Task<List<TResult>> Select<T, TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> predicate) where T : class;


        IQueryable<T> AsQueryable<T>() where T : class;
    }
}
