using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CoWork454.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcMailingList.Data;

namespace CoWork454.Controllers
{
    public class UserController : BaseController
    {
        private readonly MvcMailingListContext _context;

        public UserController(MvcMailingListContext mvcMailingList)
            : base(mvcMailingList)
        
        {
            _context = mvcMailingList;
        }
        // Login read view
        // /User/
        public IActionResult Index()
        {
            var userId = Convert.ToInt32(GetEncryptedGenericCookie("USER_ID"));
            ViewData["user"] = _context.Users.Find(userId);
            ViewData["resourceBookings"] = _context
                                            .ResourceBookings
                                            .Include(b => b.Resource)
                                            .Where(b => b.Resource.Id == b.ResourceId)
                                            .Where(b => b.UserId == userId)
                                            .Where(b => b.ResourceBookingEnd >= DateTimeOffset.Now)
                                            .ToList();

            return MemberLogin();
        }

        public IActionResult Login()
        {
            return View();
        }

        // Login submit view
        // [POST] /User/
        [HttpPost]
        public IActionResult Index(LoginModel model)
        {
            // lookup the user by email address
            var existingUser = _context.Users.SingleOrDefault(u => u.EmailAddress == model.EmailAddress);
            if (existingUser == null)
            {
                ModelState.AddModelError("EmailAddress", "Either the email address or password was incorrect");
                return View();
            }

            // validate the incoming password with the password hash in the database
            var passwordVerified = BCrypt.Net.BCrypt.Verify(model.Password, existingUser.PasswordHash);

            if (!passwordVerified)
            {
                ModelState.AddModelError("EmailAddress", "Either the email address or password was incorrect");
                return View();
            }

            // if it matches, set a cookie with the userId
            SetEncryptedGenericCookie("USER_ID", existingUser.Id.ToString());

            if (existingUser.IsAdmin)
            {
                return RedirectToAction("Index", "Admin");
            } else { 
             return RedirectToAction("Index", "User");
            }
        }

        // Register read view
        // /User/Register
        public IActionResult Register()
        {
            return View();
        }

        // Register submit view
        // [POST] /User/Register
        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            // check if email already exists in database (if so throw exception)
            var existingUser = _context.Users.SingleOrDefault(u => u.EmailAddress == model.EmailAddress);

            if (existingUser != null)
            {
                ModelState.AddModelError("EmailAddress", "Email Address already exists in database");
                return View();
            }

            // hash the incoming password
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // create a new user record
            var user = new User();
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.EmailAddress = model.EmailAddress;
            user.PasswordHash = passwordHash;
            user.UserImagePath = "https://cowork454.blob.core.windows.net/cowork454container/favicon.png";


            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }

            // redirect to the login
            return RedirectToAction("Index");
        }

        // /User/Logout
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("USER_ID");      
            return RedirectToAction("Index", "Index");
        }


    }
}
