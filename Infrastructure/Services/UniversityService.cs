﻿using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Context;
using AppCore.Models;
using Microsoft.EntityFrameworkCore;
using Azure.Core;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UniversityService : IUniversityService
    {
        private readonly UniversityDbContext _context;

        public UniversityService(UniversityDbContext context)
        {
            _context = context;
        }
        
        public List<university> GetPaginatedUniversities(int page, int pageSize)
        {
            try
            {
                return _context.university
                    .Include(u => u.country)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<university_ranking_year> GetUniversityRankingYearsByCountry(string country)
        {
            try
            {
                return _context.university_ranking_year
                    .Include(ury => ury.university)
                    .Include(ury => ury.ranking_criteria)
                    .Include(ury => ury.university.country)
                    .Where(ury => ury.university.country.country_name.ToLower() == country.ToLower())
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching university ranking years: {ex.Message}");
            }
        }
        public async Task AddUniversityScore(int universityId, int score, int year, int rankingCriteriaId)
        {
            try
            {

                var newScore = new university_ranking_year
                {
                    university_id = universityId,
                    ranking_criteria_id = rankingCriteriaId,
                    year = year,
                    score = score
                };

                _context.university_ranking_year.Add(newScore);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while adding university score: {ex.Message}");
            }
        }

        public async Task<university_ranking_year> GetUniversityScore(int universityId, int year, int rankingCriteriaId)
        {
            try
            {
                return await _context.university_ranking_year
                    .FirstOrDefaultAsync(ury =>
                        ury.university_id == universityId &&
                        ury.year == year &&
                        ury.ranking_criteria_id == rankingCriteriaId);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching university score: {ex.Message}");
            }
        }
    }
}