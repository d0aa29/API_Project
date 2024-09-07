using AutoMapper;
using MyAPI.Models;
using MyAPI.Models.Dto;

namespace MyAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            // Map between Villa and VillaDTO
            CreateMap<Villa, VillaDTO>().ReverseMap();

            // Map between Villa and VillaCreateDTO
            CreateMap<Villa, VillaCreateDTO>().ReverseMap();

            // Map between Villa and VillaUpdateDTO
            CreateMap<Villa, VillaUpdateDTO>().ReverseMap();

            
        }
    }
    
}
