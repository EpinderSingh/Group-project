using System;
using System.Collections.Generic;

namespace CoWork454.Models
{
    public class Resource
    {
        
        public int Id { get; set; }
        public string ResourceName { get; set; }
        public string ResourceDescription { get; set; }
        public string ResourceImage { get; set; }
        public int ResourceMaxCapacity { get; set; }
        public bool ResourceHasVC { get; set; }

        public virtual List<ResourceBooking> ResourceBookings { get; set; }
    }
}
