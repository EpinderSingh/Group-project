using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoWork454.Models;
using Microsoft.AspNetCore.Mvc;
using MvcMailingList.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoWork454.Controllers
{
    public class BookingController : BaseController
    {
        private readonly MvcMailingListContext _context;

        public BookingController(MvcMailingListContext mvcMailingList)
            : base(mvcMailingList)

        {
            _context = mvcMailingList;
        }

        public IActionResult Index(int id)
        {
            var userIdCookie = GetEncryptedGenericCookie("USER_ID");

            if (userIdCookie == null)
            {
                //can't make a booking without logging in
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["resource"] = _context.Resources.Find(id);
                ViewData["user"] = _context.Users.Find(Convert.ToInt32(userIdCookie));
                
                return View();

            }
        }

        [HttpPost]
        public IActionResult Index(ResourceBooking model)
        {
            //check booking is available
            
            var existingBookings = _context.ResourceBookings
                                    .Where(b => b.ResourceId == model.ResourceId)
                                    .Where(b => b.ResourceBookingStart >= DateTimeOffset.Now)
                                    .ToList();

            if (existingBookings.Count > 0)
            {
                foreach (var booking in existingBookings)
                {   
                    if (booking.ResourceBookingStart <= booking.ResourceBookingEnd && booking.ResourceBookingStart <= booking.ResourceBookingEnd)
                    {
                        ModelState.AddModelError("Duplicate Booking", "Sorry - already booked at this time");
                        return View();
                    }
                }
            }

            model.UserId = Convert.ToInt32(GetEncryptedGenericCookie("USER_ID"));
            model.ResourceBookingTimeCreated = DateTimeOffset.Now;
            _context.ResourceBookings.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index", "User");

        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _context.ResourceBookings.Remove(_context.ResourceBookings.Find(id));
            _context.SaveChanges();

            return View("Index");

        }


    }
}
     
