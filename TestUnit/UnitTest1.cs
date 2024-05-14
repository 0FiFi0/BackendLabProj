using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UniversitiesApi;
using WebApi.Dto;
using Xunit;

namespace TestUnit
{
    public class UniversityControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public UniversityControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task AddUniversityScore_ValidRequest_ReturnsOk()
        {
            var client = _factory.CreateClient();
            var id = 3;
            var model = new ScoreDTO
            {
                Score = 90,
                Year = 2024,
                RankingCriteriaId = 1
            };
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

           
            var response = await client.PostAsync($"/api/university/{id}/scores", content);

            
            response.EnsureSuccessStatusCode(); 
        }

        [Fact]
        public async Task AddUniversityScore_InvalidRequest_ReturnsBadRequest()
        {
            
            var client = _factory.CreateClient();
            var id = 1; 
            var model = new ScoreDTO
            {
                Score = -7,
                Year = 2002,
                RankingCriteriaId = 1
            };
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            
            var response = await client.PostAsync($"/api/university/{id}/scores", content);

            
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}