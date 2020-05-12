using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcMailingList.Data;
using CoWork454.Models;
namespace CoWork454.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailingListApiController : ControllerBase
    {
        private readonly MvcMailingListContext _context;
        public MailingListApiController(MvcMailingListContext context)
        {
            _context = context;
        }
        // GET: api/MailingListApi
        [HttpGet]
        public IEnumerable<MailingList> Get()
        {
            return _context.MailingList.ToList();
        }
        // GET: api/MailingListApi/5
        [HttpGet("{id}", Name = "Get")]
        public MailingList Get(int id)
        {
            return _context.MailingList.Find(id);
        }
        // POST: api/MailingListApi
        [HttpPost]
        public void Post([FromBody] MailingList model)
        {
            _context.MailingList.Add(model);
            _context.SaveChanges();
        }
        // PUT: api//MailingListApi/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] MailingList model)
        {
            _context.MailingList.Update(model);
            _context.SaveChanges();
        }
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var mailingListItem = _context.MailingList.Find(id);
            _context.MailingList.Remove(mailingListItem);
            _context.SaveChanges();
        }
    }
}
