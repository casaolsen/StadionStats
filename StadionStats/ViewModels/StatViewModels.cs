using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StadionStats.ViewModels.StatViewModels
{
    public class HjemmekampeCount
    {
        public int HomeTeamId { get; set; }
        public int HomeTeam { get; set; }
        public int HomeCount { get; set; }
        public int HomeTotal { get; set; }
        public int HomeGames { get; set; }
        public int HomeAvg { get; set; }
        public string HomeTeamName { get; set; }
        public string TeamImage { get; set; }

    }

    public class TVCount
    {
        public int HomeTeam { get; set; }
        public int HomeCount { get; set; }
        public int HomeTotal { get; set; }
        public int GameTotal { get; set; }
        public int TVTotal { get; set; }
        public int HomeGames { get; set; }
        public int HomeAvg { get; set; }
        public string HomeTeamName { get; set; }
        public string TeamImage { get; set; }

    }

    public class AttendanceCountModel
    {
        public int AttendanceCount { get; set; }
        public int TotalAttendanceCount { get; set; }
    }


    public class HometeamGroup
    {
        //[DataType(DataType.Date)]
        //public DateTime? HometeamDate { get; set; }

        public int HometeamCount { get; set; }

        public string LandNavn { get; set; }

    }

    public class SeasonticketsCount
    {
        public int Team { get; set; }
        public string TeamName { get; set; }
        public string TeamImage { get; set; }
        public int StadiumAttendance { get; set; }
        public int SeasonticketsSales { get; set; }
        public double Coverage { get; set; }
    }

    public class LigaGroup
    {
        public int LigaID { get; set; }
        public int LigaCount { get; set; }
        public string LigaName { get; set; }
        public string LigaLogo { get; set; }
        public string LigaCountry { get; set; }
        public int StadionID { get; set; }
        public int HomeTotal { get; set; }
        public int HomeAvg { get; set; }
    }

}