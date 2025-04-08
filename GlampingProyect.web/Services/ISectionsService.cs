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
using GlampingProyect.web.Data.Entities;

namespace GlampingProyect.Web.Services
{
    public interface ISectionsService
    {
        Task<Res<SectionDTO>> CreateAsync(SectionDTO dto);
        Task<Res<object>> DeleteAsync(int id);
        Task<Res<SectionDTO>> EditAsync(SectionDTO dto);
        Task<Res<List<SectionDTO>>> GetListAsync();
        Task<Res<SectionDTO>> GetOneAsync(int id);
        Task<Res<PaginationResponse<SectionDTO>>> GetPaginationAsync(PaginationRequest request);
        Task<Res<object>> ToggleAsync(ToggleSectionStatusDTO dto);
    }

    public class SectionsService : CustomQueryableOperations, ISectionsService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public SectionsService(DataContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Res<SectionDTO>> CreateAsync(SectionDTO dto)
        {
            return await CreateAsync<Section, SectionDTO>(dto);
        }

        public async Task<Res<object>> DeleteAsync(int id)
        {
            try
            {
                Section? sectionInstance = await _context.Sections.FirstOrDefaultAsync(s => s.Id == id);

                if (sectionInstance is null)
                {
                    return ResponseHelper<object>.MakeResponseFail($"No existe sección con id {id}");
                }

                _context.Sections.Remove(sectionInstance);
                await _context.SaveChangesAsync();

                return ResponseHelper<object>.MakeResponseSuccess("Sección eliminada con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<object>.MakeResponseFail(ex);
            }
        }

        public async Task<Res<SectionDTO>> EditAsync(SectionDTO dto)
        {
            try
            {
                Section? section = await _context.Sections.AsNoTracking()
                                                          .FirstOrDefaultAsync(s => s.Id == dto.Id);

                if (section is null)
                {
                    return ResponseHelper<SectionDTO>.MakeResponseFail($"No existe sección con id {dto.Id}");
                }

                section = _mapper.Map<Section>(dto);
                _context.Update(section);
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
                List<Section> sections = await _context.Sections.ToListAsync();
                List<SectionDTO> list = _mapper.Map<List<SectionDTO>>(sections);

                return ResponseHelper<List<SectionDTO>>.MakeResponseSuccess(list);
            }
            catch (Exception ex)
            {
                return ResponseHelper<List<SectionDTO>>.MakeResponseFail(ex);
            }
        }

        public async Task<Res<SectionDTO>> GetOneAsync(int id)
        {
            try
            {
                Section? section = await _context.Sections.FirstOrDefaultAsync(s => s.Id == id);

                if (section is null)
                {
                    return ResponseHelper<SectionDTO>.MakeResponseFail($"No existe sección con id {id}");
                }

                SectionDTO dto = _mapper.Map<SectionDTO>(section);
                return ResponseHelper<SectionDTO>.MakeResponseSuccess(dto, "Sección obtenida con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<SectionDTO>.MakeResponseFail(ex);
            }
        }

        public async Task<Res<PaginationResponse<SectionDTO>>> GetPaginationAsync(PaginationRequest request)
        {
            try
            {
                IQueryable<Section> query = _context.Sections.AsQueryable();

                if (!string.IsNullOrEmpty(request.Filter))
                {
                    query = query.Where(b => b.Name.ToLower().Contains(request.Filter.ToLower())
                                           || b.Descripcion.ToLower().Contains(request.Filter.ToLower()));
                }

                return await GetPaginationAsync<Section, SectionDTO>(request, query);
            }
            catch (Exception ex)
            {
                return ResponseHelper<PaginationResponse<SectionDTO>>.MakeResponseFail(ex);
            }
        }

        public async Task<Res<object>> ToggleAsync(ToggleSectionStatusDTO dto)
        {
            try
            {
                Section? section = await _context.Sections.FirstOrDefaultAsync(s => s.Id == dto.SectionId);

                if (section is null)
                {
                    return ResponseHelper<object>.MakeResponseFail($"No existe sección con id {dto.SectionId}");
                }

                section.IsHidden = dto.Hide;
                _context.Sections.Update(section);
                await _context.SaveChangesAsync();

                return ResponseHelper<object>.MakeResponseSuccess("Sección actualizada con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<object>.MakeResponseFail(ex);
            }
        }
    }
}
