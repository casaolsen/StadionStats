using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StadionStats.Models
{
    public class Article
    {
        public int ArticleID { get; set; }

        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string TeaserText { get; set; }

        public string Image { get; set; }

        public string Text { get; set; }

        public string ExternalLink { get; set; }

        public string ReadmoreLink { get; set; }

    }
}