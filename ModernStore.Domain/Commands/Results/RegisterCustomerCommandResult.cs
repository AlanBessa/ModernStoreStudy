using ModernStore.Shared.Commands;
using System;

namespace ModernStore.Domain.Commands.Results
{
    public class RegisterCustomerCommandResult : ICommandResult
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
