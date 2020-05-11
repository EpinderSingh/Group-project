using System;
namespace CoWork454.Models
{
    public class NewsPost
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public DateTimeOffset DateTimePosted { get; set; }
        public string NewsTitle { get; set; }
        public string NewsText { get; set; }
        public string NewsPhoto { get; set; }
        public NewsTag NewsTag { get; set; }
    }
}
