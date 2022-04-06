using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaNangBayBooking.Data.Entities;
using DaNangBayBooking.ViewModels.Common;
using DaNangBayBooking.ViewModels.System.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DaNangBayBooking.Application.System.Roles
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task <ApiResult<List<RoleVm>>> GetAllData()
        {
            var roles = await _roleManager.Roles
                .Select(x => new RoleVm()
                {
                    RoleID = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }).ToListAsync();

            return new ApiSuccessResult<List<RoleVm>>(roles);
        }
    }
}
