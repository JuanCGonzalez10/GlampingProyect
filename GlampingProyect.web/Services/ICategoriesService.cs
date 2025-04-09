using AutoMapper;
using GlampingProyect.Web.Core;
using GlampingProyect.Web.Core.Pagination;
using GlampingProyect.Web.Data;
using GlampingProyect.Web.Data.Entities;
using GlampingProyect.Web.DTOs;
using GlampingProyect.Web.Helpers;
using Library1.Cor;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlampingProyect.Web.Services
{
    public interface ICategoriesService
    {
        Task<Res<CategoryDTO>> CreateAsync(CategoryDTO dto);
        Task<Res<object>> DeleteAsync(int id);
        Task<Res<CategoryDTO>> EditAsync(CategoryDTO dto);
        Task<Res<List<CategoryDTO>>> GetListAsync();
        Task<Res<CategoryDTO>> GetOneAsync(int id);
        Task<Res<PaginationResponse<CategoryDTO>>> GetPaginationAsync(PaginationRequest request);
        Task<Res<object>> ToggleAsync(ToggleCategoryStatusDTO dto);
    }

    public class CategoriesService : CustomQueryableOperations, ICategoriesService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CategoriesService(DataContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Res<CategoryDTO>> CreateAsync(CategoryDTO dto)
        {
            return await CreateAsync<Category, CategoryDTO>(dto);
        }

        public async Task<Res<object>> DeleteAsync(int id)
        {
            try
            {
                Category? categoryInstance = await _context.Categories.FirstOrDefaultAsync(s => s.Id == id);

                if (categoryInstance is null)
                {
                    return ResponseHelper<object>.MakeResponseFail($"No existe categoría con id {id}");
                }

                _context.Categories.Remove(categoryInstance);
                await _context.SaveChangesAsync();

                return ResponseHelper<object>.MakeResponseSuccess("Categoría eliminada con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<object>.MakeResponseFail(ex);
            }
        }

        public async Task<Res<CategoryDTO>> EditAsync(CategoryDTO dto)
        {
            try
            {
                Category? category = await _context.Categories.AsNoTracking()
                                                              .FirstOrDefaultAsync(s => s.Id == dto.Id);

                if (category is null)
                {
                    return ResponseHelper<CategoryDTO>.MakeResponseFail($"No existe categoría con id {dto.Id}");
                }

                category = _mapper.Map<Category>(dto);
                _context.Update(category);
                await _context.SaveChangesAsync();

                return ResponseHelper<CategoryDTO>.MakeResponseSuccess(dto, "Categoría actualizada con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<CategoryDTO>.MakeResponseFail(ex);
            }
        }

        public async Task<Res<List<CategoryDTO>>> GetListAsync()
        {
            try
            {
                List<Category> categories = await _context.Categories.ToListAsync();
                List<CategoryDTO> list = _mapper.Map<List<CategoryDTO>>(categories);

                return ResponseHelper<List<CategoryDTO>>.MakeResponseSuccess(list);
            }
            catch (Exception ex)
            {
                return ResponseHelper<List<CategoryDTO>>.MakeResponseFail(ex);
            }
        }

        public async Task<Res<CategoryDTO>> GetOneAsync(int id)
        {
            try
            {
                Category? category = await _context.Categories.FirstOrDefaultAsync(s => s.Id == id);

                if (category is null)
                {
                    return ResponseHelper<CategoryDTO>.MakeResponseFail($"No existe categoría con id {id}");
                }

                CategoryDTO dto = _mapper.Map<CategoryDTO>(category);
                return ResponseHelper<CategoryDTO>.MakeResponseSuccess(dto, "Categoría obtenida con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<CategoryDTO>.MakeResponseFail(ex);
            }
        }

        public async Task<Res<PaginationResponse<CategoryDTO>>> GetPaginationAsync(PaginationRequest request)
        {
            try
            {
                IQueryable<Category> query = _context.Categories.AsQueryable();

                if (!string.IsNullOrEmpty(request.Filter))
                {
                    query = query.Where(b => b.Name.ToLower().Contains(request.Filter.ToLower())
                                           || b.Description.ToLower().Contains(request.Filter.ToLower()));
                }

                return await GetPaginationAsync<Category, CategoryDTO>(request, query);
            }
            catch (Exception ex)
            {
                return ResponseHelper<PaginationResponse<CategoryDTO>>.MakeResponseFail(ex);
            }
        }

        public async Task<Res<object>> ToggleAsync(ToggleCategoryStatusDTO dto)
        {
            try
            {
                Category? category = await _context.Categories.FirstOrDefaultAsync(s => s.Id == dto.CategoryId);

                if (category is null)
                {
                    return ResponseHelper<object>.MakeResponseFail($"No existe categoría con id {dto.CategoryId}");
                }

                category.IsHidden = dto.Hide;
                _context.Categories.Update(category);
                await _context.SaveChangesAsync();

                return ResponseHelper<object>.MakeResponseSuccess("Categoría actualizada con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<object>.MakeResponseFail(ex);
            }
        }
    }
}
