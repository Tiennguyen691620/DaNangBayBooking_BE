using DaNangBayBooking.Application.System.Users;
using DaNangBayBooking.ViewModels.Common;
using DaNangBayBooking.ViewModels.System.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaNangBayBooking.BackendApi.Controllers
{
    //[EnableCors()]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;


        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Đăng nhập dành cho khách hàng
        /// </summary>
        [HttpPost("login-client")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResult<LoginUser>>> LoginClient([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.LoginClient(request);

            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Đăng nhập dành cho admin
        /// </summary>
        [HttpPost("login-admin")] 
        [AllowAnonymous]
        public async Task<ActionResult<ApiResult<LoginUser>>> LoginAdmin([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.LoginAdmin(request);

            if (result==null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Đăng kí tài khoản khách hàng
        /// </summary>
        [HttpPost("register-client")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResult<bool>>> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.Register(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

    }
}
