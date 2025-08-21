using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET.ASSIGNMENT.Areas.HouseBroker.Controllers
{
    public class HomeController : Controller
    {
        [Area("HouseBroker")]
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
