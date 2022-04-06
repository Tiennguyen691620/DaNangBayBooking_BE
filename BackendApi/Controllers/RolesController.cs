using DaNangBayBooking.Application.System.Roles;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaNangBayBooking.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// Lấy tất cả quyền
        ///</summary>
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllData()
        {
            var roles = await _roleService.GetAllData();
            return Ok(roles);
        }
    }
}
