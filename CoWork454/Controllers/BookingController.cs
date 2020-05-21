using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        // GET: /<controller>/
        public IActionResult Index()
        {   
            var userIdCookie = GetEncryptedGenericCookie("USER_ID");

            if (userIdCookie == null)
            {
                //can't make a booking without logging in
                return RedirectToAction("Index", "Home");
            } else
            {
                
                return View();
            }


            
        }
    }
}
