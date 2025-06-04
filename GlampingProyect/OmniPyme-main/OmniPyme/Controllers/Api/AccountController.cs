using GlampingProyect.Web.Core;
using GlampingProyect.Web.Core.Pagination;
using GlampingProyect.Web.DTOs;
using GlampingProyect.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlampingProyect.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public AccountController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        //[HttpGet]
        //public async Task<IActionResult> Get([FromQuery] PaginationRequest request)
        //{
        //    Microsoft.AspNetCore.Identity.SignInResult result = await _usersService.LoginAsync(dto);
        //    return StatusCode(StatusCodes.Status200OK, response);
        //}

        //[HttpPost]
        //public async Task<IActionResult> CreateUser([FromBody] UsersDTO dto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        //    }
        //    Response<LoginDTO> response = await _usersService.CreateAsync(dto);

        //    if (response.IsSuccess)
        //    {
        //        return StatusCode(StatusCodes.Status201Created, response);

        //    }
        //    return StatusCode(StatusCodes.Status400BadRequest, response);
        //}
    }
}
