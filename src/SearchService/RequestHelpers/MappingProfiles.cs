using AutoMapper;
using Contracts;
using SearchService.Models;

namespace SearchService.RequestHelpers
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles()
        {
            CreateMap<IssueCreated, Issue>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Id.ToString())); // Ensure mapping from Guid to string
            CreateMap<IssueUpdated, Issue>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Id.ToString())); // Ensure mapping from Guid to string
    
        }

    }
}