using System;
using CoWork454.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcMailingList.Data;

namespace CoWork454.Controllers.Api
{
    public class BaseApiController : ControllerBase
    {
        private readonly MvcMailingListContext _context;

        public BaseApiController(MvcMailingListContext context)
        {
            _context = context;
        }

        protected void SetEncryptedGenericCookie(string cookieKey, string cookieValue)
        {
            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTimeOffset.MaxValue;

            var encryptedCookieContent = EncryptionHelper.EncryptString(cookieValue, EncryptionHelper.EncryptionKey);

            HttpContext.Response.Cookies.Append(cookieKey, encryptedCookieContent, cookieOptions);
        }

        protected string GetEncryptedGenericCookie(string cookieKey)
        {
            var encryptedGenericCookie = HttpContext.Request.Cookies[cookieKey];

            if (encryptedGenericCookie == null)
            {
                return null;
            }

            var cookieContent = EncryptionHelper.DecryptString(encryptedGenericCookie, EncryptionHelper.EncryptionKey);

            return cookieContent;
        }
    }
}
