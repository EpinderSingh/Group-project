using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcMailingList.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoWork454.Controllers
{
    public class AdminController : BaseController
    {
        private readonly MvcMailingListContext _context;

        public AdminController(MvcMailingListContext mvcMailingList)
            : base(mvcMailingList)

        {
            _context = mvcMailingList;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {

           return AdminLogin();

        }

        public IActionResult Resources()
        {

            return View();

        }

        public IActionResult Bookings()
        {

            return View();

        }

        public IActionResult MailingList()
        {
            return View();
        }

        public IActionResult Users()
        {
            return View();
        }


    }
}
