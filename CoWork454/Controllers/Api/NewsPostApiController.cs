using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public class NewsPostApiController : ControllerBase
    {

        private readonly MvcMailingListContext _context;
        private readonly IConfiguration _configuration;

        public NewsPostApiController(MvcMailingListContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/NewsPostApi
        [HttpGet]
        public List<NewsPost> Get()
        {
            return _context.NewsPosts.OrderByDescending(p => p.DateTimePosted).ToList();
        }

        // GET: api/NewsPostApi/top/5
        [HttpGet("{pos}/{num}")]
        public List<NewsPost> Get(string pos, int num)
        {
            if (pos == "top")
            {
                return _context.NewsPosts.OrderByDescending(p => p.DateTimePosted).Take(num).ToList();
            }

            return null;
            
        }


        // GET: api/NewsPostApi/5
        [HttpGet("{id}")]
        public NewsPost Get(int id)
        {
            return _context.NewsPosts.SingleOrDefault(n => n.Id == id);
        }

        // GET: api/NewsPostApi/tags/2
        // returns filtered list of news posts by news tag
        [HttpGet("tags/{tag}")]
        public List<NewsPost> Get(NewsTag tag)
        {
            return _context.NewsPosts.Where(n => n.NewsTag == tag).ToList();
        }


        // POST: api/NewsPostApi
        [HttpPost]
        public void Post(IFormFile file, [FromForm] NewsPost model)
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
                filePath = "/images/news_default.jpg";
            }
            model.NewsPhoto = filePath;
            model.DateTimePosted = DateTimeOffset.Now;
            
            _context.NewsPosts.Add(model);
            _context.SaveChanges();
        }

        // PUT: api/NewsPostApi/5
        [HttpPut("{id}")]
        public void Put(int id, IFormFile file, [FromForm] NewsPost model)
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
                filePath = "/images/news_default.jpg";
            }
            model.NewsPhoto = filePath;
            model.DateTimePosted = DateTimeOffset.Now;
            _context.NewsPosts.Update(model);
            _context.SaveChanges();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var post = _context.NewsPosts.SingleOrDefault(n => n.Id == id);
            if (post == null)
            {
                throw new Exception("News Post not found");
            }
            _context.NewsPosts.Remove(post);
            _context.SaveChanges();
        }
    }
}
