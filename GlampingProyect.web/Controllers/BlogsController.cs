﻿using AspNetCoreHero.ToastNotification.Abstractions;
using Azure;
using Library1.Cor;
using Microsoft.AspNetCore.Mvc;
using GlampingProyect.Web.Core;
using GlampingProyect.Web.Core.Pagination;
using GlampingProyect.Web.DTOs;
using GlampingProyect.Web.Helpers;
using GlampingProyect.Web.Services;

namespace GlampingProyect.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogsService _blogsService;
        private readonly INotyfService _notifyService;
        private readonly ICombosHelper _combosHelper;

        public BlogsController(INotyfService notifyService, IBlogsService blogsService, ICombosHelper combosHelper)
        {
            _notifyService = notifyService;
            _blogsService = blogsService;
            _combosHelper = combosHelper;
        }


        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] PaginationRequest request)
        {
            Res<PaginationResponse<BlogDTO>> response = await _blogsService.GetPaginationAsync(request);

            if (!response.IsSuccess || response.MyProperty == null)
            {
                _notifyService.Error(response.Message ?? "No se pudo cargar la lista de blogs.");
                return View(new PaginationResponse<BlogDTO>()); 
            }

            return View(response.MyProperty);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            BlogDTO dto = new BlogDTO { Sections = await _combosHelper.GetComboSections() };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BlogDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _notifyService.Error("Debe ajustar los errores de validación");
                dto.Sections = await _combosHelper.GetComboSections();
                return View(dto);
            }

            Res<BlogDTO> response = await _blogsService.CreateAsync(dto);

            if (!response.IsSuccess)
            {
                _notifyService.Error(response.Message);
                dto.Sections = await _combosHelper.GetComboSections();
                return View(dto);
            }

            _notifyService.Success(response.Message);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            Res<BlogDTO> response = await _blogsService.GetOneAsync(id);

            if (!response.IsSuccess)
            {
                _notifyService.Error(response.Message);
                return RedirectToAction(nameof(Index));
            }

            response.MyProperty.Sections = await _combosHelper.GetComboSections();
            return View(response.MyProperty);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BlogDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _notifyService.Error("Debe ajustar los errores de validación");
                dto.Sections = await _combosHelper.GetComboSections();
                return View(dto);
            }

            Res<BlogDTO> response = await _blogsService.EditAsync(dto);

            if (!response.IsSuccess)
            {
                _notifyService.Error(response.Message);
                dto.Sections = await _combosHelper.GetComboSections();
                return View(dto);
            }

            _notifyService.Success(response.Message);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Res<object> response = await _blogsService.DeleteAsync(id);

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
