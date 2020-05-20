using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoWork454.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PasswordHash { get; set; }
        public string UserImagePath { get; set; }

        public bool IsAdmin { get; set; }
        public virtual List<ResourceBooking> ResourceBookings { get; set; }
    }
}
