using System;
namespace CoWork454.Models
{
    /* This is for taking in news posts from the admin panel */
    public class NewsPostModel
    {
        public string NewsTitle { get; set; }
        public string NewsText { get; set; }
        public string NewsPhoto { get; set; }
        public NewsTag NewsTag { get; set; }
    }
}
