using AutoMapper;
using GlampingProyect.web.Data.Entities;
using Humanizer;
using Library1.Cor;
using Microsoft.EntityFrameworkCore;
using PrivateBlog.Web.Core;
using PrivateBlog.Web.Core.Pagination;
using PrivateBlog.Web.Data;
using PrivateBlog.Web.Data.Entities;
using PrivateBlog.Web.DTOs;
using PrivateBlog.Web.Helpers;
using System.Xml.Linq;



namespace PrivateBlog.Web.Services
{
    public interface ISectionsService
    {
        public Task<Res<SectionDTO>> CreateAsync(SectionDTO dto);
        public Task<Res<object>> DeleteAsync(int id);
        public Task<Res<SectionDTO>> EditAsync(SectionDTO dto);
        public Task<Res<List<SectionDTO>>> GetListAsync();
        public Task<Res<SectionDTO>> GetOneAsync(int id);
        public Task<Res<PaginationResponse<SectionDTO>>> GetPaginationAsync(PaginationRequest request);
        public Task<Res<object>> ToggleAsync(ToggleSectionStatusDTO dto);
    }

    public class SectionsService : CustomQueryableOperations, ISectionsService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public SectionsService(DataContext context, IMapper mapper) : base (context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Res<SectionDTO>> CreateAsync(SectionDTO dto)
        {
           

            return await CreateAsync<Section, SectionDTO>(dto);
        }

        public async Task<Res<object>> deleteasync(int id)
        {
            try
            {
                Section? sectionInstance = await _context.Sections.FirstOrDefaultAsync(s => s.id == id);

                if (sectionInstance is null)
                {
                    return ResponseHelper<object>.MakeResponseFail($"no existe sección con id {id}");
                }

                _context.Sections.Remove(sectionInstance);
                await _context.SaveChangesAsync();

                return ResponseHelper<object>.MakeResponseSuccess("sección eliminada con éxito");

            }
            catch (Exception ex)
            {
                return ResponseHelper<object>.MakeResponseFail(ex);
            }

            Res<object> Response = await DeleteAsync<Section>(id);
            Response.Message = !Response.IsSuccess ? $"La sección con id: {id} no existe" : Response.Message;
            return Response;
        }

        public Task<Res<object>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Res<SectionDTO>> EditAsync(SectionDTO dto)
        {
            try
            {
                Section? Section = await _context.Sections.AsNoTracking()
                                                          .FirstOrDefaultAsync(s => s.Id == dto.Id);

                if (Section is null)
                {
                    return ResponseHelper<Section>.MakeResponseFail($"No existe sección con id {dto.Id}");
                }

                Section = _mapper.Map<Section>(dto);
                _context.Update(Section);
                await _context.SaveChangesAsync();

                return ResponseHelper<SectionDTO>.MakeResponseSuccess(dto, "Sección actualizada con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<SectionDTO>.MakeResponseFail(ex);
            }

            
        }

        public async Task<Res<List<SectionDTO>>> GetListAsync()
        {
            try
            {
                List<Section> Sections = await _context.Sections.ToListAsync();

                List<SectionDTO> list = _mapper.Map<List<SectionDTO>>(Sections);

                return ResponseHelper<List<SectionDTO>>.MakeResponseSuccess(list);
            }
            catch(Exception ex)
            {
                return ResponseHelper<List<SectionDTO>>.MakeResponseFail(ex);
            }
        }

        public async Task<Res<SectionDTO>> getoneasync(int id)
        {
            try
            {
                Section? section = await _context.Sections.FirstOrDefaultAsync(s => s.id == id);

                if (section is null)
                {
                    return ResponseHelper<SectionDTO>.MakeResponseFail($"no existe sección con id {id}");
                }

                SectionDTO dto = _mapper.Map<SectionDTO>(section);

                //_context.entry(section).state = entitystate.unchanged;

                return ResponseHelper<SectionDTO>.MakeResponseSuccess(dto, "sección obtenida con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<SectionDTO>.MakeResponseFail(ex);
            }

        }

        public Task<Res<SectionDTO>> GetOneAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Res<PaginationResponse<SectionDTO>>> GetPaginationAsync(PaginationRequest request)
        {
            try
            {
                IQueryable<Section> query = _context.Sections.AsQueryable();

                PagedList<Section> list = await PagedList<Section>.ToPagedListAsync(query, request);

                PaginationResponse<SectionDTO> response = new PaginationResponse<SectionDTO>
                {
                    List = _mapper.Map<PagedList<SectionDTO>>(list),
                    TotalCount = list.TotalCount,
                    RecordsPerPage = list.RecordsPerPage,
                    CurrentPage = list.CurrentPage,
                    TotalPages = list.TotalPages,
                };

                return ResponseHelper<PaginationResponse<SectionDTO>>.MakeResponseSuccess(response);
            }
            catch (Exception ex)
            {
                return ResponseHelper<PaginationResponse<SectionDTO>>.MakeResponseFail(ex);
            }


            IQueryable<Section> query = _context.Sections.AsQueryable();

            if (!string.IsNullOrEmpty(request.Filter))
            {
                query = query.Where(b => b.Name.ToLower().Contains(request.Filter.ToLower())
                                      || b.Description.ToLower().Contains(request.Filter.ToLower()));
            }

            return await GetPaginationAsync<Section, SectionDTO>(request, query);

        }

        public async Task<Res<object>> ToggleAsync(ToggleSectionStatusDTO dto)
        {
            try
            {
                SectionDTO? Section = await _context.Sections.FirstOrDefaultAsync(s => s.Id == dto.SectionId);

                if (Section is null)
                {
                    return ResponseHelper<object>.MakeResponseFail($"No existe sección con id {dto.SectionId}");
                }

                Section.IsHidden = dto.Hide;
                _context.Sections.Update(Section);
                await _context.SaveChangesAsync();

                return ResponseHelper<object>.MakeResponseSuccess("Sección actualizada con éxito");
            }
            catch(Exception ex)
            {
                return ResponseHelper<object>.MakeResponseFail(ex);
            }
        }

    }
}
