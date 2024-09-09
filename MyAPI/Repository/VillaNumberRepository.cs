using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyAPI.Data;
using MyAPI.Models;
using MyAPI.Repository.IRepository;
using System.Linq.Expressions;

namespace MyAPI.Repository
{
    public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
    {
        private ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public VillaNumberRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        //   _mapper = mapper;
        }

      
        public async Task<VillaNumber> Update(VillaNumber villa)
        {
            villa.UpdatedDate = DateTime.Now;
             _db.villaNumbers.Update(villa);
            await Save();
            return villa;
        }
    }
}
