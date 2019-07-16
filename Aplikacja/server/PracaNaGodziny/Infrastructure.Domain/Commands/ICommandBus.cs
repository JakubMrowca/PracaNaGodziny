using System.Threading.Tasks;

namespace Infrastructure.Domain.Commands
{
    public interface ICommandBus
    {
        Task Send<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
