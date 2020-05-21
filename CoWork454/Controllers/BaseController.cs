using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoWork454.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcMailingList.Data;

namespace CoWork454.Controllers
{
    public class BaseController : Controller
    {

        private readonly MvcMailingListContext _context;

        public BaseController(MvcMailingListContext context)
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

        protected IActionResult AdminLogin()
        {
            var userId = GetEncryptedGenericCookie("USER_ID");
            var isAdmin = _context.Users.Find(Convert.ToInt32(userId)).IsAdmin;
            if (userId != null && isAdmin)
            {
                return View("../Admin/Index");
            }

            return RedirectToAction("Login", "User");
        }

        protected IActionResult MemberLogin()
        {
            var userId = GetEncryptedGenericCookie("USER_ID");
            if (userId != null)
            {
                return View("../User/Index");
            }

            return RedirectToAction("Login", "User");
        }
    }

        
}