using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InventoryAccounting.Models
{
    public interface IGenericDataAccessLayer<T> where T : class
    {
        Task<IList<T>> GetAllAsync(params Expression<Func<T, object>>[] navigationProperties);
        IList<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        void AddAsync(params T[] items);
        void UpdateAsync(params T[] items);
        void RemoveAsync(params T[] items);
    }
}
