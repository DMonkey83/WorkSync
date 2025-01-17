using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers
{
    public class IssueDeletedConsumer : IConsumer<IssueDeleted>
    {
        public async Task Consume(ConsumeContext<IssueDeleted> context)
        {
            Console.WriteLine("--> Consuming issue deleted: " + context.Message.Id);

            var result = await DB.DeleteAsync<Issue>(context.Message.Id.ToString());

            if (!result.IsAcknowledged)
            {
                throw new MessageException(typeof(IssueDeleted), "Problem deleting from mongodb");
            }
        }
    }
}