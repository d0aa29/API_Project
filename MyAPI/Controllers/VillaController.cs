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
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
       // private ApplicationDbContext _db;
        private readonly IVillaRepository _dbvilla;
        private readonly IMapper _mapper;
        private APIResponse _response;
        public VillaController(IVillaRepository dbvilla, IMapper mapper)
        {
        
            _dbvilla = dbvilla;
             _mapper = mapper;
            this._response = new ();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetVialla()
        {
            try
            {
                IEnumerable<Villa> villaList = await _dbvilla.GetAll();
                _response.Result = _mapper.Map<List<VillaDTO>>(villaList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex) { 
                _response.IsSuccess = false;
                _response.ErrorMessages=new List<string>() { ex.ToString()};
            }
            return _response;
        }

        [HttpGet("{id:int}", Name = ("GetViall"))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetViall(int id)
        {
            try { 
            if (id == 0)
            {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
            }
            var villa = await _dbvilla.Get(x => x.Id == id);
            if (villa == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
            _response.Result = _mapper.Map<VillaDTO>(villa);
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateViall([FromBody] VillaCreateDTO villaDTO)
        {
            try { 
            if (villaDTO == null)
                return BadRequest();
           
            if (await _dbvilla.Get(x => x.Name.ToLower() == villaDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("", "Vaill alredy exist");
                return BadRequest(ModelState);
            }
           
            Villa villa = _mapper.Map<Villa>(villaDTO);

            await _dbvilla.Create(villa);
            // await _dbvilla.Save();
            _response.Result = _mapper.Map<VillaDTO>(villa);
            _response.StatusCode = HttpStatusCode.OK;
          //  return Ok(_response);
            return CreatedAtRoute("GetViall", new { id = villa.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
       
        
        
        
        
        
        [HttpDelete("{id:int}", Name = ("DeleteViall"))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> DeleteViall(int id)
        {
            try { 
            if (id == null)
                return BadRequest();
           var vaill = await _dbvilla.Get
               (x => x.Id == id);
            if (vaill == null)
            {
                return BadRequest();
            }
            await _dbvilla.Remove(vaill);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPut("{id:int}", Name = ("UpdateViall"))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateViall(int id, [FromBody] VillaUpdateDTO villaDTOcs)
        {
            try { 
            if (id != villaDTOcs.Id || villaDTOcs == null)
                return BadRequest();

      
            Villa model = _mapper.Map<Villa>(villaDTOcs);

          
            await _dbvilla.Update(model);

          //  _response.Result = _mapper.Map<VillaDTO>(model);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPatch("{id:int}", Name = ("UpdatePartialViall"))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse> >UpdatePartialViall(int id, JsonPatchDocument<VillaUpdateDTO> patchDTO)
        {
            if (id == 0 || patchDTO == null)
                return BadRequest();
            var vaill = await _dbvilla.Get
              (x => x.Id == id,tracked:false);
            if (vaill == null)
                return BadRequest();

            VillaUpdateDTO villaDTO = _mapper.Map<VillaUpdateDTO>(vaill);

            patchDTO.ApplyTo(villaDTO , ModelState);
          
            Villa model = _mapper.Map<Villa>(villaDTO);

            _dbvilla.Update(model);
       
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return NoContent();
        }

    }

}
