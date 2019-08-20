using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Infrastructure.Domain.Aggregates;

namespace Common.Shared.Repositories
{
    public interface IRepository
    {

    }

    public interface IReadRepository<T> : IDisposable, IRepository where T : IAggregate, new()
    {
        Task<List<T>> GetAllAsync();
        int Count();
        Task<T> GetSingleAsync(Guid id);
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
    }

    public interface IWriteRepository<T> : IDisposable, IRepository where T : IAggregate, new()
    {
        Task<T> GetSingleAsync(Guid id);
        void Add(T entity);
        Task SaveChangesAsync();

        void StartTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted);
        void CommitTransaction();
        void RollbackTransaction();
    }
}
