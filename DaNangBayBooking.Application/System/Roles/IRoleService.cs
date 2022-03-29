using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DaNangBayBooking.ViewModels.System.Roles;

namespace DaNangBayBooking.Application.System.Roles
{
    public interface IRoleService
    {
        Task<List<RoleVm>> GetAll();
    }
}
