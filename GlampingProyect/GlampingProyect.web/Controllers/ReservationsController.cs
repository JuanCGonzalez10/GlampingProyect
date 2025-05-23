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

       
    }
}
