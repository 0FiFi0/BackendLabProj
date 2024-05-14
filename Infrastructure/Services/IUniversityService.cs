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

    }
}

