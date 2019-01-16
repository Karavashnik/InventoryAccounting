using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace InventoryAccounting.Models
{
    public class GenericDataRepository<T> : IGenericDataRepository<T> where T : class
    {
        protected readonly InventoryAccountingContext context;

        public GenericDataRepository(InventoryAccountingContext context)
        {
            this.context = context;
        }
        public virtual async Task<IList<T>> GetAllAsync(params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            
            IQueryable<T> dbQuery = context.Set<T>();

            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include(navigationProperty);
            list = await dbQuery
                .AsNoTracking()
                .ToListAsync();
            
            return list;
        }

        public virtual IList<T> GetList(Func<T, bool> where,
             params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            
            IQueryable<T> dbQuery = context.Set<T>();

            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            list = dbQuery
                .AsNoTracking()
                .AsEnumerable()
                .Where(where)
                .ToList<T>();
            
            return list;
        }

        public virtual async Task<T> GetSingleAsync(Expression<Func<T, bool>> where,
             params Expression<Func<T, object>>[] navigationProperties)
        {
            T item = null;
            
            IQueryable<T> dbQuery = context.Set<T>();

            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            item = await dbQuery
                .AsNoTracking()
                .SingleOrDefaultAsync(where); //Apply where clause
            
            return item;
        }

        public virtual async Task AddAsync(params T[] items)
        {
            foreach (T item in items)
            {
                context.Entry(item).State = EntityState.Added;
            }
            await context.SaveChangesAsync();
            
        }
        public virtual async Task UpdateAsync(params T[] items)
        {
            foreach (T item in items)
            {
                context.Entry(item).State = EntityState.Modified;
            }
            await context.SaveChangesAsync();
        }

        public virtual async Task RemoveAsync(params T[] items)
        {
            foreach (T item in items)
            {
                context.Entry(item).State = EntityState.Deleted;
            }
            await context.SaveChangesAsync();
            
        }

        public virtual async Task<bool> ItemExists(Expression<Func<T, bool>> where)
        {
            return await context.Set<T>().AnyAsync(where);
        }
    }
}
