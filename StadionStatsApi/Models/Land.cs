using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace StadionstatsApi.Models
{
    public class Land
    {
        public int LandID { get; set; }

        [StringLength(40, MinimumLength = 3)]
        [Required]
        public string Landenavn { get; set; }

        public ICollection<Liga> Liga { get; set; }
    }
}