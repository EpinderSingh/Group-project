using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CoWork454.Common;
using CoWork454.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MvcMailingList.Data;


namespace CoWork454.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsPostApiController : BaseController
    {

        private readonly MvcMailingListContext _context;
        private readonly IConfiguration _configuration;

        public NewsPostApiController(MvcMailingListContext context, IConfiguration configuration) : base(context)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/NewsPostApi
        [HttpGet]
        public List<NewsPost> Get()
        {
            return _context.NewsPosts
                .OrderByDescending(p => p.DateTimePosted)
                .Include(p => p.Author)
                .ToList();
        }

        // GET: api/NewsPostApi/top/5
        [HttpGet("{pos}/{num}")]
        public List<NewsPost> Get(string pos, int num)
        {
            if (pos == "top")
            {
                return _context.NewsPosts
                    .OrderByDescending(p => p.DateTimePosted)
                    .Include(p => p.Author)
                    .Take(num)
                    .ToList();
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
            return _context.NewsPosts
                .Include(p => p.Author)
                .Where(n => n.NewsTag == tag)
                .ToList();
        }


        // POST: api/NewsPostApi
        [HttpPost]
        public void Post(IFormFile file, [FromForm] NewsPost model)
        {
            updateModelValues(file, model);
            _context.NewsPosts.Add(model);
            _context.SaveChanges();
        }

        // PUT: api/NewsPostApi/5
        [HttpPut("{id}")]
        public void Put(int id, IFormFile file, [FromForm] NewsPost model)
        {
            var existingPost = _context.NewsPosts.Find(id);
            string filePath = null;

            if (file != null)
            {
                using (var stream = file.OpenReadStream())
                {
                    var connectionString = _configuration.GetConnectionString("StorageConnection");
                    filePath = AzureStorage.AddUpdateFile(file.FileName, stream, connectionString, "CoWork454Container");
                }

            }
            //retain existing photo if none uploaded
            else if (file == null && existingPost.NewsPhoto != null)
                {
                    filePath = existingPost.NewsPhoto;
                }

            //if nothing use default image
            else 
            {
                filePath = "/images/news_default.jpg";
            }

            existingPost.NewsText = model.NewsText;
            existingPost.NewsTitle = model.NewsTitle;
            existingPost.NewsTag = model.NewsTag;
            existingPost.AuthorId = Convert.ToInt32(GetEncryptedGenericCookie("USER_ID"));
            existingPost.NewsPhoto = filePath;
            existingPost.DateTimePosted = DateTimeOffset.Now;

            _context.NewsPosts.Update(existingPost);
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



        //method to set model values for new & updating post
        public void updateModelValues(IFormFile file, NewsPost model)
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

            model.AuthorId = Convert.ToInt32(GetEncryptedGenericCookie("USER_ID"));
            model.NewsPhoto = filePath;
            model.DateTimePosted = DateTimeOffset.Now;
        }
    }
}
