using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using  GlampingProyect.Web.Core;
using  GlampingProyect.Web.Core.Pagination;
using  GlampingProyect.Web.Data;
using  GlampingProyect.Web.Data.Entities;
using  GlampingProyect.Web.DTOs;
using  GlampingProyect.Web.Helpers;
using  GlampingProyect.Web.Data.Entities;
using  GlampingProyect.Web.Core;
using  GlampingProyect.Web.Services;

namespace  GlampingProyect.Web.Services
{
    public interface IRolesService
    {
        public Task<Response<PaginationResponse<GlampingRoleDTO>>> GetPaginationAsync(PaginationRequest request);
        public Task<Response<GlampingRoleDTO>> GetOneAsync(int id);
        public Task<Response<List<PermissionDTO>>> GetPermissionsAsync();
        public Task<Response<List<CategoryDTO>>> GetCategoriesAsync();
        public Task<Response<GlampingRoleDTO>> CreateAsync(GlampingRoleDTO dto);
        public Task<Response<GlampingRoleDTO>> EditAsync(GlampingRoleDTO dto);
        public Task<Response<List<PermissionForRoleDTO>>> GetPermissionsByRoleAsync(int id);
        public Task<Response<List<CategoryForRoleDTO>>> GetCategoriesByRoleAsync(int id);
    }

