using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DaNangBayBooking.ViewModels.Common;
using DaNangBayBooking.ViewModels.System.Roles;

namespace DaNangBayBooking.Application.System.Roles
{
    public interface IRoleService
    {
        //Task<List<RoleVm>> GetAll();

        Task<ApiResult<List<RoleVm>> > GetAllData(); 
    }
}
