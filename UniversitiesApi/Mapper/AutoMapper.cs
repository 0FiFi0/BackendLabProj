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

            CreateMap<university_ranking_year, universityscoreDTO>()
                .ForMember(dest => dest.UniversityId, opt => opt.MapFrom(src => src.university.id))
                .ForMember(dest => dest.UniversityName, opt => opt.MapFrom(src => src.university.university_name))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.year))
                .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.score))
                .ForMember(dest => dest.CriteriaName, opt => opt.MapFrom(src => src.ranking_criteria.criteria_name));

            CreateMap<ScoreDTO, university_ranking_year>();
        }
    }
}
