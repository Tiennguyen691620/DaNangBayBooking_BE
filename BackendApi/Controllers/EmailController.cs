using DaNangBayBooking.ViewModels.Common;
using DaNangBayBooking.ViewModels.System.Users;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webgentle.BookStore.Service;

namespace DaNangBayBooking.BackendApi.Controllers
{
    [EnableCors()]
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService  _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public IActionResult SendEmail(string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("tiennguyen691620@gmail.com"));
            email.To.Add(MailboxAddress.Parse("danangbaybooking@gmail.com"));
            email.Subject = "tesst email subject";
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("tiennguyen691620@gmail.com", "ouzbpbxdtxkaogya");
            //smtp.Authenticate("danangbaybooking@gmail.com", "fvpbmtwskyimarbc");
            smtp.Send(email);
            smtp.Disconnect(true);

            return Ok();
        }


        /*/// <summary>
        /// quên mật khẩu
        /// </summary>
        [HttpPut("forgot-password")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResult<UserForgotPass>>> ForgotPassword([FromQuery] string Email)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _emailService.ForgotPassword(Email);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }*/
    }
}
