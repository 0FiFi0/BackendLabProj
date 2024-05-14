using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AppCore.Models;
using WebApi.Dto;


namespace WebApi.Mapper
{
    public class UniversityProfile : Profile
    {
        public UniversityProfile()
        {
            CreateMap<university, universityDTO>()
                .ForMember(dest => dest.UniversityName, opt => opt.MapFrom(src => src.university_name))
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.country.country_name));

        }
    }
}
