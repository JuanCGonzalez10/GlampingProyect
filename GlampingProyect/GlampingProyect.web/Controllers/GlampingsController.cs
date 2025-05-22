using AspNetCoreHero.ToastNotification.Abstractions;
using Library1.Cor;
using Microsoft.AspNetCore.Mvc;
using  GlampingProyect.Web.Core;
using  GlampingProyect.Web.Core.Pagination;
using  GlampingProyect.Web.DTOs;
using  GlampingProyect.Web.Helpers;
using  GlampingProyect.Web.Services;
using Microsoft.AspNetCore.Authorization;

namespace  GlampingProyect.Web.Controllers
{
    
    public class GlampingsController : Controller
    {
        private readonly IGlampingsService _glampingsService;
        private readonly INotyfService _notifyService;
        private readonly ICombosHelper _combosHelper;

        public GlampingsController(INotyfService notifyService, IGlampingsService glampingsService, ICombosHelper combosHelper)
        {
            _notifyService = notifyService;
            _glampingsService = glampingsService;
            _combosHelper = combosHelper;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] PaginationRequest request)
        {
            Response<PaginationResponse<GlampingDTO>> response = await _glampingsService.GetPaginationAsync(request);

            if (!response.IsSuccess || response.Result == null)
            {
                _notifyService.Error(response.Message ?? "No se pudo cargar la lista de glampings.");
                return View(new PaginationResponse<GlampingDTO>());
            }

            return View(response.Result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            GlampingDTO dto = new GlampingDTO { Categories = await _combosHelper.GetComboCategories() };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(GlampingDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _notifyService.Error("Debe ajustar los errores de validación");
                dto.Categories = await _combosHelper.GetComboCategories();
                return View(dto);
            }

            Response<GlampingDTO> response = await _glampingsService.CreateAsync(dto);

            if (!response.IsSuccess)
            {
                _notifyService.Error(response.Message);
                dto.Categories = await _combosHelper.GetComboCategories();
                return View(dto);
            }

            _notifyService.Success(response.Message);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            Response<GlampingDTO> response = await _glampingsService.GetOneAsync(id);

            if (!response.IsSuccess)
            {
                _notifyService.Error(response.Message);
                return RedirectToAction(nameof(Index));
            }

            response.Result.Categories = await _combosHelper.GetComboCategories();
            return View(response.Result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GlampingDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _notifyService.Error("Debe ajustar los errores de validación");
                dto.Categories = await _combosHelper.GetComboCategories();
                return View(dto);
            }

            Response<GlampingDTO> response = await _glampingsService.EditAsync(dto);

            if (!response.IsSuccess)
            {
                _notifyService.Error(response.Message);
                dto.Categories = await _combosHelper.GetComboCategories();
                return View(dto);
            }

            _notifyService.Success(response.Message);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Response<object> response = await _glampingsService.DeleteAsync(id);

            if (!response.IsSuccess)
            {
                _notifyService.Error(response.Message);
            }
            else
            {
                _notifyService.Success(response.Message);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
