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

            // Map between VillaNumber and VillaNumberDTO
            CreateMap<VillaNumber, VillaNumberDTO>().ReverseMap();

            // Map between VillaNumber and VillaNumberCreateDTO
            CreateMap<VillaNumber, VillaNumberCreateDTO>().ReverseMap();

            // Map between VillaNumber and VillaNumberUpdateDTO
            CreateMap<VillaNumber, VillaNumberUpdateDTO>().ReverseMap();

            CreateMap<ApplicationUser, UserDTO>().ReverseMap();


        }
    }
    
}
