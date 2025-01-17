using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using MassTransit;

namespace ProjectService.Consumers
{
    public class IssueCreatedFaultConsumer : IConsumer<Fault<IssueCreated>>
    {
        public async Task Consume(ConsumeContext<Fault<IssueCreated>> context)
        {
            Console.WriteLine("--> IssueCreatedFaultConsumer: " + context.Message.Message);

            var exception = context.Message.Exceptions.First();

            if (exception.ExceptionType == "System.ArgumentException")
            {
                await context.Publish(context.Message.Message);
            } else
            {
                Console.WriteLine("Not an argument exception - update error dashboard");
            }
        }
    }
}