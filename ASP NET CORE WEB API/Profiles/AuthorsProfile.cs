using ASP_NET_CORE_WEB_API.Helpers;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_NET_CORE_WEB_API.Profiles
{
    public class AuthorsProfile : Profile
    {
        public AuthorsProfile()
        {
            // Map property name on source to destination type with the same property
            CreateMap<Entities.Author, Models.AuthorDto>()
                // Make sure the member properties are mapped correctly on destination
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(
                    dest => dest.Age,
                    opt => opt.MapFrom(src => src.DateOfBirth.GetCurrentAge()));

            CreateMap<Models.AuthorForCreationDto, Entities.Author>();
        }
    }
}
