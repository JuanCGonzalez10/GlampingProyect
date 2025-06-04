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
    public class RolesController : ControllerBase
    {

        private readonly IRolesService _rolesService;

        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationRequest request)
        {
            Response<PaginationResponse<PrivateURoleDTO>> response = await _rolesService.GetPaginationAsync(request);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoler([FromBody] PrivateURoleDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            Response<PrivateURoleDTO> response = await _rolesService.CreateAsync(dto);

            if (response.IsSuccess)
            {
                return StatusCode(StatusCodes.Status201Created, response);

            }
            return StatusCode(StatusCodes.Status400BadRequest, response);
        }
    }
}
