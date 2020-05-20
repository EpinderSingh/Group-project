using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcMailingList.Data;

namespace CoWork454.Controllers
{
    public class ResourceController : BaseController
    {
        private readonly MvcMailingListContext _context;

        public ResourceController(MvcMailingListContext mvcMailingList)
            : base(mvcMailingList)

        {
            _context = mvcMailingList;
        }
        // GET: Resource
        public ActionResult Index()
        {
            ViewData["resources"] = _context.Resources
                                .ToList();
            return View();
        }

        // GET: Resource/Details/5
        public ActionResult Details(int id)
        {
            ViewData["resource"] = _context.Resources
                                .Find(id);
            return View();
        }
    }
}