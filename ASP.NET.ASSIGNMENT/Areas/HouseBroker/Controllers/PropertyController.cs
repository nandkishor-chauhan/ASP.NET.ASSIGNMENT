using ASP.NET.HouseBrokerAPP.DAL;
using ASP.NET.HouseBrokerAPP.IServices;
using ASP.NET.HouseBrokerAPP.Models;
using ASP.NET.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASP.NET.ASSIGNMENT.Areas.HouseBroker.Controllers
{
    [Area("HouseBroker")]
    [Authorize(Roles = StaticDetail.Role_Broker)]
    public class PropertyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPropertyService _propertyService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PropertyController(UserManager<IdentityUser> userManager,
            IUnitOfWork unitOfWork,
            IPropertyService propertyService,
            IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _propertyService = propertyService;
            _webHostEnvironment = webHostEnvironment;

        }

        public IActionResult Index()
        {
            return View();
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
        public async Task<IActionResult> Create(Property viewModel, IFormFile? file)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, @"images\property");

                if (!string.IsNullOrEmpty(viewModel.ImageUrl))
                {
                    //delete oldImage
                    var oldImagePath =
                        Path.Combine(wwwRootPath, viewModel.ImageUrl.TrimStart('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                viewModel.ImageUrl = @"\images\property\" + fileName;
            }

            var property = new Property
            {
                Id = Guid.NewGuid(),
                PropertyType = viewModel.PropertyType,
                Description = viewModel.Description,
                Price = viewModel.Price,
                Location = viewModel.Location,
                ImageUrl = viewModel.ImageUrl,
                Features = viewModel.Features,
            };
            TempData["success"] = "Propert created successfully";

            await _propertyService.Create(property);

            return RedirectToAction("Index");
        }


        #region API CALLS
        public async Task<JsonResult> Get()
        {
            try
            {
                var propertyList = await _propertyService.Get();
                return Json(new { success = true, data = propertyList });
            }
            catch (Exception ex)
            {
                // Return error message directly without a custom handler
                return Json(new { success = false, error = ex.Message });
            }
        }

        #endregion
    }
}
