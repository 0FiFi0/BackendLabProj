using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Context;
using AppCore.Models;
using Microsoft.EntityFrameworkCore;
using Azure.Core;

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
    }
}