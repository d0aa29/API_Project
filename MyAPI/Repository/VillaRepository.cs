using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyAPI.Data;
using MyAPI.Models;
using MyAPI.Repository.IRepository;
using System.Linq.Expressions;

namespace MyAPI.Repository
{
    public class VillaRepository : Repository<Villa>, IVillaRepository
    {
        private ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public VillaRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        //   _mapper = mapper;
        }

      
        public async Task<Villa> Update(Villa villa)
        {
            villa.UpdatedDate = DateTime.Now;
             _db.villas.Update(villa);
            await Save();
            return villa;
        }
    }
}
