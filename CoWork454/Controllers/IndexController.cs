using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CoWork454.Models;
using MvcMailingList.Data;
using Microsoft.EntityFrameworkCore;

namespace CoWork454.Controllers
{
    public class IndexController : BaseController
    {
        private readonly MvcMailingListContext _context;

        public IndexController(MvcMailingListContext mvcMailingList)
            : base(mvcMailingList)

        {
            _context = mvcMailingList;
        }

        public IActionResult Index()
        {
            ViewData["news__featured"] = _context.NewsPosts
                                            .OrderByDescending(p => p.DateTimePosted)
                                            .Include(p => p.Author)
                                            .Take(4)
                                            .ToList();
            return View();
        }
        public IActionResult Gallery()
        {
            return View();
        }

        public IActionResult News()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
