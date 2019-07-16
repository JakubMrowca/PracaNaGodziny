using MediatR;

namespace Infrastructure.Domain.Queries
{
    public interface IQuery<out TResponse>: IRequest<TResponse>
    {
    }
}
