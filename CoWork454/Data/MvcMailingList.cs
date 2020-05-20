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
        public DbSet<Resource> Resources { get; set; }
        public DbSet <ResourceBooking> ResourceBookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Resource>()
                .HasMany(p => p.ResourceBookings)
                .WithOne(ci => ci.Resource)
                .HasForeignKey(ci => ci.ResourceId);

            modelBuilder.Entity<User>()
                .HasMany(p => p.ResourceBookings)
                .WithOne(ci => ci.User)
                .HasForeignKey(ci => ci.UserId);
        }
    }
}