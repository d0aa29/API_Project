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

namespace MyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
       // private ApplicationDbContext _db;
        private readonly IVillaRepository _dbvilla;
        private readonly IMapper _mapper;

        public VillaController(IVillaRepository dbvilla, IMapper mapper)
        {
        
            _dbvilla = dbvilla;
             _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVialla()
        {
            IEnumerable<Villa> villaList = await _dbvilla.GetAll(); 
            return Ok(_mapper.Map<List<VillaDTO>>(villaList));
        }

        [HttpGet("{id:int}", Name = ("GetViall"))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDTO>> GetViall(int id)
        {

            if (id == 0)
            {
                return BadRequest();
            }
            var villa = await _dbvilla.Get(x => x.Id == id);
            if (villa == null)
            {
                return BadRequest();
            }

            return Ok(_mapper.Map<VillaDTO>(villa));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDTO>> CreateViall([FromBody] VillaCreateDTO villaDTO)
        {
            if (villaDTO == null)
                return BadRequest();
            //if (villaDTO.Id > 0)
            //    return StatusCode(StatusCodes.Status500InternalServerError);
            if (await _dbvilla.Get(x => x.Name.ToLower() == villaDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("", "Vaill alredy exist");
                return BadRequest(ModelState);
            }
            //Villa model = new()
            //{
            //    //Id = villaDTO.Id,
            //    Name = villaDTO.Name,
            //    Amenity = villaDTO.Amenity,
            //    Details = villaDTO.Details,
            //    ImageUrl = villaDTO.ImageUrl,
            //    Occupancy = villaDTO.Occupancy,
            //    Rate = villaDTO.Rate,
            //    Sqft = villaDTO.Sqft

            //};
            Villa model = _mapper.Map<Villa>(villaDTO);

            await _dbvilla.Create(model);
           // await _dbvilla.Save();
            return CreatedAtRoute("GetViall", new { id = model.Id }, model);
          
        }
        [HttpDelete("{id:int}", Name = ("DeleteViall"))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDTO>> DeleteViall(int id)
        {
            if (id == null)
                return BadRequest();
           var vaill = await _dbvilla.Get
               (x => x.Id == id);
            if (vaill == null)
            {
                return BadRequest();
            }
            await _dbvilla.Remove(vaill);
           
            return NoContent();

        }
        [HttpPut("{id:int}", Name = ("UpdateViall"))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<VillaDTO>> UpdateViall(int id, [FromBody] VillaUpdateDTO villaDTOcs)
        {
            if (id != villaDTOcs.Id || villaDTOcs == null)
                return BadRequest();

      
            Villa model = _mapper.Map<Villa>(villaDTOcs);

          
            await _dbvilla.Update(model);
            return Ok(villaDTOcs);
        }

        [HttpPatch("{id:int}", Name = ("UpdatePartialViall"))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<VillaDTO> >UpdatePartialViall(int id, JsonPatchDocument<VillaUpdateDTO> patchDTO)
        {
            if (id == 0 || patchDTO == null)
                return BadRequest();
            var vaill = await _dbvilla.Get
              (x => x.Id == id,tracked:false);
            if (vaill == null)
                return BadRequest();

            //VillaUpdateDTO villaDTO = new()
            //{
            //    Id = vaill.Id,
            //    Name = vaill.Name,
            //    Amenity = vaill.Amenity,
            //    Details = vaill.Details,
            //    ImageUrl = vaill.ImageUrl,
            //    Occupancy = vaill.Occupancy,
            //    Rate = vaill.Rate,
            //    Sqft = vaill.Sqft

            //};
            VillaUpdateDTO villaDTO = _mapper.Map<VillaUpdateDTO>(vaill);

            patchDTO.ApplyTo(villaDTO , ModelState);
            //Villa model = new()
            //{
            //    Id = villaDTO.Id,
            //    Name = villaDTO.Name,
            //    Amenity = villaDTO.Amenity,
            //    Details = villaDTO.Details,
            //    ImageUrl = villaDTO.ImageUrl,
            //    Occupancy = villaDTO.Occupancy,
            //    Rate = villaDTO.Rate,
            //    Sqft = villaDTO.Sqft

            //};
            Villa model = _mapper.Map<Villa>(villaDTO);

            _dbvilla.Update(model);
       
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return NoContent();
        }

    }

}
