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
    [ApiVersion("1.0")]
    [ApiController]
    public class VillaNumberController : ControllerBase
    {
       // private ApplicationDbContext _db;
        private readonly IVillaNumberRepository _dbvillaNumber;
        private readonly IVillaRepository _dbvilla;
        private readonly IMapper _mapper;
        private APIResponse _response;
        public VillaNumberController(IVillaNumberRepository dbvillaNumber, IVillaRepository dbvilla, IMapper mapper)
        {

            _dbvillaNumber = dbvillaNumber;
             _mapper = mapper;
            _dbvilla=dbvilla;
            this._response = new ();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetViallaNumber()
        {
            try
            {
                IEnumerable<VillaNumber> villaNumberList = await _dbvillaNumber.GetAll();
                _response.Result = _mapper.Map<List<VillaNumberDTO>>(villaNumberList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex) { 
                _response.IsSuccess = false;
                _response.ErrorMessages=new List<string>() { ex.ToString()};
            }
            return _response;
        }

        [HttpGet("{id:int}", Name = ("GetViallNumber"))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetViallNumber(int id)
        {
            try { 
            if (id == 0)
            {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
            }
            var villaNumber = await _dbvillaNumber.Get(x => x.VillaNo == id);
            if (villaNumber == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
            _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
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
        public async Task<ActionResult<APIResponse>> CreateViallNumber([FromBody] VillaNumberCreateDTO villaNumberDTO)
        {
            try { 
            if (villaNumberDTO == null)
                return BadRequest();
           
            if (await _dbvillaNumber.Get(x => x.VillaNo == villaNumberDTO.VillaNo) != null)
            {
                ModelState.AddModelError("", "Vaill Number alredy exist");
                return BadRequest(ModelState);
            }
                if (await _dbvilla.Get(x => x.Id == villaNumberDTO.VillaID) == null)
                {
                    ModelState.AddModelError("CustomerError", "Vaill id not valid");
                    return BadRequest(ModelState);
                }

                VillaNumber villaNumber = _mapper.Map<VillaNumber>(villaNumberDTO);

            await _dbvillaNumber.Create(villaNumber);
            _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
            _response.StatusCode = HttpStatusCode.OK;
         
            return CreatedAtRoute("GetViallNumber", new { id = villaNumber.VillaNo }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
       
        
        
        
        
        
        [HttpDelete("{id:int}", Name = ("DeleteViallNumber"))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> DeleteViallNumber(int id)
        {
            try { 
            if (id == null)
                return BadRequest();
           var villaNumber = await _dbvillaNumber.Get
               (x => x.VillaNo == id);
            if (villaNumber == null)
            {
                return BadRequest();
            }
            await _dbvillaNumber.Remove(villaNumber);
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
        [HttpPut("{id:int}", Name = ("UpdateViallNumber"))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateViallNumber(int id, [FromBody] VillaNumberUpdateDTO villaNumberDTOcs)
        {
            try { 
            if (id != villaNumberDTOcs.VillaNo || villaNumberDTOcs == null)
                return BadRequest();

                if (await _dbvilla.Get(x => x.Id == villaNumberDTOcs.VillaID) == null)
                {
                    ModelState.AddModelError("CustomerError", "Vaill id not valid");
                    return BadRequest(ModelState);
                }

                VillaNumber model = _mapper.Map<VillaNumber>(villaNumberDTOcs);

          
            await _dbvillaNumber.Update(model);

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

        //[HttpPatch("{id:int}", Name = ("UpdatePartialViallNumber"))]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<APIResponse> > UpdatePartialViallNumber(int id, JsonPatchDocument<VillaNumberUpdateDTO> patchDTO)
        //{
        //    if (id == 0 || patchDTO == null)
        //        return BadRequest();
        //    var vaillNumber = await _dbvillaNumber.Get
        //      (x => x.VillaNo == id,tracked:false);
        //    if (vaillNumber == null)
        //        return BadRequest();

        //    VillaNumberUpdateDTO villaNumberDTO = _mapper.Map<VillaNumberUpdateDTO>(vaillNumber);

        //    patchDTO.ApplyTo(villaNumberDTO, ModelState);

        //    VillaNumber model = _mapper.Map<VillaNumber>(villaNumberDTO);

        //    _dbvillaNumber.Update(model);
       
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    return NoContent();
        //}

    }

}
