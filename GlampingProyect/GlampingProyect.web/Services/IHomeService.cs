using AutoMapper;
using Microsoft.EntityFrameworkCore;
using  GlampingProyect.Web.Core;
using  GlampingProyect.Web.Core.Pagination;
using  GlampingProyect.Web.Data;
using  GlampingProyect.Web.Data.Entities;
using  GlampingProyect.Web.DTOs;
using  GlampingProyect.Web.Helpers;
using ClaimsUser = System.Security.Claims.ClaimsPrincipal;
using  GlampingProyect.Web.Data.Entities;

namespace  GlampingProyect.Web.Services
{
    public interface IHomeService
    {
        public Task<Response<GlampingDTO>> GetGlampingAsync(int id);
        public Task<Response<CategoryDTO>> GetCategoryAsync(PaginationRequest request, int id);
        public Task<Response<PaginationResponse<CategoryDTO>>> GetCategoryAsync(PaginationRequest request);
    }

    public class HomeService : IHomeService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public HomeService(DataContext context, IHttpContextAccessor httpContextAccessor, IUsersService usersService, IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _usersService = usersService;
            _mapper = mapper;
        }

        public async Task<Response<GlampingDTO>> GetGlampingAsync(int id)
        {
            try
            {
                Glamping? glamping = await _context.Glampings.FirstOrDefaultAsync(b => b.Id == id);

                if (glamping is null)
                {
                    return ResponseHelper<GlampingDTO>.MakeResponseFail($"El blog con id '{id}' no exuste.");
                }

                return ResponseHelper<GlampingDTO>.MakeResponseSuccess(_mapper.Map<GlampingDTO>(glamping));
            }
            catch (Exception ex)
            {
                return ResponseHelper<GlampingDTO>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<CategoryDTO>> GetCategoryAsync(PaginationRequest request, int id)
        {
            try
            {
                Category? category = await _context.Categories.Include(s => s.RoleCategories)
                                                          .Where(s => !s.IsHidden && s.Id == id)
                                                          .FirstOrDefaultAsync();

                if (category is null)
                {
                    return ResponseHelper<CategoryDTO>.MakeResponseFail($"La sección con id '{id}' no existe.");
                }

                ClaimsUser? claimsUser = _httpContextAccessor.HttpContext?.User;
                string? userName = claimsUser.Identity.Name;
                User user = await _usersService.GetUserAsync(userName);

                bool isAuthorized = true;
                if (!await _usersService.CurrentUserIsSuperAdmin())
                {
                    isAuthorized = category.RoleCategories.Any(rs => rs.RoleId == user.GlampingRoleId);
                }

                if (!isAuthorized)
                {
                    return ResponseHelper<CategoryDTO>.MakeResponseFail("No tiene autorización para consultar esta sección");
                }

                IQueryable<Glamping> query = _context.Glampings.Where(b => b.CategoryId == category.Id);

                if (!string.IsNullOrWhiteSpace(request.Filter))
                {
                    query = query.Where(s => s.Name.ToLower().Contains(request.Filter.ToLower()));
                }

                query = query.Select(b => new Glamping
                {
                    Id = b.Id,
                    Name = b.Name,
                });

                PagedList<Glamping> list = await PagedList<Glamping>.ToPagedListAsync(query, request);

                PaginationResponse<GlampingDTO> paginatedGlampingsResponse = new PaginationResponse<GlampingDTO>
                {
                    List = _mapper.Map<PagedList<GlampingDTO>>(list),
                    TotalCount = list.TotalCount,
                    RecordsPerPage = list.RecordsPerPage,
                    CurrentPage = list.CurrentPage,
                    TotalPages = list.TotalPages,
                    Filter = request.Filter,
                };

                CategoryDTO dto = new CategoryDTO
                {
                    Id = category.Id,
                    Name = category.Name,
                    PaginatedGlampings = paginatedGlampingsResponse
                };

                return ResponseHelper<CategoryDTO>.MakeResponseSuccess(dto);

            }
            catch(Exception ex)
            {
                return ResponseHelper<CategoryDTO>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<PaginationResponse<CategoryDTO>>> GetCategoryAsync(PaginationRequest request)
        {
            try
            {
                ClaimsUser? claimsUser = _httpContextAccessor.HttpContext?.User;
                string? userName = claimsUser.Identity.Name;
                User user = await _usersService.GetUserAsync(userName);

                IQueryable<Category> query = _context.Categories.Include(s => s.RoleCategories)
                                                             .Where(s => !s.IsHidden);

                if (!await _usersService.CurrentUserIsSuperAdmin())
                {
                    query = query.Where(s => s.RoleCategories.Any(rs => rs.RoleId == user.GlampingRoleId));
                }

                if (!string.IsNullOrWhiteSpace(request.Filter))
                {
                    query = query.Where(s => s.Name.ToLower().Contains(request.Filter.ToLower()));
                }

                PagedList<Category> list = await PagedList<Category>.ToPagedListAsync(query, request);

                PaginationResponse<CategoryDTO> response = new PaginationResponse<CategoryDTO>
                {
                    List = _mapper.Map<PagedList<CategoryDTO>>(list),
                    TotalCount = list.TotalCount,
                    RecordsPerPage = list.RecordsPerPage,
                    CurrentPage = list.CurrentPage,
                    TotalPages = list.TotalPages,
                    Filter = request.Filter,
                };

                return ResponseHelper<PaginationResponse<CategoryDTO>>.MakeResponseSuccess(response);
            }
            catch(Exception ex)
            {
                return ResponseHelper<PaginationResponse<CategoryDTO>>.MakeResponseFail(ex);
            }
        }
    }
}
