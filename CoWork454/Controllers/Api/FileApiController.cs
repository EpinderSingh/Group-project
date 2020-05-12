using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoWork454.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileApiController : ControllerBase
    {
        // GET: api/FileApi
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/FileApi/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/FileApi
        [HttpPost]
        public void Post([FromBody] IFormFile file)
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
        }

        // PUT: api/FileApi/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
