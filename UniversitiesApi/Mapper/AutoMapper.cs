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
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.UniversityName, opt => opt.MapFrom(src => src.university_name))
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.country.country_name));

            CreateMap<university_year, universitydataDTO>()
                .ForMember(dest => dest.UniversityId, opt => opt.MapFrom(src => src.university_id))
                .ForMember(dest => dest.UniversityName, opt => opt.MapFrom(src => src.university.university_name)) 
                .ForMember(dest => dest.NumberOfStudents, opt => opt.MapFrom(src => src.num_students))
                .ForMember(dest => dest.StaffRatio, opt => opt.MapFrom(src => src.student_staff_ratio))
                .ForMember(dest => dest.NumberOfInternationalStudents, opt => opt.MapFrom(src => src.pct_international_students))
                .ForMember(dest => dest.NumberOfFemaleStudents, opt => opt.MapFrom(src => src.pct_female_students));

            CreateMap<ScoreDTO, university_ranking_year>();
        }
    }
}
