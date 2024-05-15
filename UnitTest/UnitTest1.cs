using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using WebApi.Dto;


namespace UnitTest1
{
    public class UniversityControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public UniversityControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task AddUniversityScore_ValidRequest_ReturnsSuccessStatusCode()
        {
            var client = _factory.CreateClient();
            var id = 1;
            var scoreDto = new ScoreDTO
            {
                Score = 90,
                Year = 2023,
                RankingCriteriaId = 1
            };
            var content = new StringContent(JsonConvert.SerializeObject(scoreDto), Encoding.UTF8, "application/json");

            
            var response = await client.PostAsync("/api/University/1/scores", content);

            
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task AddUniversityScore_InvalidScore_ReturnsBadRequest()
        {
            
            var client = _factory.CreateClient();
            var id = 1;
            var scoreDto = new ScoreDTO
            {
                Score = 110,
                Year = 2023,
                RankingCriteriaId = 1
            };
            var content = new StringContent(JsonConvert.SerializeObject(scoreDto), Encoding.UTF8, "application/json");

            
            var response = await client.PostAsync("/api/University/1/scores", content);

            
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}