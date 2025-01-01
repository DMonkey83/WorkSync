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
            CreateMap<Board, BoardDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<CreateProjectDto, Project>();
            CreateMap<UpdateProjectDto, Project>();
            CreateMap<Component, ComponentDto>();
            CreateMap<CreateComponentDto, Component>();
            CreateMap<UpdateComponentDto, Component>();
            CreateMap<Sprint, SprintDto>();
            CreateMap<IssueComment, IssueCommentDto>();
            CreateMap<IssueCustomField, IssueCustomFieldDto>();
            CreateMap<IssueLabel, IssueLabelDto>();
            CreateMap<IssuePriority, IssuePriorityDto>();
            CreateMap<IssueStatus, IssueStatusDto>();
            CreateMap<IssueType, IssueTypeDto>();
            CreateMap<CreateIssueCommentDto, IssueComment>();
            CreateMap<CreateIssueCustomFieldDto, IssueCustomField>();
            CreateMap<BoardDto, Board>();
            CreateMap<CreateBoardDto, Board>();
            CreateMap<UpdateBoardDto, Board>();
            CreateMap<UpdateIssueCommentDto, IssueComment>();
            CreateMap<UpdateIssueCustomFieldDto, IssueCustomField>();
            CreateMap<Issue, IssueDto>()
                .ForMember(dest => dest.IssuePriorityName, opt => opt.MapFrom(src => src.IssuePriority.PriorityName)) // Example for nested mapping
                .ForMember(dest => dest.IssueStatusName, opt => opt.MapFrom(src => src.IssueStatus.StatusName))    // Adjust as necessary
                .ForMember(dest => dest.IssueTypeName, opt => opt.MapFrom(src => src.IssueType.IssueTypeName))
                .ForMember(dest => dest.IssueLabels, opt => opt.MapFrom(src => src.IssueLabels))
                .ForMember(dest => dest.IssueComments, opt => opt.MapFrom(src => src.IssueComments))
                .ForMember(dest => dest.IssueCustomFields, opt => opt.MapFrom(src => src.IssueCustomFields))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.IssueKey, opt => opt.MapFrom(src => src.IssueKey))
                .ReverseMap(); //
        }
        
    }
}