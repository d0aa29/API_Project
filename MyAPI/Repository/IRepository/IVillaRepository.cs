using MyAPI.Models;
using System.Linq.Expressions;

namespace MyAPI.Repository.IRepository
{
    public interface IVillaRepository : IRepository<Villa>
    {
      
        Task<Villa> Update( Villa villa );
       

    }
}
