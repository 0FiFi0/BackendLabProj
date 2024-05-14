using AppCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IUniversityService
    {
        List<university> GetPaginatedUniversities(int page, int pageSize);

        IEnumerable<university_ranking_year> GetUniversityRankingYearsByCountry(string country);

        Task AddUniversityScore(int universityId, int score, int year, int rankingCriteriaId);

        Task<university_ranking_year> GetUniversityScore(int universityId, int year, int rankingCriteriaId);
    }
}

