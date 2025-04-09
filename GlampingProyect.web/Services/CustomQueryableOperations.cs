﻿using AutoMapper;
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
    public class CustomQueryableOperations
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CustomQueryableOperations(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Res<TDTO>> CreateAsync<TEntity, TDTO>(TDTO dto)
        {
            try
            {
                TEntity entity = _mapper.Map<TEntity>(dto);

                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();

                return ResponseHelper<TDTO>.MakeResponseSuccess(dto, "Registro creado con éxito");
            }
            catch (DbUpdateException dbEx)
            {
                var detailedMessage = $"Error al guardar en la base de datos: {dbEx.InnerException?.Message ?? dbEx.Message}";
                return ResponseHelper<TDTO>.MakeResponseFail(new Exception(detailedMessage));
            }
            catch (AutoMapperMappingException mapEx)
            {
                var detailedMessage = $"Error al mapear el DTO a la entidad: {mapEx.InnerException?.Message ?? mapEx.Message}";
                return ResponseHelper<TDTO>.MakeResponseFail(new Exception(detailedMessage));
            }
            catch (Exception ex)
            {
                var detailedMessage = $"Error inesperado: {ex.InnerException?.Message ?? ex.Message}";
                return ResponseHelper<TDTO>.MakeResponseFail(new Exception(detailedMessage));
            }
        }

        public async Task<Res<object>> DeleteAsync<TEntity>(int id) where TEntity : class, IId
        {
            try
            {
                TEntity? entity = await _context.Set<TEntity>()
                                                .FirstOrDefaultAsync(e => e.Id == id);

                if (entity is null)
                {
                    return ResponseHelper<object>.MakeResponseFail($"No existe registro con id {id}");
                }

                _context.Remove(entity);
                await _context.SaveChangesAsync();

                return ResponseHelper<object>.MakeResponseSuccess("Registro eliminado con éxito");

            }
            catch (Exception ex)
            {
                return ResponseHelper<object>.MakeResponseFail(ex);
            }
        }

        public async Task<Res<TDTO>> EditAsync<TEntity, TDTO>(TDTO dto, int id) where TEntity : class, IId
        {
            try
            {
                TEntity entity = _mapper.Map<TEntity>(dto);
                entity.Id = id;

                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return ResponseHelper<TDTO>.MakeResponseSuccess(dto, "Registro actualizado con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<TDTO>.MakeResponseFail(ex);
            }
        }

        public async Task<Res<TDTO>> GetOneAsync<TEntity, TDTO>(int id) 
        where TEntity : class, IId
        where TDTO : class
        {
            try
            {
                TEntity? entity = await _context.Set<TEntity>()
                                                .FirstOrDefaultAsync(e => e.Id == id);

                if (entity is null)
                {
                    return ResponseHelper<TDTO>.MakeResponseFail($"No existe registro con id {id}");
                }

                TDTO dto = _mapper.Map<TDTO>(entity);

                return ResponseHelper<TDTO>.MakeResponseSuccess(dto, "Registro obtenido con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<TDTO>.MakeResponseFail(ex);
            }
        }
        public async Task<Res<PaginationResponse<TDTO>>> GetPaginationAsync<TEntity, TDTO>(PaginationRequest request, IQueryable<TEntity> query = null)
        where TEntity : class
        where TDTO : class
        {
            try
            {
                if (query is null)
                {
                    query = _context.Set<TEntity>();
                }

                PagedList <TEntity> list = await PagedList<TEntity>.ToPagedListAsync(query, request);

                PaginationResponse<TDTO> response = new PaginationResponse<TDTO>
                {
                    List = _mapper.Map<PagedList<TDTO>>(list),
                    TotalCount = list.TotalCount,
                    RecordsPerPage = list.RecordsPerPage,
                    CurrentPage = list.CurrentPage,
                    TotalPages = list.TotalPages,
                    Filter = request.Filter,
                };

                return ResponseHelper<PaginationResponse<TDTO>>.MakeResponseSuccess(response);
            }
            catch (Exception ex)
            {
                return ResponseHelper<PaginationResponse<TDTO>>.MakeResponseFail(ex);
            }
        }
    }
}
