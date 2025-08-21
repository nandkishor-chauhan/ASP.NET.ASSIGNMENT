using ASP.NET.HouseBrokerAPP.DAL;
using ASP.NET.HouseBrokerAPP.IServices;
using ASP.NET.HouseBrokerAPP.Models;
using ASP.NET.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET.ASSIGNMENT.Areas.HouseBroker.Controllers
{
    [Area("HouseBroker")]
    [Authorize(Roles = StaticDetail.Role_Broker)]
    public class PropertyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPropertyService _propertyService;
        public PropertyController(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork, IPropertyService propertyService)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _propertyService = propertyService;

        }

        public IActionResult Index()
        {
            List<Property> propertyList = _unitOfWork.PropertyRepository.Get().ToList();
            return View(propertyList);
        }
        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Property viewModel)
        {
           /* if (ModelState.IsValid)
            {*/
                var property = new Property
                {
                    Id = Guid.NewGuid(),
                    PropertyType = viewModel.PropertyType,
                    Description = viewModel.Description,
                    Price = viewModel.Price,
                    Location = viewModel.Location,
                    Features = viewModel.Features,
                };
                TempData["success"] = "Propert created successfully";

                await _propertyService.Create(property);

                return RedirectToAction("Index");
            //}
            //return View();
        }


    }
}
