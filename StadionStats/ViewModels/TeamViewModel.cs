using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StadionStats.Models;

namespace StadionstatsApi.ViewModels
{
    public class TeamViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Seasontickets { get; set; }
        public int AttendanceCapacity { get; set; }
        public string Stadionnavn { get; set; }
    }
}
