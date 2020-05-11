using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoWork454.Controllers
{
    public class FileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new Exception("File empty!");
            }

            var appDataDirectory = AppDomain.CurrentDomain.GetData("DataDirectory");
            var filePath = Path.Combine(appDataDirectory.ToString(), file.FileName);
            using (var stream = System.IO.File.Create(filePath))
            {
                file.CopyTo(stream);
            } // stream.Dispose();

            return View();
        }
    }
}
