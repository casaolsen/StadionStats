using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StadionStats.Models;

namespace StadionStats.Data
{
    public static class DbInitializer
    {
        public static void Initialize(StatContext context)
        {
            context.Database.EnsureCreated();

            // Kig efter lande
            if (context.Land.Any())
            {
                return;   // Databasen er opdateret
            }

            var lande = new Land[]
            {
            new Land{Landenavn="Danmark"},
            };
            foreach (Land c in lande)
            {
                context.Land.Add(c);
            }
            context.SaveChanges();


            var ligaer = new Liga[]
            {
            new Liga{Navn="3F Superligaen",Logo="1.png",LandID=1},
            };
            foreach (Liga d in ligaer)
            {
                context.Liga.Add(d);
            }
            context.SaveChanges();

            var stadions = new Stadion[]
{
            new Stadion{Navn="Brøndby Stadion",AttendanceCapacity=28000},
            new Stadion{Navn="Lyngby Stadion",AttendanceCapacity=10000},
            new Stadion{Navn="CASA Arena",AttendanceCapacity=10000},
            new Stadion{Navn="Parken",AttendanceCapacity=38500},
};
            foreach (Stadion e in stadions)
            {
                context.Stadion.Add(e);
            }
            context.SaveChanges();

            var teams = new Team2[]
{
            new Team2{Name="Brondby IF",StadionID=1,Logo="bif.png",Image="bif.jpg",Sponsortext="Her er sponsortekst. Lorum ipsum",LigaID=1,Seasontickets=11900},
            new Team2{Name="Lyngby Boldklub",StadionID=2,Logo="lfc.png",Image="lyn.jpg",Sponsortext="Her er sponsortekst. Lorum ipsum",LigaID=1,Seasontickets=1200},
            new Team2{Name="AC Horsens",StadionID=3,Logo="ach.png",Image="ach.jpg",Sponsortext="Her er sponsortekst. Lorum ipsum",LigaID=1,Seasontickets=800},
            new Team2{Name="FC Copenhagen",StadionID=4,Logo="fck.png",Image="fck.jpg",Sponsortext="Her er sponsortekst. Lorum ipsum",LigaID=1,Seasontickets=12500},
};
            foreach (Team2 s in teams)
            {
                context.Team2s.Add(s);
            }
            context.SaveChanges();

            var games = new Game[]
            {
            new Game{Date=DateTime.Parse("2021-04-01"),HomeTeamId=1,AwayTeamId=3,HomeScore=1,GuestScore=0,Attendance=27500,StadionID=1,LigaID=1},
            new Game{Date=DateTime.Parse("2021-04-05"),HomeTeamId=2,AwayTeamId=4,HomeScore=1,GuestScore=3,Attendance=6500,StadionID=2,LigaID=1},
            new Game{Date=DateTime.Parse("2021-04-11"),HomeTeamId=3,AwayTeamId=2,HomeScore=1,GuestScore=3,Attendance=7600,StadionID=3,LigaID=1},
            new Game{Date=DateTime.Parse("2021-04-13"),HomeTeamId=4,AwayTeamId=1,HomeScore=0,GuestScore=5,Attendance=31300,StadionID=4,LigaID=1},
            };
            foreach (Game f in games)
            {
                context.Games.Add(f);
            }
            context.SaveChanges();

            var sponsors = new Sponsor[]
{
            new Sponsor{Title="Business Partner",SponsorText="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce quis lectus quis sem lacinia nonummy.",Team2ID=1},
            new Sponsor{Title="Euro Partner",SponsorText="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce quis lectus quis sem lacinia nonummy.",Team2ID=1},
};
            foreach (Sponsor f in sponsors)
            {
                context.Sponsor.Add(f);
            }
            context.SaveChanges();


            var articles = new Article[]
{
            new Article{Date=DateTime.Parse("2021-04-01"),Title="Artikel",TeaserText="Lorem ipsum dolor sit amet, consectetur adipiscing elit.", ExternalLink="https://www.tipsbladet.dk/nyhed/superliga/broendby-lander-ny-hovedsponsor-aftalen-gaelder-i-et-halvt-aar"},
};
            foreach (Article f in articles)
            {
                context.Article.Add(f);
            }
            context.SaveChanges();


        }
    }
}