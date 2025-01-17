using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers
{
    public class IssueUpdatedConsumer : IConsumer<IssueUpdated>
    {
        private readonly IMapper _mapper;

        public IssueUpdatedConsumer(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<IssueUpdated> context)
        {
            Console.WriteLine("--> Consuming issue updated: " + context.Message.IssueKey);

            var issue = _mapper.Map<Issue>(context.Message);
            var result = await DB.Update<Issue>()
                .Match(i => i.ID == context.Message.Id.ToString())
                .ModifyOnly(x => new
                {
                    x.ID,
                    x.DueDate,
                    x.Description,
                    x.IssueKey,
                    x.IssueLabels,
                    x.UpdatedAt,
                    x.TimeSpent,
                    x.Summary,
                    x.RemainingEstimate,
                    x.OriginalEstimate,
                    x.IssueStatusName,
                    x.IssuePriorityName,
                    x.IssueTypeName,
                }, issue)
                .ExecuteAsync();

            if (!result.IsAcknowledged)
            {
                throw new MessageException(typeof(IssueUpdated), "Problem updating mongodb");
            }

        }
    }
}