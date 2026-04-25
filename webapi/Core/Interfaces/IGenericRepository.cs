using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity 
    {
        Task<IReadOnlyList<T>> GetDataAsync();
        Task<T> GetDataByIdAsync(int id);
        Task AddDataObj(T DataObj);
        void DeleteObj(T DataObj);
        bool DataExists(int id);
        Task<T> UpdateObj(T DataObj);
        Task<bool> saveChanges();
        Task<IReadOnlyList<T>> GetDataWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<TResult>> GetDataWithSpec<TResult>(ISpecification<T, TResult> spec);

    }
}