    public class RolesService : CustomQueryableOperations, IRolesService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public RolesService(DataContext context, IMapper mapper) : base (context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<GlampingRoleDTO>> CreateAsync(GlampingRoleDTO dto)
        {
            using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Role Creation
                    GlampingRole role = _mapper.Map<GlampingRole>(dto);
                    await _context.GlampingRoles.AddAsync(role);

                    await _context.SaveChangesAsync();

                    int roleId = role.Id;

                    // Permissions
                    List<int> permissionIds = new();

                    if (!string.IsNullOrWhiteSpace(dto.PermissionIds))
                    {
                        permissionIds = JsonConvert.DeserializeObject<List<int>>(dto.PermissionIds);
                    }

                    foreach(int permissionId in permissionIds)
                    {
                        RolePermission rolePermission = new RolePermission
                        {
                            RoleId = roleId,
                            PermissionId = permissionId
                        };

                        await _context.RolePermissions.AddAsync(rolePermission);
                    }

                    // Sections
                    List<int> categoryIds = new();

                    if (!string.IsNullOrWhiteSpace(dto.CategoryIds))
                    {
                        categoryIds = JsonConvert.DeserializeObject<List<int>>(dto.CategoryIds);
                    }

                    foreach (int categoryId in categoryIds)
                    {
                        RoleCategory rolePermission = new RoleCategory
                        {
                            RoleId = roleId,
                            CategoryId = categoryId
                        };

                        await _context.RoleCategories.AddAsync(rolePermission);
                    }

                    await _context.SaveChangesAsync();

                    transaction.Commit();

                    return ResponseHelper<GlampingRoleDTO>.MakeResponseSuccess(dto, "Rol creado con éxito");
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    return ResponseHelper<GlampingRoleDTO>.MakeResponseFail(ex);
                }
            }
        }

        public async Task<Response<GlampingRoleDTO>> EditAsync(GlampingRoleDTO dto)
        {
            try
            {
                if (dto.Name == Env.SUPER_ADMIN_ROL_NAME)
                {
                    return ResponseHelper<GlampingRoleDTO>.MakeResponseFail($"El rol '{Env.SUPER_ADMIN_ROL_NAME}' no puede ser editado");
                }

                // Permissions
                List<int> permissionIds = new();

                if (!string.IsNullOrWhiteSpace(dto.PermissionIds))
                {
                    permissionIds = JsonConvert.DeserializeObject<List<int>>(dto.PermissionIds);
                }

                // Delete old permissions
                List<RolePermission> oldRolePermissions = await _context.RolePermissions.Where(rp => rp.RoleId == dto.Id).ToListAsync();
                _context.RolePermissions.RemoveRange(oldRolePermissions);

                foreach (int permissionId in permissionIds)
                {
                    RolePermission rolePermission = new RolePermission
                    {
                        RoleId = dto.Id,
                        PermissionId = permissionId
                    };

                    await _context.RolePermissions.AddAsync(rolePermission);
                }

                // Sections
                List<int> categoryIds = new();

                if (!string.IsNullOrWhiteSpace(dto.CategoryIds))
                {
                    categoryIds = JsonConvert.DeserializeObject<List<int>>(dto.CategoryIds);
                }

                // Delete old sections
                List<RoleCategory> oldRoleCategories = await _context.RoleCategories.Where(rp => rp.RoleId == dto.Id).ToListAsync();
                _context.RoleCategories.RemoveRange(oldRoleCategories);

                foreach (int categoryId in categoryIds)
                {
                    RoleCategory rolePermission = new RoleCategory
                    {
                        RoleId = dto.Id,
                        CategoryId = categoryId
                    };

                    await _context.RoleCategories.AddAsync(rolePermission);
                }

                // Update role
                GlampingRole role = _mapper.Map<GlampingRole>(dto);
                _context.GlampingRoles.Update(role);

                await _context.SaveChangesAsync();

                return ResponseHelper<GlampingRoleDTO>.MakeResponseSuccess(dto, "Rol actualizado con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<GlampingRoleDTO>.MakeResponseFail(ex);
            }
            
        }

        public async Task<Response<PaginationResponse<GlampingRoleDTO>>> GetPaginationAsync(PaginationRequest request)
        {
            IQueryable<GlampingRole> query = _context.GlampingRoles.AsQueryable();

            if (!string.IsNullOrEmpty(request.Filter))
            {
                query = query.Where(b => b.Name.ToLower()
                                               .Contains(request.Filter
                                               .ToLower()));
            }

            return await GetPaginationAsync<GlampingRole, GlampingRoleDTO>(request, query);
        }

        public async Task<Response<GlampingRoleDTO>> GetOneAsync(int id)
        {
            try
            {
                GlampingRole role = await _context.GlampingRoles.FirstOrDefaultAsync(r => r.Id == id);

                if (role is null)
                {
                    return ResponseHelper<GlampingRoleDTO>.MakeResponseFail($"El rol con id '{id}' no existe.");
                }

                List<PermissionForRoleDTO> permissions = await _context.Permissions.Select(p => new PermissionForRoleDTO
                {
                    Id = p.Id,
                    Description = p.Description,
                    Name = p.Name,
                    Module = p.Module,
                    Selected = _context.RolePermissions.Any(rp => rp.PermissionId == p.Id && rp.RoleId == role.Id)
                }).ToListAsync();


                List<CategoryForRoleDTO> categories = await _context.Categories.Select(p => new CategoryForRoleDTO
                {
                    Id = p.Id,
                    Description = p.Description,
                    Name = p.Name,
                    Selected = _context.RoleCategories.Any(rs => rs.CategoryId == p.Id && rs.RoleId == role.Id)
                }).ToListAsync();

                GlampingRoleDTO dto = new GlampingRoleDTO
                {
                    Id = role.Id,
                    Name = role.Name,
                    Permissions = permissions,
                    Categories = categories
                };

                return ResponseHelper<GlampingRoleDTO>.MakeResponseSuccess(dto);
            }
            catch(Exception ex)
            {
                return ResponseHelper<GlampingRoleDTO>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<List<PermissionDTO>>> GetPermissionsAsync()
        {
            return await GetCompleteList<Permission, PermissionDTO>();
        }

        public async Task<Response<List<CategoryDTO>>> GetSectionsAsync()
        {
            return await GetCompleteList<Category, CategoryDTO>();
        }

        public async Task<Response<List<PermissionForRoleDTO>>> GetPermissionsByRoleAsync(int id)
        {
            try
            {
                Response<GlampingRoleDTO> response = await GetOneAsync(id);

                if (!response.IsSuccess)
                {
                    return ResponseHelper<List<PermissionForRoleDTO>>.MakeResponseFail(response.Message);
                }

                List<PermissionForRoleDTO> permissions = response.Result.Permissions;

                return ResponseHelper<List<PermissionForRoleDTO>>.MakeResponseSuccess(permissions);
            }
            catch (Exception ex)
            {
                return ResponseHelper<List<PermissionForRoleDTO>>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<List<CategoryForRoleDTO>>> GetCategoriesByRoleAsync(int id)
        {
            try
            {
                Response<GlampingRoleDTO> response = await GetOneAsync(id);

                if (!response.IsSuccess)
                {
                    return ResponseHelper<List<CategoryForRoleDTO>>.MakeResponseFail(response.Message);
                }

                List<CategoryForRoleDTO> categories = response.Result.Categories;

                return ResponseHelper<List<CategoryForRoleDTO>>.MakeResponseSuccess(categories);
            }
            catch (Exception ex)
            {
                return ResponseHelper<List<CategoryForRoleDTO>>.MakeResponseFail(ex);
            }
        }

        public Task<Response<List<CategoryDTO>>> GetCategoriesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
