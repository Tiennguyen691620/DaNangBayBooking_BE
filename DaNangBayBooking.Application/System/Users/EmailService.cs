using DaNangBayBooking.ViewModels.Common;
using DaNangBayBooking.ViewModels.System.Users;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
//using MailKit.Net.Smtp;
//using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
//using System.Net.Mail.SmptClient;
using System.Text;
using System.Threading.Tasks;

namespace Webgentle.BookStore.Service
{
    public class EmailService : IEmailService
    {
        private const string templatePath = @"EmailTemplate/{0}.html";
        private readonly SMTPConfigModel _smtpConfig;

        private readonly string _userContentFolder;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        private const string EMAILTEMPLATE_CONTENT_FOLDER_NAME = "EmailTemplate";
        private const string ASSETS_CONTENT_FOLDER_NAME = "assets";

        public EmailService(IWebHostEnvironment webHostEnvironment, IConfiguration configuration, IOptions<SMTPConfigModel> smtpConfig)
        {
            _configuration = configuration;
            _userContentFolder = Path.Combine(webHostEnvironment.WebRootPath, Path.Combine(EMAILTEMPLATE_CONTENT_FOLDER_NAME));
            _webHostEnvironment = webHostEnvironment;
            _smtpConfig = smtpConfig.Value;
        }

        /*public EmailService(IOptions<SMTPConfigModel> smtpConfig)
        {
            _smtpConfig = smtpConfig.Value;
        }*/


        public string GetFileUrl(string fileName)
        {
            return $"/{EMAILTEMPLATE_CONTENT_FOLDER_NAME}/{fileName}";
        }

        public async Task SendTestEmail(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceHolders("Hello {{FullName}}, This is test email subject from book store web app", userEmailOptions.PlaceHolders);
           
            userEmailOptions.Body = UpdatePlaceHolders(GetEmailBody("TestEmail"), userEmailOptions.PlaceHolders);

            await SendEmail(userEmailOptions);
        }

        public async Task SendEmailForgotAndResetPass(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceHolders("Xin Chào {{FullName}}!, bạn có thông báo đổi mật khẩu mới", userEmailOptions.PlaceHolders);

            userEmailOptions.Body = UpdatePlaceHolders(GetEmailBody("EmailForgotAndResetPassword"), userEmailOptions.PlaceHolders);

            await SendEmail(userEmailOptions);
        }

        public async Task SendEmailBookRoomToAccommodation(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceHolders("Xin chào {{Name}}, bạn có thông tin xác nhận đặt phòng !", userEmailOptions.PlaceHolders);

            userEmailOptions.Body = UpdatePlaceHolders(GetEmailBody("SendEmailBookRoomToAccommodation"), userEmailOptions.PlaceHolders);

            await SendEmail(userEmailOptions);
        }
        
        public async Task SendEmailCancelToAccommodation(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceHolders("Xin chào {{Name}}, bạn có thông tin hủy đặt phòng !", userEmailOptions.PlaceHolders);

            userEmailOptions.Body = UpdatePlaceHolders(GetEmailBody("SendEmailCancelToAccommodation"), userEmailOptions.PlaceHolders);

            await SendEmail(userEmailOptions);
        }

        public async Task SendEmailCancelToUser(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceHolders("Xin chào {{FullName}}, bạn có thông tin hủy đặt phòng !", userEmailOptions.PlaceHolders);

            userEmailOptions.Body = UpdatePlaceHolders(GetEmailBody("SendEmailCancelToUser"), userEmailOptions.PlaceHolders);

            await SendEmail(userEmailOptions);
        }
        
        public async Task SendEmailSuccessBookingToUser(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceHolders("Xin chào {{FullName}}, bạn có thông tin về xác nhận đặt phòng !", userEmailOptions.PlaceHolders);

            userEmailOptions.Body = UpdatePlaceHolders(GetEmailBody("SendEmailSuccessBookingToUser"), userEmailOptions.PlaceHolders);

            await SendEmail(userEmailOptions);
        }

        public async Task SendEmailForForgotPassword(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceHolders("Hello {{FullName}}, reset your password.", userEmailOptions.PlaceHolders);

            userEmailOptions.Body = UpdatePlaceHolders(GetEmailBody("ForgotPassword"), userEmailOptions.PlaceHolders);

            await SendEmail(userEmailOptions);
        }

        
        private async Task SendEmail(UserEmailOptions userEmailOptions)
        {
            //var email = new MimeMessage();
            /*email.From.Add(MailboxAddress.Parse(_smtpConfig.SenderAddress));*/

            /*email.Subject = userEmailOptions.Subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = userEmailOptions.Body };

            using var smtp = new SmtpClient();
            smtp.Connect(_smtpConfig.Host, _smtpConfig.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_smtpConfig.UserName, _smtpConfig.Password);*/

            MailMessage mail = new MailMessage
            {
                Subject = userEmailOptions.Subject,
                Body = userEmailOptions.Body,
                From = new MailAddress(_smtpConfig.SenderAddress, _smtpConfig.SenderDisplayName),
                IsBodyHtml = _smtpConfig.IsBodyHTML
            };

            foreach (var toEmail in userEmailOptions.ToEmails)
            {
                mail.To.Add(toEmail);
            }

            NetworkCredential networkCredential = new NetworkCredential(_smtpConfig.UserName, _smtpConfig.Password);

            SmtpClient smtpClient = new SmtpClient()
            {
                Host = _smtpConfig.Host,
                Port = _smtpConfig.Port,
                EnableSsl = _smtpConfig.EnableSSL,
                UseDefaultCredentials = _smtpConfig.UseDefaultCredentials,
                Credentials = networkCredential
            };

            mail.BodyEncoding = Encoding.Default;

            await smtpClient.SendMailAsync(mail);
            /*smtp.Send(email);
            smtp.Disconnect(true);*/
        }

        public string GetEmailBody(string templateName)
        {
            var body = File.ReadAllText($"{_userContentFolder}/{templateName}.html");
            //var body = File.ReadAllText(string.Format(templatePath, templateName));
            return body;
        }

        public string UpdatePlaceHolders(string text, List<KeyValuePair<string, string>> keyValuePairs)
        {
            if (!string.IsNullOrEmpty(text) && keyValuePairs != null)
            {
                foreach (var placeholder in keyValuePairs)
                {
                    if (text.Contains(placeholder.Key))
                    {
                        text = text.Replace(placeholder.Key, placeholder.Value);
                    }
                }
            }

            return text;
        }

        public Task<ApiResult<string>> ResetPassword(UserResetPassRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<UserForgotPass>> ForgotPassword(string Email)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<bool>> ChangePassword(UserChangePassRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
