using MyAPI.Models;
using System.Linq.Expressions;

namespace MyAPI.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll(Expression<Func<T, bool>>? fillter = null,
            string? includProperties=null,
            int pageSize=3,int pageNum = 1);

        Task<T> Get(Expression<Func<T, bool>> fillter = null, bool tracked = true,
            string? includProperties = null);
        Task Create(T villa);
       
        Task Remove(T villa);
        Task Save();
    }
}
