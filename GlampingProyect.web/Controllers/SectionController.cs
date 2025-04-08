using AspNetCoreHero.ToastNotification.Abstractions;
using Azure;
using Microsoft.AspNetCore.Mvc;
using GlampingProyect.Web.Core;
using GlampingProyect.Web.Core.Pagination;
using GlampingProyect.Web.DTOs;
using GlampingProyect.Web.Services;
using System.Threading.Tasks;
using Library1.Cor;
using Humanizer;


namespace GlampingProyect.Web.Controllers
{
    public class SectionsController : Controller
    {
        private readonly ISectionsService _sectionsService;
        private readonly INotyfService _notifyService;

        public SectionsController(ISectionsService sectionsService, INotyfService notifyService)
        {
            _sectionsService = sectionsService;
            _notifyService = notifyService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] PaginationRequest request)
        {
            Res<PaginationResponse<SectionDTO>> response = await _sectionsService.GetPaginationAsync(request);
            return View(response.MyProperty);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] SectionDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _notifyService.Error("Debe ajustar los errores de validación");
                return View(dto);
            }

            Res<SectionDTO> response = await _sectionsService.CreateAsync(dto);

            if (response.IsSuccess)
            {
                _notifyService.Success(response.Message);
                return RedirectToAction(nameof(Index));
            }

            _notifyService.Error(response.Message);
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            Res<SectionDTO> response = await _sectionsService.GetOneAsync(id);

            if (response.IsSuccess)
            {
                return View(response.MyProperty);
            }

            _notifyService.Error(response.Message);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SectionDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _notifyService.Error("Debe ajustar los errores de validación");
                return View(dto);
            }

            Res<SectionDTO> response = await _sectionsService.EditAsync(dto);

            if (response.IsSuccess)
            {
                _notifyService.Success(response.Message);
                return RedirectToAction(nameof(Index));
            }

            _notifyService.Error(response.Message);
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Res<object> response = await _sectionsService.DeleteAsync(id);

            if (response.IsSuccess)
            {
                _notifyService.Success(response.Message);
            }
            else
            {
                _notifyService.Error(response.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Toggle([FromForm] ToggleSectionStatusDTO dto)
        {
            Res<object> response = await _sectionsService.ToggleAsync(dto);

            if (response.IsSuccess)
            {
                _notifyService.Success(response.Message);
            }
            else
            {
                _notifyService.Error(response.Message);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
