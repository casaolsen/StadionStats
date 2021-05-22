using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StadionstatsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace StadionstatsApi.Data
{
    public class StatContext : DbContext
    {
        public StatContext(DbContextOptions<StatContext> options) : base(options)
        {
        }

        public DbSet<Liga> Liga { get; set; }
        public DbSet<Stadion> Stadion { get; set; }
        public DbSet<Land> Land { get; set; }
        public DbSet<Team2> Team2s { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Sponsor> Sponsor { get; set; }
    }
}