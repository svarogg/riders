using System.Linq;

namespace Riders.Common
{
    public abstract class DataProvider<T> where T : IIdentifieable
    {
        /// <summary>
        /// Returns a Queryable object to query all objects in this data set.
        /// </summary>
        public abstract IQueryable<T> Query { get; }

        /// <summary>
        /// Saves or updates given object to the database
        /// </summary>
        /// <param name="obj">The object to save</param>
        /// <returns>The same object</returns>
        public T SaveOrUpdate(T obj)
        {
            return obj.Id.HasValue
                ? Update(obj)
                : Save(obj);
        }
        
        protected abstract T Save(T obj);

        protected abstract T Update(T obj);
    }
}
