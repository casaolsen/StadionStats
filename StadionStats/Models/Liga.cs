using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace StadionStats.Models
{
    public class Liga
    {
        public int LigaID { get; set; }

        [StringLength(40, MinimumLength = 3)]
        [Required]
        public string Navn { get; set; }
        public string Logo { get; set; }
        public int LandID { get; set; }

        public Land Land { get; set; }

        public ICollection<Game> Games { get; set; }
        public ICollection<Team2> Team2s { get; set; }
    }
}
