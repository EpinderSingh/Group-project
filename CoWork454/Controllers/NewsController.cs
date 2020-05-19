using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcMailingList.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoWork454.Controllers
{
    public class NewsController : Controller
    {
        private readonly MvcMailingListContext _context;

        public NewsController(MvcMailingListContext mvcMailingList)

        {
            _context = mvcMailingList;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewData["news"] = _context.NewsPosts.OrderByDescending(p => p.DateTimePosted).ToList();
            return View();
        }

        public IActionResult Details(int id)
        {
            ViewData["newspost"] = _context.NewsPosts.Find(id);
            return View();
        }

        
    }
}
