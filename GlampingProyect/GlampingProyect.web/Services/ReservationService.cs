using System.Collections.Generic;
using System.Threading.Tasks;
using GlampingProyect.Web.Data.Entities;
using GlampingProyect.Web.Data.DTOs;
using Microsoft.EntityFrameworkCore;
using GlampingProyect.Web.Data;

namespace GlampingProyect.Web.Services
{
    public class ReservationService : IReservationService
    {
        private readonly DataContext _context;

        public ReservationService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<ReservationDTO>> GetAllReservationsAsync()
        {
            return await _context.Reservations
                .Select(r => new ReservationDTO
                {
                    Id = r.Id,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    NumberOfPeople = r.NumberOfPeople,
                    Status = r.Status,
                    PaymentStatus = r.PaymentStatus,
                    GlampingUnitId = r.GlampingUnitId
                }).ToListAsync();
        }

        public async Task<ReservationDTO> GetReservationByIdAsync(int id)
        {
            var r = await _context.Reservations.FindAsync(id);
            if (r == null) return null;

            return new ReservationDTO
            {
                Id = r.Id,
                StartDate = r.StartDate,
                EndDate = r.EndDate,
                NumberOfPeople = r.NumberOfPeople,
                Status = r.Status,
                PaymentStatus = r.PaymentStatus,
                GlampingUnitId = r.GlampingUnitId
            };
        }

        public async Task CreateReservationAsync(ReservationDTO dto)
        {
            var reservation = new Reservation
            {
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                NumberOfPeople = dto.NumberOfPeople,
                Status = dto.Status,
                PaymentStatus = dto.PaymentStatus,
                GlampingUnitId = dto.GlampingUnitId
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReservationAsync(ReservationDTO dto)
        {
            var reservation = await _context.Reservations.FindAsync(dto.Id);
            if (reservation == null) return;

            reservation.StartDate = dto.StartDate;
            reservation.EndDate = dto.EndDate;
            reservation.NumberOfPeople = dto.NumberOfPeople;
            reservation.Status = dto.Status;
            reservation.PaymentStatus = dto.PaymentStatus;
            reservation.GlampingUnitId = dto.GlampingUnitId;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteReservationAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null) return;

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
        }
    }
}
