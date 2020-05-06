using System;
namespace CoWork454.Models
{
    public class ContactForm
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string SubjectLine { get; set; }
        public string MessageBody { get; set; }
    }
}
