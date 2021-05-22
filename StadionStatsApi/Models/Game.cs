using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StadionstatsApi.Models
{
    public class Game
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public int? HomeTeamId { get; set; }
        [ForeignKey("HomeTeamId")]
        public Team2 HomeTeam { get; set; }

        public int? AwayTeamId { get; set; }
        [ForeignKey("AwayTeamId")]
        public virtual Team2 AwayTeam { get; set; }

        public float HomeScore { get; set; }
        public float GuestScore { get; set; }
        public int Attendance { get; set; }
        public int Tv { get; set; }
        public int StadionID { get; set; }
        public int LigaID { get; set; }
        public bool IsCorona { get; set; }

        public Liga Liga { get; set; }
        public Stadion Stadion { get; set; }
    }
}
