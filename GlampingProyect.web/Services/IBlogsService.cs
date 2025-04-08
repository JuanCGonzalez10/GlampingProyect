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
    public interface IBlogsService
    {
        public Task<Res<BlogDTO>> CreateAsync(BlogDTO dto);
        public Task<Res<object>> DeleteAsync(int id);
        public Task<Res<BlogDTO>> EditAsync(BlogDTO dto);
        public Task<Res<BlogDTO>> GetOneAsync(int id);
        public Task<Res<PaginationResponse<BlogDTO>>> GetPaginationAsync(PaginationRequest request);
    }

    public class BlogsService : CustomQueryableOperations, IBlogsService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public BlogsService(DataContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Res<BlogDTO>> CreateAsync(BlogDTO dto)
        {
            return await CreateAsync<Blog, BlogDTO>(dto);
        }

        public async Task<Res<object>> DeleteAsync(int id)
        {
            Res<object> response = await DeleteAsync<Blog>(id);
            response.Message = !response.IsSuccess ? $"El blog con id: {id} no existe" : response.Message;
            return response;
        }

        public async Task<Res<BlogDTO>> EditAsync(BlogDTO dto)
        {
            return await EditAsync<Blog, BlogDTO>(dto, dto.Id);
        }

        public async Task<Res<BlogDTO>> GetOneAsync(int id)
        {
            return await GetOneAsync<Blog, BlogDTO>(id);
        }

        public async Task<Res<PaginationResponse<BlogDTO>>> GetPaginationAsync(PaginationRequest request)
        {
            IQueryable<Blog> query = _context.Blogs.Include(b => b.Section)
                                                   .Select(b => new Blog
                                                   {
                                                       Id = b.Id,
                                                       Name = b.Name,
                                                       Section = b.Section
                                                   })
                                                   .AsQueryable();

            if (!string.IsNullOrEmpty(request.Filter))
            {
                query = query.Where(b => b.Name.ToLower()
                                               .Contains(request.Filter
                                               .ToLower()));
            }

            return await GetPaginationAsync<Blog, BlogDTO>(request, query);
        }
    }
}
