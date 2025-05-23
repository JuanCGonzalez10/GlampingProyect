using System.Collections.Generic;
using System.Threading.Tasks;
using GlampingProyect.Web.Data.DTOs;

namespace GlampingProyect.Web.Services
{
    public interface IReservationService
    {
        Task<List<ReservationDTO>> GetAllReservationsAsync();
        Task<ReservationDTO> GetReservationByIdAsync(int id);
        Task CreateReservationAsync(ReservationDTO reservation);
        Task UpdateReservationAsync(ReservationDTO reservation);
        Task DeleteReservationAsync(int id);
    }
}
