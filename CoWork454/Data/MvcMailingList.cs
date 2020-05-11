using Microsoft.EntityFrameworkCore;
using CoWork454.Models;

namespace MvcMailingList.Data
{
    public class MvcMailingListContext : DbContext
    {
        public MvcMailingListContext(DbContextOptions<MvcMailingListContext> options)
            : base(options)
        {
        }

        public DbSet<MailingList> MailingList { get; set; }
    }
}