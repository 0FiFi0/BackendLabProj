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

        [HttpGet("{id}")]
        public async Task<ActionResult<universitydataDTO>> GetUniversityData(int id, [FromQuery] int year)
        {
            try
            {
                var universityData = await _universityService.GetUniversityData(id, year);

                if (universityData == null)
                {
                    return NotFound($"University data for ID {id} in year {year} not found.");
                }

                var universityDto = _mapper.Map<universitydataDTO>(universityData);

                return Ok(universityDto);
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
                if (model.Score < 0 || model.Score > 100)
                {
                    return BadRequest("Score should be between 0 and 100.");
                }

                if (model.Year <= 0)
                {
                    return BadRequest("Year must be a positive integer.");
                }

                if (model.RankingCriteriaId <= 0)
                {
                    return BadRequest("RankingCriteriaId must be a positive integer.");
                }

                var existingScore = await _universityService.GetUniversityScore(id, model.Year, model.RankingCriteriaId);
                if (existingScore != null)
                {
                    return Conflict("University score for the specified year and ranking criteria already exists.");
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
