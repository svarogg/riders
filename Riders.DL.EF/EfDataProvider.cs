using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riders.Common;

namespace Riders.DL.EF
{
    internal class EfDataProvider<T> : DataProvider<T> where T: class, IIdentifieable
    {
        private DbSet<T> DbSet { get; }
        private RidersContext Context { get; }

        public EfDataProvider(RidersContext context, DbSet<T> dbSet)
        {
            DbSet = dbSet;
            Context = context;
        }

        public override IQueryable<T> Query => DbSet.AsQueryable();

        protected override T Save(T obj)
        {
            DbSet.Add(obj);
            Context.SaveChanges();
            return obj;
        }

        protected override T Update(T obj)
        {
            Context.SaveChanges();
            return obj;
        }
    }
}
