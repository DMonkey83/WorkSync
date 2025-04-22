using AutoMapper;
using Contracts;
using ProjectService.DTOs;
using ProjectService.Entities;

namespace ProjectService.RequestHelpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Project, ProjectDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Components, opt => opt.MapFrom(src => src.Components));
            CreateMap<Board, BoardDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<CreateProjectDto, Project>();
            CreateMap<UpdateProjectDto, Project>();
            CreateMap<Component, ComponentDto>();
            CreateMap<CreateComponentDto, Component>();
            CreateMap<UpdateComponentDto, Component>();
            CreateMap<Sprint, SprintDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId))
                .ForMember(dest => dest.Project, opt => opt.MapFrom(src => src.Project));
            // New mapping for creating a sprint
            CreateMap<CreateSprintDto, Sprint>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Assuming ID is auto-generated
                .ForMember(dest => dest.StartDate, opt => opt.Ignore()) // Handle timestamps in service or database
                .ForMember(dest => dest.EndDate, opt => opt.Ignore());
            CreateMap<IssueComment, IssueCommentDto>();
            CreateMap<IssueCustomField, IssueCustomFieldDto>();
            CreateMap<Entities.IssueLabel, IssueLabelDto>();
            CreateMap<IssuePriority, IssuePriorityDto>();
            CreateMap<IssueSequence, IssueSequenceDto>();
            CreateMap<IssueStatus, IssueStatusDto>();
            CreateMap<IssueType, IssueTypeDto>();
            CreateMap<CreateIssueTypeDto, IssueType>();
            CreateMap<CreateIssueCommentDto, IssueComment>();
            CreateMap<CreateIssueCustomFieldDto, IssueCustomField>();
            CreateMap<BoardDto, Board>();
            CreateMap<CreateBoardDto, Board>();
            CreateMap<UpdateBoardDto, Board>();
            CreateMap<UpdateIssueCommentDto, IssueComment>();
            CreateMap<UpdateIssueCustomFieldDto, IssueCustomField>();
            CreateMap<IssueDto, IssueCreated>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Handle timestamps in service or database
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
            CreateMap<UpdateIssueDto, Issue>();
            CreateMap<IssueDto, IssueUpdated>()
                .ForMember(dest => dest.IssueKey, opt => opt.MapFrom(src => src.IssueKey))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId))
                .ForMember(dest => dest.IssuePriorityName, opt => opt.MapFrom(src => src.IssuePriorityName))
                .ForMember(dest => dest.IssueStatusName, opt => opt.MapFrom(src => src.IssueStatusName))
                .ForMember(dest => dest.IssueTypeName, opt => opt.MapFrom(src => src.IssueTypeName))
                .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Summary))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
                .ForMember(dest => dest.OriginalEstimate, opt => opt.MapFrom(src => src.OriginalEstimate))
                .ForMember(dest => dest.RemainingEstimate, opt => opt.MapFrom(src => src.RemainingEstimate))
                .ForMember(dest => dest.TimeSpent, opt => opt.MapFrom(src => src.TimeSpent))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt));
            CreateMap<Issue, IssueDeleted>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<Issue, IssueDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.IssuePriorityName, opt => opt.MapFrom(src => src.IssuePriority.PriorityName))
                .ForMember(dest => dest.IssueStatusName, opt => opt.MapFrom(src => src.IssueStatus.StatusName))
                .ForMember(dest => dest.IssueTypeName, opt => opt.MapFrom(src => src.IssueType.IssueTypeName))
                .ForMember(dest => dest.IssueLabels, opt => opt.MapFrom(src => src.IssueLabels))
                .ForMember(dest => dest.IssueComments, opt => opt.MapFrom(src => src.IssueComments))
                .ForMember(dest => dest.IssueCustomFields, opt => opt.MapFrom(src => src.IssueCustomFields));
            // New mapping for creating an issue
            CreateMap<CreateIssueDto, Issue>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Assuming ID is auto-generated
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Handle timestamps in service or database
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IssueCustomFields, opt => opt.Ignore());

        }

    } //
}