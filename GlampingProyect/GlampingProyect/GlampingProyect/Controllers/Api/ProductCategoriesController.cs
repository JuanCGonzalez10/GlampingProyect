using GlampingProyect.Web.Core;
using GlampingProyect.Web.Core.Pagination;
using GlampingProyect.Web.DTOs;
using GlampingProyect.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace GlampingProyect.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoriesController : ControllerBase
    {
        private readonly IProductCategoriesService _service;

        public ProductCategoriesController(IProductCategoriesService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationRequest request)
        {
            Response<PaginationResponse<ProductCategoryDTO>> response = await _service.GetPaginationAsync(request);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] ProductCategoryDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            Response<ProductCategoryDTO> response = await _service.CreateAsync(dto);

            if (response.IsSuccess)
            {
                return StatusCode(StatusCodes.Status201Created, response);

            }
            return StatusCode(StatusCodes.Status400BadRequest, response);
        }
    }
}
