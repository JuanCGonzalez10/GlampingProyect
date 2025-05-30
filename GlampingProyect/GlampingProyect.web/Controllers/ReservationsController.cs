using System.Threading.Tasks;
using GlampingProyect.Web.Data.DTOs;
using GlampingProyect.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace GlampingProyect.Web.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public async Task<IActionResult> Index()
        {
            var reservations = await _reservationService.GetAllReservationsAsync();
            return View(reservations);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReservationDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _reservationService.CreateReservationAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        // GET: Editar una reserva
        public async Task<IActionResult> Edit(int id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Guardar cambios de edición
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ReservationDTO dto)
        {
            if (id != dto.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(dto);

            await _reservationService.UpdateReservationAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        // GET: Confirmación para eliminar
        public async Task<IActionResult> Delete(int id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Eliminar la reserva
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _reservationService.DeleteReservationAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
