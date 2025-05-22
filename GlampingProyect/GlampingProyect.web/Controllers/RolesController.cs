using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using  GlampingProyect.Web.Core.Attributes;
using  GlampingProyect.Web.Core;
using  GlampingProyect.Web.Core.Pagination;
using  GlampingProyect.Web.Data.Entities;
using  GlampingProyect.Web.DTOs;
using  GlampingProyect.Web.Services;

namespace  GlampingProyect.Web.Controllers
{
    public class RolesController : Controller
    {
        private readonly IRolesService _rolesService;
        private readonly INotyfService _notifyService;

        public RolesController(IRolesService rolesService, INotyfService notifyService)
        {
            _rolesService = rolesService;
            _notifyService = notifyService;
        }

        [HttpGet]
        [CustomAuthorize(permission: "showRoles", module: "Roles")]
        public async Task<IActionResult> Index([FromQuery] PaginationRequest request)
        {
            Response<PaginationResponse<GlampingRoleDTO>> response = await _rolesService.GetPaginationAsync(request);
            if (!response.IsSuccess)
            {
                _notifyService.Error(response.Message);
                return RedirectToAction("Index", "Home");
            }
            return View(response.Result);
        }

        [HttpGet]
        [CustomAuthorize(permission: "createRoles", module: "Roles")]
        public async Task<IActionResult> Create()
        {
            Response<List<PermissionDTO>> permissionsResponse = await _rolesService.GetPermissionsAsync();

            if (!permissionsResponse.IsSuccess)
            {
                _notifyService.Error(permissionsResponse.Message);
                return RedirectToAction(nameof(Index));
            }

            Response<List<CategoryDTO>> CategoryResponse = await _rolesService.GetCategoriesAsync();

            if (!CategoryResponse.IsSuccess)
            {
                _notifyService.Error(CategoryResponse.Message);
                return RedirectToAction(nameof(Index));
            }

            GlampingRoleDTO dto = new GlampingRoleDTO
            {
                Permissions = permissionsResponse.Result.Select(p => new PermissionForRoleDTO 
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Module = p.Module,
                    Selected = false
                }).ToList(),

                Categories = CategoryResponse.Result.Select(p => new CategoryForRoleDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Selected = false
                }).ToList(),
            };

            return View(dto);
        }



        [HttpPost]
        [CustomAuthorize(permission: "createRoles", module: "Roles")]
        public async Task<IActionResult> Create(GlampingRoleDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _notifyService.Error("Debe ajustar los errores de validación");

                Response<List<PermissionDTO>> permissionResponse1 = await _rolesService.GetPermissionsAsync();
                Response<List<CategoryDTO>> sectionsResponse1 = await _rolesService.GetCategoriesAsync();

                dto.Permissions = permissionResponse1.Result.Select(p => new PermissionForRoleDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Module = p.Module,
                    Selected = false,
                }).ToList();

                dto.Categories = sectionsResponse1.Result.Select(p => new CategoryForRoleDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                }).ToList();

                return View(dto);
            }

            Response<GlampingRoleDTO> createResponse = await _rolesService.CreateAsync(dto);

            if (createResponse.IsSuccess)
            {
                _notifyService.Success(createResponse.Message);
                return RedirectToAction(nameof(Index));
            }

            _notifyService.Error(createResponse.Message);

            Response<List<PermissionDTO>> pemrissionResponse2 = await _rolesService.GetPermissionsAsync();
            Response<List<CategoryDTO>> sectionsResponse2 = await _rolesService.GetCategoriesAsync();

            dto.Permissions = pemrissionResponse2.Result.Select(p => new PermissionForRoleDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Module = p.Module,
            }).ToList();

            dto.Categories = sectionsResponse2.Result.Select(p => new CategoryForRoleDTO
            {
                Id = p.Id,
                Name = p.Name,
            }).ToList();

            return View(dto);
        }


        [HttpPost]
        [CustomAuthorize(permission: "updateRoles", module: "Roles")]
        public async Task<IActionResult> Edit(GlampingRoleDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _notifyService.Error("Debe ajustar los errores de validación");

                Response<List<PermissionForRoleDTO>> permissionsByRoleResponse = await _rolesService.GetPermissionsByRoleAsync(dto.Id);
                Response<List<CategoryForRoleDTO>> categoriesByRoleResponse = await _rolesService.GetCategoriesByRoleAsync(dto.Id);
                dto.Permissions = permissionsByRoleResponse.Result.ToList();
                dto.Categories = categoriesByRoleResponse.Result.ToList();

                return View(dto);
            }

            Response<GlampingRoleDTO> editResponse = await _rolesService.EditAsync(dto);

            if (editResponse.IsSuccess)
            {
                _notifyService.Success(editResponse.Message);
                return RedirectToAction(nameof(Index));
            }

            _notifyService.Error(editResponse.Message);

            Response<List<PermissionForRoleDTO>> permissionsByRoleResponse2 = await _rolesService.GetPermissionsByRoleAsync(dto.Id);
            Response<List<CategoryForRoleDTO>> sectionsByRoleResponse2 = await _rolesService.GetCategoriesByRoleAsync(dto.Id);
            dto.Permissions = permissionsByRoleResponse2.Result.ToList();
            dto.Categories = sectionsByRoleResponse2.Result.ToList();

            return View(dto);
        }

        [HttpGet]
        [CustomAuthorize(permission: "updateRoles", module: "Roles")]
        public async Task<IActionResult> Edit(int id)
        {
            Response<GlampingRoleDTO> response = await _rolesService.GetOneAsync(id);

            if (!response.IsSuccess)
            {
                _notifyService.Error(response.Message);
                return RedirectToAction(nameof(Index));
            }

            return View(response.Result);
        }
    }
}
