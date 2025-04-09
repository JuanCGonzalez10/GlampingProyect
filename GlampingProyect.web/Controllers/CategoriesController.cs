using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using GlampingProyect.Web.Core;
using GlampingProyect.Web.Core.Pagination;
using GlampingProyect.Web.DTOs;
using GlampingProyect.Web.Services;
using Library1.Cor;
using System.Threading.Tasks;

namespace GlampingProyect.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService _categoriesService;
        private readonly INotyfService _notifyService;

        public CategoriesController(ICategoriesService categoriesService, INotyfService notifyService)
        {
            _categoriesService = categoriesService;
            _notifyService = notifyService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] PaginationRequest request)
        {
            Res<PaginationResponse<CategoryDTO>> response = await _categoriesService.GetPaginationAsync(request);
            return View(response.MyProperty);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CategoryDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _notifyService.Error("Debe ajustar los errores de validación");
                return View(dto);
            }

            Res<CategoryDTO> response = await _categoriesService.CreateAsync(dto);

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
            Res<CategoryDTO> response = await _categoriesService.GetOneAsync(id);

            if (response.IsSuccess)
            {
                return View(response.MyProperty);
            }

            _notifyService.Error(response.Message);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _notifyService.Error("Debe ajustar los errores de validación");
                return View(dto);
            }

            Res<CategoryDTO> response = await _categoriesService.EditAsync(dto);

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
            Res<object> response = await _categoriesService.DeleteAsync(id);

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
        public async Task<IActionResult> Toggle([FromForm] ToggleCategoryStatusDTO dto)
        {
            Res<object> response = await _categoriesService.ToggleAsync(dto);

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
