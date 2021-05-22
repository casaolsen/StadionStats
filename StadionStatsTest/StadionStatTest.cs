using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;
using StadionstatsApi.ViewModels;
using StadionStats.Models;
using StadionStats.Controllers;
using Stadionstats.Api;

namespace StadionStatsTest
{

    public class StadionStatTest
    {
        private TestServer server;

        public HttpClient Client { get; private set; }

        public StadionStatTest()
        {
            server = new TestServer(new WebHostBuilder().UseStartup<StadionStats.Startup>().UseSetting("ConnectionStrings:DefaultConnection", "Server=(localdb)\\mssqllocaldb;Database=StadionStatsDb;Trusted_Connection=True;MultipleActiveResultSets=true"));
            Client = server.CreateClient();
        }

        [Fact]
        public void Test_GameAPI_Ok()
        {
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/ApiGetGame");

            var response = Client.SendAsync(request).Result;

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }

        [Fact]
        public async Task Test_TeamAPI_Teamname_Ok()
        {
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/ApiGetTeam");

            var response = Client.SendAsync(request).Result;

            Assert.Contains("Brondby IF", await response.Content.ReadAsStringAsync());

        }

        [Theory]
        [InlineData("thomas@localhost.dk", true)]
        [InlineData("thomas.olsen@localhost.dk", true)]
        [InlineData("thomas.localhost.dk", false)]
        [InlineData("thomas@olsen@localhost.dk", false)]
        [InlineData("thomas", false)]
        [InlineData("", false)]

        public void Test_CheckEmail_Ok(string Email, bool expectedTestResult)
        {
            var RegisterViewModel = new RegisterViewModel();

            Assert.Equal(expectedTestResult, RegisterViewModel.IsValidAddress(Email));
        }

        [Fact]
        public void Test_OpretLiga_Ok()
        {
            Liga var = new Liga();

            var.Navn = "3F Superligaen";

            Assert.Equal("3F Superligaen", var.Navn);
        }

    }
}