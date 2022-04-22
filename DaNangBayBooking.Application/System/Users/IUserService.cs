using DaNangBayBooking.ViewModels.Common;
using DaNangBayBooking.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DaNangBayBooking.Application.System.Users
{
    public interface IUserService
    {
        Task<ApiResult<LoginUser>> LoginAdmin(LoginRequest request);
        Task<ApiResult<LoginUser>> LoginClient(LoginRequest request);
        Task<ApiResult<bool>> Register(RegisterRequest request);
        Task<ApiResult<PagedResult<UserVm>>> GetCustomerAllPaging(GetUserPagingRequest request);
        Task<ApiResult<PagedResult<UserVm>>> GetAdminAllPaging(GetUserPagingRequest request);
        Task<ApiResult<UserVm>> GetById(Guid id);
        Task<ApiResult<bool>> CreateAdmin(CreateAdminRequest request);
    }
}
