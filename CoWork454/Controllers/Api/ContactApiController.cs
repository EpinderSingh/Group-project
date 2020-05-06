using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoWork454.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;

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
            Execute(model).Wait();
        }

        static async Task Execute(ContactForm model)
        {
            //need to move API key out of file ****
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("test@cowork454.com", "CoWork454 Superuser");
            var subject = model.SubjectLine;
            var to = new EmailAddress("lauraduggan89@gmail.com", "Laura Duggan");
            var plainTextContent = model.MessageBody;
            var htmlContent = model.MessageBody;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
