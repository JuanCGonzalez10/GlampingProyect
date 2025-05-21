//using AutoMapper;
using AutoMapper;
using Library1.Cor;
using Microsoft.EntityFrameworkCore;
using GlampingProyect.Web.Core;
using GlampingProyect.Web.Core.Pagination;
using GlampingProyect.Web.Data;
using GlampingProyect.Web.Data.Entities;
using GlampingProyect.Web.DTOs;
using GlampingProyect.Web.Helpers;

namespace GlampingProyect.Web.Services
{
    public interface IGlampingsService
    {
        Task<Response<GlampingDTO>> CreateAsync(GlampingDTO dto);
        Task<Response<object>> DeleteAsync(int id);
        Task<Response<GlampingDTO>> EditAsync(GlampingDTO dto);
        Task<Response<GlampingDTO>> GetOneAsync(int id);
        Task<Response<PaginationResponse<GlampingDTO>>> GetPaginationAsync(PaginationRequest request);
    }

    public class GlampingsService : CustomQueryableOperations, IGlampingsService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GlampingsService(DataContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<GlampingDTO>> CreateAsync(GlampingDTO dto)
        {
            return await CreateAsync<Glamping, GlampingDTO>(dto);
        }

        public async Task<Response<object>> DeleteAsync(int id)
        {
            var response = await DeleteAsync<Glamping>(id);
            response.Message = !response.IsSuccess ? $"El glamping con id: {id} no existe" : response.Message;
            return response;
        }

        public async Task<Response<GlampingDTO>> EditAsync(GlampingDTO dto)
        {
            return await EditAsync<Glamping, GlampingDTO>(dto, dto.Id);
        }

        public async Task<Response<GlampingDTO>> GetOneAsync(int id)
        {
            return await GetOneAsync<Glamping, GlampingDTO>(id);
        }

        public async Task<Response<PaginationResponse<GlampingDTO>>> GetPaginationAsync(PaginationRequest request)
        {
            IQueryable<Glamping> query = _context.Glampings.Include(g => g.Category)
                .Select(g => new Glamping
                {
                    Id = g.Id,
                    Name = g.Name,
                    Category = g.Category
                });

            if (!string.IsNullOrEmpty(request.Filter))
            {
                query = query.Where(g => g.Name.ToLower().Contains(request.Filter.ToLower()));
            }

            return await GetPaginationAsync<Glamping, GlampingDTO>(request, query);
        }
    }
}
