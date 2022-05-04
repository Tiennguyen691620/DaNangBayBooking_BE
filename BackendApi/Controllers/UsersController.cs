 using DaNangBayBooking.Application.System.Users;
using DaNangBayBooking.Data.Enums;
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


        /// <summary>
        /// Lấy danh sách tài khoản khách hàng phân trang 
        /// </summary>
        /// 
        [HttpGet("filter/customer")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedResult<UserVm>>> GetUsersAllPaging([FromQuery] GetUserPagingRequest request)
        {
            var user = await _userService.GetCustomerAllPaging(request);
            return Ok(user);
        }

        /// <summary>
        /// Trạng thái tài khoản của Client
        /// </summary>
        /// 
        [HttpPut("update/{UserClientID}/status/client")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResult<bool>>> UpdateStatusClient(Guid UserClientID, bool Status)
        {
            var UserAdmin = await _userService.UpdateStatusClient(UserClientID, Status);
            return Ok(UserAdmin);
        }



        /// <summary>
        /// Tạo tài khoản Admin
        /// </summary>
        [HttpPost("create/admin")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResult<bool>>> CreateAdmin([FromBody] CreateAdminRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.CreateAdmin(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        /// <summary>
        /// Lấy danh sách  tài khoản admin phân trang 
        /// </summary>
        /// 
        [HttpGet("filter/admin")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedResult<UserVm>>> Customer([FromQuery] GetUserPagingRequest request)
        {
            var user = await _userService.GetAdminAllPaging(request);
            return Ok(user);
        }

        /// <summary>
        /// Trạng thái tài khoản của Admin
        /// </summary>
        /// 
        [HttpPut("update/{UserAdminID}/status/admin")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResult<bool>>> UpdateStatusAdmin( Guid UserAdminID, bool Status)
        {
            var UserAdmin = await _userService.UpdateStatusAdmin(UserAdminID, Status);
            return Ok(UserAdmin);
        }

        /// <summary>
        /// Lấy thông tin chi tiết tài khoản
        /// </summary>
        /// 
        [HttpGet("get-by-id/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<UserVm>> GetById(Guid id)
        {
            var user = await _userService.GetById(id);
            return Ok(user);
        }
    }
}
