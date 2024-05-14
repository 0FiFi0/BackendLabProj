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
    }
}
