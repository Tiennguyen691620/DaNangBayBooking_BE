using DaNangBayBooking.ViewModels.Common;
using DaNangBayBooking.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DaNangBayBooking.ApiIntegration
{
    public interface IUserApiClient
    {
        Task<ApiResult<string>> Authenticate(LoginRequest request);
    }
}
