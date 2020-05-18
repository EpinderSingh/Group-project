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
        public DbSet<NewsPost> NewsPosts { get; set; }
        public DbSet<User> Users { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<NewsPost>()
        //    .HasOne<User>()
        //    .WithMany()
        //    .HasForeignKey(p => p.AuthorId);
        //}
    }
}