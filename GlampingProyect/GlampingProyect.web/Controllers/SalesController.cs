using AspNetCoreHero.ToastNotification.Abstractions;
using GlampingProyect.Web.Helpers;
using GlampingProyect.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace GlampingProyect.Web.Controllers
{
    public class SalesController : Controller
    {
        private readonly ISalesService _salesService;
        private readonly INotyfService _notyfService;
        private readonly ICombosHelper _combosHelper;

        public SalesController(ISalesService salesService, INotyfService notyfService, ICombosHelper combosHelper)
        {
            _salesService = salesService;
            _notyfService = notyfService;
            _combosHelper = combosHelper;
        }
    }
}
