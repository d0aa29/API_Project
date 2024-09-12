using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Data;
using MyAPI.Models.Dto;
using MyAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using MyAPI.Repository.IRepository;
using System.Net;
using Azure;

namespace MyAPI.Controllers
{
   // [Route("api/VillaNumber")]
    [Route("api/v{version:apiVersion}/VillaNumber")]
    [ApiVersion("2.0")]
    [ApiController]
    public class VillaNumberV2Controller : ControllerBase
    {
       // private ApplicationDbContext _db;
        private readonly IVillaNumberRepository _dbvillaNumber;
        private readonly IVillaRepository _dbvilla;
        private readonly IMapper _mapper;
        private APIResponse _response;
        public VillaNumberV2Controller(IVillaNumberRepository dbvillaNumber, IVillaRepository dbvilla, IMapper mapper)
        {

            _dbvillaNumber = dbvillaNumber;
             _mapper = mapper;
            _dbvilla=dbvilla;
            this._response = new ();
        }

        [HttpGet("GetString")]
        public IEnumerable<string> Get()
        {
            return new string[] { "Version2Test", "VillaNumberV2Controller" };
        }

    }

}
