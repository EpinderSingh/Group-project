using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoWork454.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcMailingList.Data;

namespace CoWork454.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsPostApiController : ControllerBase
    {

        private readonly MvcMailingListContext _context;

        public NewsPostApiController(MvcMailingListContext context)
        {
            _context = context;
        }

        // GET: api/NewsPostApi
        [HttpGet]
        public List<NewsPost> Get()
        {
            return _context.NewsPosts.ToList();
        }

        // GET: api/NewsPostApi/5
        [HttpGet("{id}")]
        public NewsPost Get(int id)
        {
            return _context.NewsPosts.SingleOrDefault(n => n.Id == id);
        }

        // POST: api/NewsPostApi
        [HttpPost]
        public void Post([FromBody] NewsPost model)
        {
            model.DateTimePosted = DateTimeOffset.Now;
            _context.NewsPosts.Add(model);
            _context.SaveChanges();
        }

        // PUT: api/NewsPostApi/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] NewsPost model)
        {
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
