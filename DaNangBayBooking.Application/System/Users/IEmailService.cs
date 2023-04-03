using DaNangBayBooking.ViewModels.Common;
using DaNangBayBooking.ViewModels.System.Users;
using System.Threading.Tasks;

namespace Webgentle.BookStore.Service
{
    public interface IEmailService
    {
        Task SendTestEmail(UserEmailOptions userEmailOptions);

        Task SendEmailForgotAndResetPass(UserEmailOptions userEmailOptions);

        Task SendEmailBookRoomToAccommodation(UserEmailOptions userEmailOptions);

        Task SendEmailCancelToAccommodation(UserEmailOptions userEmailOptions);

        Task SendEmailCancelToUser(UserEmailOptions userEmailOptions);

        Task SendEmailSuccessBookingToUser(UserEmailOptions userEmailOptions);


        Task SendEmailForForgotPassword(UserEmailOptions userEmailOptions);

        Task<ApiResult<string>> ResetPassword(UserResetPassRequest request);
        Task<ApiResult<UserForgotPass>> ForgotPassword(string Email);
        Task<ApiResult<bool>> ChangePassword(UserChangePassRequest request);
    }
}