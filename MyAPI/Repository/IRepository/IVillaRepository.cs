using MyAPI.Models;
using System.Linq.Expressions;

namespace MyAPI.Repository.IRepository
{
    public interface IVillaRepository
    {
        Task<List<Villa>> GetAll(Expression<Func<Villa,bool>> fillter =null );

        Task<Villa> Get(Expression<Func<Villa, bool>> fillter = null,bool tracked=true);
        Task Create( Villa villa);
        Task Update(Villa villa);
        Task Remove( Villa villa );
        Task Save();
      //  Task<Villa> Delete( Villa villa );

    }
}
