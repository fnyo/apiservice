using AutoMapper;
using Fnyo.Learn.Jenkins.Dto;
using Fnyo.Learn.Jenkins.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Fnyo.Learn.Jenkins
{
    public class MapProfile:Profile
    {
        //2021-11-02
        public MapProfile()
        {
            CreateMap<Book, BookDto>().ForMember(dest => dest.Types,
                options => options.MapFrom(src => JsonConvert.DeserializeObject<List<string>>(src.Types)))
                .ForMember(dest => dest.Date, options => options.MapFrom(src => Convert.ToDateTime(src.Date).ToString("yyyy-MM-dd")));

            CreateMap<BookDto, Book>().ForMember(dest => dest.Types,
                options => options.MapFrom(src => JsonConvert.SerializeObject(src.Types)));

            //CreateMap<StudentScore, StudentScoreDto>();
            CreateMap<StudentScore, StudentScoreDto>().ForMember(dest => dest.ImportTime,
                options => options.MapFrom(src => src.ImportTime.ToString("yyyy-MM-dd")));


            CreateMap<UserDto, User>();
            //    .ForMember(dest => dest.ExamineType, options => options.MapFrom(src => src.ExamineType.ToString()))
            //    .ForMember(dest => dest.Level, options => options.MapFrom(src => src.Level.ToString()))
            //    .ForMember(dest=>dest.IsBest,options=>options.MapFrom(src=>src.IsBest?"是":"否"));
        }
    }
}
