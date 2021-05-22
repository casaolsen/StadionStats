using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StadionstatsApi.Models
{
    public class Sponsor
    {
        public int SponsorID { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string SponsorText { get; set; }
        public int Team2ID { get; set; }

        public Team2 Team2 { get; set; }

    }
}