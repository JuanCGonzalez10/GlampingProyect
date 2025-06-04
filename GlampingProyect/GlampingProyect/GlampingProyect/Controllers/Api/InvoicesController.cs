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
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoicesService _invoicesService;

        public InvoicesController(IInvoicesService invoicesService)
        {
            _invoicesService = invoicesService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationRequest request)
        {
            Response<PaginationResponse<InvoiceDTO>> response = await _invoicesService.GetPaginationAsync(request);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] InvoiceDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            Response<InvoiceDTO> response = await _invoicesService.CreateAsync(dto);

            if (response.IsSuccess)
            {
                return StatusCode(StatusCodes.Status201Created, response);

            }
            return StatusCode(StatusCodes.Status400BadRequest, response);
        }
    }
}
