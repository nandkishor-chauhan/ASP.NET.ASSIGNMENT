using ASP.NET.HouseBrokerAPP.DAL;
using ASP.NET.HouseBrokerAPP.IServices;
using ASP.NET.HouseBrokerAPP.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.NET.HouseBrokerAPP.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        public PropertyService(UserManager<IdentityUser> userManager,IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public Task<object> GetCreateData()
        {
            throw new NotImplementedException();
        }
        public Task<object> Get()
        {
            throw new NotImplementedException();
        }

        public Task<Property> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<object>> GetCategoryNameList()
        {
            throw new NotImplementedException();
        }

        public async Task<Property> Create(Property entity)
        {
            //var currentUser = await _userManager.GetUserAsync(User);
            entity.UserId = _unitOfWork.GetCurrentUserName() ?? throw new InvalidOperationException("No logged-in user."); ;
            _unitOfWork.PropertyRepository.Insert(entity);
            await _unitOfWork.SaveAsync();

            return entity;
        }

        public Task<Property> Edit(Property entity)
        {
            throw new NotImplementedException();
        }

    }
}
