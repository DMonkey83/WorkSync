using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers
{
    public class IssueCreatedConsumer : IConsumer<IssueCreated>
    {
        private readonly IMapper _mapper;

        public IssueCreatedConsumer(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task Consume(ConsumeContext<IssueCreated> context)
        {
            Console.WriteLine("--> Consuming issue created: " + context.Message.IssueKey);
            var issue = _mapper.Map<Issue>(context.Message);
            Console.WriteLine($"Mapped Issue Id: {issue.ID}");

            await issue.SaveAsync();
        }
    }
}