using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace StadionStats.Models
{
    public class Stadion
    {
        public int StadionID { get; set; }
        public string Navn { get; set; }
        public int AttendanceCapacity { get; set; }

        public ICollection<Team2> Team2s { get; set; }
    }
}