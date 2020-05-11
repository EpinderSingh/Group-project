using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoWork454.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace CoWork454.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactApiController : ControllerBase
    {
        // POST: api/ContactApi
        [HttpPost]
        public void Post([FromBody] ContactForm model)
        {
            var fromAddress = new MailAddress("cowork454team2@gmail.com", "CoWork454");
            var toAddress = new MailAddress("cowork454team2@gmail.com", "CoWork454");
            const string fromPassword = "&u},IRX<A+hv8Tsw";
            var subject = $"New Contact from Contact Form: {model.SubjectLine}";
            var body = $"New message from: {model.FirstName} {model.LastName}" +
                $"<br /> Email address: {model.EmailAddress}" +
                $"<br /> Phone number: {model.PhoneNumber}" +
                $"<br /> Message: {model.MessageBody}";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
        })
            {
                smtp.Send(message);
            }
        }

    }
}
