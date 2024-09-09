using MyAPI.Models;
using System.Linq.Expressions;

namespace MyAPI.Repository.IRepository
{
    public interface IVillaNumberRepository : IRepository<VillaNumber>
    {
      
        Task<VillaNumber> Update( VillaNumber villa );
       

    }
}
