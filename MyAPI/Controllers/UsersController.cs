using System.Net;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Models;
using MyAPI.Models.Dto;
using MyAPI.Repository.IRepository;

namespace MyAPI.Controllers
{
    [Route("api/v{version:apiVersion}/Users")]
    [ApiVersionNeutral]
   // [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        protected APIResponse _response;
        public UsersController(IUserRepository userRepo) {
            _userRepo = userRepo;
            this._response = new ();
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginRequestDTO model)
        {
            var loginResponse = await _userRepo.Login(model);
            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token)) {
                _response.StatusCode=HttpStatusCode.BadRequest;
                _response.IsSuccess=false;
                _response.ErrorMessages.Add("username or password is incorrect");
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = loginResponse;
            return Ok(_response);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterationRequestDTO model)
        {
            bool ifUserNameUnique =_userRepo.IsUniqueUser(model.UserName);
            if (!ifUserNameUnique) {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("username already exists");
                return BadRequest(_response);
            }
            var user= await _userRepo.Register(model);
            if (user == null) {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Error while registering");
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
           
            return Ok(_response);

        }
    }
}
