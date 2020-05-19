using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoWork454.Models
{
    /* This is for taking in passing / updating / deleting news posts to and from the DB */
    public class NewsPost
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public DateTimeOffset DateTimePosted { get; set; }
        public string NewsTitle { get; set; }
        public string NewsText { get; set; }
        public string NewsPhoto { get; set; }
        public NewsTag NewsTag { get; set; }

        public virtual User Author { get; set; }

        [NotMapped]
        public string NewsTagLabel { get
            { return NewsTag.ToString(); }
        }
    }
}
