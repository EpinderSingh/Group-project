using System;
namespace CoWork454.Models
{
    public class ResourceBooking
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ResourceId { get; set; }
        public DateTimeOffset ResourceBookingTimeCreated { get; set; }
        public DateTimeOffset ResourceBookingStart { get; set; }
        public DateTimeOffset ResourceBookingEnd { get; set; }

        public virtual User User { get; set; }
        public virtual Resource Resource { get; set; }
    }
}   
