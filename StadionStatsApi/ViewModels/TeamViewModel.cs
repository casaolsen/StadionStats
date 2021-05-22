using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StadionstatsApi.Models;

namespace StadionstatsApi.ViewModels
{
    public class TeamViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Seasontickets { get; set; }
        public int AttendanceCapacity { get; set; }
        public string Stadionnavn { get; set; }
        //public string Image { get; set; }
        //public string Logo { get; set; }
        //public string Address { get; set; }
        //public string Sponsortext { get; set; }
        //public int LigaID { get; set; }
    }
    //public class StadionViewModel
    //{
    //    public int StadionID { get; set; }
    //    public string Navn { get; set; }
    //    public int AttendanceCapacity { get; set; }
    //}
}
