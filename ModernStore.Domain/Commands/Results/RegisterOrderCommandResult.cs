using ModernStore.Shared.Commands;

namespace ModernStore.Domain.Commands.Results
{
    public class RegisterOrderCommandResult : ICommandResult
    {
        public string Number { get; set; }
    }
}
