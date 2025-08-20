using Microsoft.AspNetCore.Mvc;

namespace ASP.NET.ASSIGNMENT.Areas.HouseBroker.Controllers
{
    public class PropertyController : Controller
    {
        [Area("HouseBroker")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
