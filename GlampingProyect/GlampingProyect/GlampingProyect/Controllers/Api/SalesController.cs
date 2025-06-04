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
    public class SalesController : ControllerBase
    {
        //private readonly ISalesService _salesService;

        //public SalesController(ISalesService salesService)
        //{
        //    _salesService = salesService;
        //}

        //[HttpGet]
        //public async Task<IActionResult> Get([FromQuery] PaginationRequest request)
        //{
        //    Response<PaginationResponse<UsersDTO>> response = await _usersService.GetPaginationAsync(request);
        //    return StatusCode(StatusCodes.Status200OK, response);
        //}

        //[HttpPost]
        //public async Task<IActionResult> CreateUser([FromBody] UsersDTO dto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        //    }
        //    Response<UsersDTO> response = await _usersService.CreateAsync(dto);

        //    if (response.IsSuccess)
        //    {
        //        return StatusCode(StatusCodes.Status201Created, response);

        //    }
        //    return StatusCode(StatusCodes.Status400BadRequest, response);
        //}
    }
}
