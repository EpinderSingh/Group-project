using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using MvcMailingList.Data;

namespace CoWork454.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvcMailingListContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MvcMailingListContext>>()))
            {
                // Look for any super user logins.
                if (context.Users.Any())
                {
                    return;   // DB has been seeded
                }

                context.Users.AddRange(
                    new User
                    {
                        FirstName = "CoWork454",
                        LastName = "Admin",
                        EmailAddress = "admin@cowork454.com",
                        PasswordHash = "$2a$11$JrFlOUbUXB5MdhriMuCqye3XNolKNbxX2JeJBai71Vs47WsgT/nq6",
                        IsAdmin = true

                    }
                );
                context.SaveChanges();
            }
        }
    }
}
