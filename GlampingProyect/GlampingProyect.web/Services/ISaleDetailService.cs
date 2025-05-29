using AutoMapper;
using GlampingProyect.Data;
using GlampingProyect.Web.Data.Entities;
using GlampingProyect.Web.DTOs;
using GlampingProyect.Web.Services;
using Microsoft.EntityFrameworkCore;
using GlampingProyect.Web.Core;


namespace GlampingProyect.Web.Services
{
    public interface ISaleDetailService
    {
        public Task<Response<SaleDetailDTO>> CreateAsync(SaleDetailDTO dto);
        public Task<Response<object>> DeleteAsync(int id);
        public Task<Response<List<SaleDetailDTO>>> GetBySaleIdAsync(int saleId);
    }

    public class SaleDetailService : CustomQueryableOperations, ISaleDetailService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public SaleDetailService(DataContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Response<SaleDetailDTO>> CreateAsync(SaleDetailDTO dto)
        {
            return await CreateAsync<SaleDetail, SaleDetailDTO>(dto);
        }
        public async Task<Response<object>> DeleteAsync(int id)
        {
            Response<object> response = await DeleteAsync<SaleDetail>(id);
            response.Message = !response.IsSuccess ? $"El detalle de venta con id: {id} no existe" : response.Message;
            return response;
        }

        public async Task<Response<List<SaleDetailDTO>>> GetBySaleIdAsync(int saleId)
        {
            List<SaleDetail> saleDetails = await _context.SaleDetails
        .Where(sd => sd.IdSale == saleId)
        .ToListAsync();

            List<SaleDetailDTO> saleDetailDTOs = new List<SaleDetailDTO>();

            foreach (SaleDetail detail in saleDetails)
            {
                SaleDetailDTO dto = new SaleDetailDTO
                {
                    Id = detail.Id,
                    IdSale = detail.IdSale,
                    SaleDetailCode = detail.SaleDetailCode,
                    SaleDetailProductCode = detail.SaleDetailProductCode,
                    SaleDetailProductQuantity = detail.SaleDetailProductQuantity,
                    SaleDetailProductPrice = detail.SaleDetailProductPrice,
                    SaleDetailSubtotal = detail.SaleDetailSubtotal,
                    SaleDetailProductTax = detail.SaleDetailProductTax
                };
                saleDetailDTOs.Add(dto);
            }

            return new Response<List<SaleDetailDTO>>
            {
                IsSuccess = true,
                Message = "Detalles de venta obtenidos correctamente.",
                Result = saleDetailDTOs
            };
        }
    }
}
