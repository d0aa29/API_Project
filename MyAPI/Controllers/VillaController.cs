using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Data;
using MyAPI.Models.Dto;
using MyAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace MyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private ApplicationDbContext _db;

        public VillaController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVialla()
        {
            return Ok(_db.villas.ToList());
        }

        [HttpGet("{id:int}", Name = ("GetViall"))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> GetViall(int id)
        {

            if (id == 0)
            {
                return BadRequest();
            }
            var vaill = _db.villas.FirstOrDefault
                (x => x.Id == id);
            if (vaill == null)
            {
                return BadRequest();
            }

            return Ok(vaill);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDTO> CrateViall([FromBody] VillaDTO villaDTOcs)
        {
            if (villaDTOcs == null)
                return BadRequest();
            if (villaDTOcs.Id > 0)
                return StatusCode(StatusCodes.Status500InternalServerError);
            if (_db.villas.FirstOrDefault(x => x.Name.ToLower() == villaDTOcs.Name.ToLower()) != null)
            {
                ModelState.AddModelError("", "Vaill alredy exist");
                return BadRequest(ModelState);
            }
            Villa model = new()
            {
                Id = villaDTOcs.Id,
                Name = villaDTOcs.Name,
                Amenity = villaDTOcs.Amenity,
                Details = villaDTOcs.Details,
                ImageUrl = villaDTOcs.ImageUrl,
                Occupancy = villaDTOcs.Occupancy,
                Rate = villaDTOcs.Rate,
                Sqft = villaDTOcs.Sqft

            };
            _db.villas.Add(model);
            _db.SaveChanges();
            return Ok(villaDTOcs);
        }
        [HttpDelete("{id:int}", Name = ("DeleteViall"))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDTO> DeleteViall(int id)
        {
            if (id == null)
                return BadRequest();
            var vaill = _db.villas.FirstOrDefault
               (x => x.Id == id);
            if (vaill == null)
            {
                return BadRequest();
            }
            _db.villas.Remove(vaill);
            _db.SaveChanges();
            return NoContent();

        }
        [HttpPut("{id:int}", Name = ("UpdateViall"))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<VillaDTO> UpdateViall(int id, [FromBody] VillaDTO villaDTOcs)
        {
            if (id != villaDTOcs.Id || villaDTOcs == null)
                return BadRequest();
           
            Villa model = new()
            {
                Id = villaDTOcs.Id,
                Name = villaDTOcs.Name,
                Amenity = villaDTOcs.Amenity,
                Details = villaDTOcs.Details,
                ImageUrl = villaDTOcs.ImageUrl,
                Occupancy = villaDTOcs.Occupancy,
                Rate = villaDTOcs.Rate,
                Sqft = villaDTOcs.Sqft

            };
            _db.villas.Update(model);
            _db.SaveChanges();
            return Ok(villaDTOcs);
        }

        [HttpPatch("{id:int}", Name = ("UpdatePartialViall"))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<VillaDTO> UpdatePartialViall(int id, JsonPatchDocument<VillaDTO> patchDTO)
        {
            if (id == 0 || patchDTO == null)
                return BadRequest();
            var vaill = _db.villas.AsNoTracking().FirstOrDefault
              (x => x.Id == id);
            if (vaill == null)
                return BadRequest();
   
            VillaDTO villaDTO = new()
            {
                Id = vaill.Id,
                Name = vaill.Name,
                Amenity = vaill.Amenity,
                Details = vaill.Details,
                ImageUrl = vaill.ImageUrl,
                Occupancy = vaill.Occupancy,
                Rate = vaill.Rate,
                Sqft = vaill.Sqft

            };
            patchDTO.ApplyTo(villaDTO , ModelState);
            Villa model = new()
            {
                Id = villaDTO.Id,
                Name = villaDTO.Name,
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                ImageUrl = villaDTO.ImageUrl,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft

            };
            _db.villas.Update(model);
            _db.SaveChanges();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return NoContent();
        }

    }

}
