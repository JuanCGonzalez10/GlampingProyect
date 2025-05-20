using Microsoft.AspNetCore.Mvc;
using GlampingProyect.Web.DTOs;
using GlampingProyect.Web.Services;

namespace GlampingProyect.Web.Controllers
{
    public class ApplicationLogsController : Controller
    {
        private readonly IReadLogsService _readLogsService;

        public ApplicationLogsController(IReadLogsService readLogsService)
        {
            _readLogsService = readLogsService;
        }

        public IActionResult Index(DateTime? date)
        {
            LogViewerDTO dto = new LogViewerDTO
            {
                Logs = _readLogsService.GetLogs(date),
                SelectedDate = date
            };

            return View(dto);
        }
    }
}
