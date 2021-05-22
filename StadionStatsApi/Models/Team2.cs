using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StadionstatsApi.Models
{
    public class Team2
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StadionID { get; set; }
        public int Seasontickets { get; set; }
        public string Image { get; set; }
        public string Logo { get; set; }
        public string Address { get; set; }
        public string Sponsortext { get; set; }
        public int LigaID { get; set; }

        public Stadion Stadion { get; set; }
        public Liga Liga { get; set; }

        [InverseProperty("HomeTeam")]
        public ICollection<Game> HomeGames { get; set; }

        [InverseProperty("AwayTeam")]
        public ICollection<Game> AwayGames { get; set; }

        public ICollection<Sponsor> Sponsor { get; set; }
    }
}
