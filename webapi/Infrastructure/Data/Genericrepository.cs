using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data
{
    public class Genericrepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _storeContext;
        public Genericrepository(StoreContext storeContext)
        {
            this._storeContext = storeContext;
        }

        async Task<IReadOnlyList<T>> IGenericRepository<T>.GetDataAsync()
        {
            return await _storeContext.Set<T>().ToListAsync<T>();
        }
        async Task<T> IGenericRepository<T>.GetDataByIdAsync(int id)
        {
            return await _storeContext.Set<T>().FindAsync(id);
        }

        async Task<IReadOnlyList<T>> IGenericRepository<T>.GetDataWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecifications(spec).ToListAsync();
        }

        async Task IGenericRepository<T>.AddDataObj(T DataObj)
        {
            await _storeContext.Set<T>().AddAsync(DataObj);
        }

        void IGenericRepository<T>.DeleteObj(T DataObj)
        {
             _storeContext.Set<T>().Remove(DataObj);
        }
        bool IGenericRepository<T>.DataExists(int id)
        {
            return _storeContext.Set<T>().Any(p => p.Id == id);
        }
        async Task<bool> IGenericRepository<T>.saveChanges()
        {
            return await _storeContext.SaveChangesAsync() > 0;
        }

        async Task<T> IGenericRepository<T>.UpdateObj(T DataObj)
        {
            _storeContext.Entry(DataObj).State = EntityState.Modified;
            await _storeContext.SaveChangesAsync();
            return DataObj;
        }

        async Task<IReadOnlyList<TResult>> IGenericRepository<T>.GetDataWithSpec<TResult>(ISpecification<T,TResult> spec)
        {
            return await ApplySpecifications(spec).ToListAsync();
        }
        private IQueryable<TResult> ApplySpecifications<TResult>(ISpecification<T, TResult> spec)
        {
            return SpecificationEvaluator<T>.GetQuery<T, TResult>(_storeContext.Set<T>().AsQueryable(), spec);
        }

        private IQueryable<T> ApplySpecifications(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_storeContext.Set<T>().AsQueryable(), spec);
        }
    }
}
