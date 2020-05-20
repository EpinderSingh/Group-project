using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoWork454.Common;
using CoWork454.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MvcMailingList.Data;

namespace CoWork454.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceApiController : BaseController
    {

        private readonly MvcMailingListContext _context;
        private readonly IConfiguration _configuration;

        public ResourceApiController(MvcMailingListContext context, IConfiguration configuration) : base(context)
        {
            _context = context;
            _configuration = configuration;
        }
        // GET: api/ResourceApi
        [HttpGet]
        public IEnumerable<Resource> Get()
        {
            return _context.Resources.ToList();
        }

        // GET: api/ResourceApi/5
        [HttpGet("{id}")]
        public Resource Get(int id)
        {
            return _context.Resources.Find(id);
        }

        // POST: api/ResourceApi
        [HttpPost]
        public void Post(IFormFile file, [FromForm] Resource model)
        {
            string filePath = null;

            if (file != null)
            {
                using (var stream = file.OpenReadStream())
                {
                    var connectionString = _configuration.GetConnectionString("StorageConnection");
                    filePath = AzureStorage.AddUpdateFile(file.FileName, stream, connectionString, "CoWork454Container");
                }

            }
            else
            {
                filePath = "/images/resource_default.jpg";
            }

            model.ResourceImage = filePath;
            
            _context.Add(model);
            _context.SaveChanges();
        }

        // PUT: api/ResourceApi/5
        [HttpPut("{id}")]
        public void Put(int id, IFormFile file, [FromForm] Resource model)
        {
            _context.Update(model);
            _context.SaveChanges();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var resource = _context.Resources.Find(id);
            if (resource == null)
            {
                throw new Exception("Resource not found");
            }
            _context.Resources.Remove(resource);
            _context.SaveChanges();
        }
    }
}
