using MediatR;

namespace Infrastructure.Domain.Commands
{
    public interface ICommandHandler<in T>: IRequestHandler<T>
        where T : ICommand
    {
    }
}
