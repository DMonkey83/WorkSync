using AutoMapper;
using ProjectService.DTOs;
using ProjectService.Entities;

namespace ProjectService.RequestHelpers
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Project, ProjectDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<CreateProjectDto, Project>();
            CreateMap<UpdateProjectDto, Project>();
            CreateMap<Component, ComponentDto>();
        }
        
    }
}