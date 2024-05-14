using AppCore.Models;
using Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;
using AutoMapper;
using WebApi.Dto;
using Infrastructure.Services;
using System.Data.Entity;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityController : ControllerBase
    {
        private readonly IUniversityService _universityService;
        private readonly IMapper _mapper;

        public UniversityController(IUniversityService universityService, IMapper mapper)
        {
           _universityService = universityService;
           _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<universityDTO>> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
           try
           {
               var universities = _universityService.GetPaginatedUniversities(page, pageSize);
               var universityDTOs = _mapper.Map<IEnumerable<universityDTO>>(universities);
               return Ok(universityDTOs);
           }
            catch (Exception ex)
            {
               return StatusCode(500, $"Internal server error: {ex.Message}");
           }
        }


        [HttpGet("universities")]
        public ActionResult<IEnumerable<universityscoreDTO>> Get([FromQuery] string country)
        {
            try
            {
                if (string.IsNullOrEmpty(country))
                {
                    return BadRequest("Country parameter is required.");
                }

                var universities = _universityService.GetUniversityRankingYearsByCountry(country);

                var universityDtos = _mapper.Map<IEnumerable<universityscoreDTO>>(universities);

                return Ok(universityDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("{id}/scores")]
        public async Task<IActionResult> AddUniversityScore(int id, [FromBody] ScoreDTO model)
        {
            try
            {
                // Walidacja punktacji, roku i id kryterium
                if (model.Score < 0 || model.Score > 100)
                {
                    return BadRequest("Score must be between 0 and 100.");
                }

                if (model.Year <= 0)
                {
                    return BadRequest("Year must be a positive integer.");
                }

                if (model.RankingCriteriaId <= 0)
                {
                    return BadRequest("RankingCriteriaId must be a positive integer.");
                }

                // Sprawdzenie czy istnieje już punktacja dla podanego uniwersytetu, roku i kryterium
                var existingScore = await _universityService.GetUniversityScore(id, model.Year, model.RankingCriteriaId);
                if (existingScore != null)
                {
                    return BadRequest("University score for the specified year and ranking criteria already exists.");
                }

                await _universityService.AddUniversityScore(id, model.Score, model.Year, model.RankingCriteriaId);
                return Ok("University score added successfully");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }

}
