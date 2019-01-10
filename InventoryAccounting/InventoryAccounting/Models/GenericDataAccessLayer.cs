using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace InventoryAccounting.Models
{
    public class GenericDataAccessLayer<T> : IGenericDataAccessLayer<T> where T : class
    {
        private readonly InventoryAccountingContext context;

        public GenericDataAccessLayer(InventoryAccountingContext context)
        {
            this.context = context;
        }
        public virtual async Task<IList<T>> GetAllAsync(params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            
            IQueryable<T> dbQuery = context.Set<T>();

            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            list = await dbQuery
                .AsNoTracking()
                .ToListAsync<T>();
            
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

        public virtual T GetSingle(Func<T, bool> where,
             params Expression<Func<T, object>>[] navigationProperties)
        {
            T item = null;
            
            IQueryable<T> dbQuery = context.Set<T>();

            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            item = dbQuery
                .AsNoTracking() //Don't track any changes for the selected item
                .FirstOrDefault(where); //Apply where clause
            
            return item;
        }

        public virtual async void AddAsync(params T[] items)
        {
            foreach (T item in items)
            {
                context.Entry(item).State = EntityState.Added;
            }
            await context.SaveChangesAsync();
            
        }
        public virtual async void UpdateAsync(params T[] items)
        {
            foreach (T item in items)
            {
                context.Entry(item).State = EntityState.Modified;
            }
            await context.SaveChangesAsync();
            
        }

        public virtual async void RemoveAsync(params T[] items)
        {
            foreach (T item in items)
            {
                context.Entry(item).State = EntityState.Deleted;
            }
            await context.SaveChangesAsync();
            
        }
    }
}
