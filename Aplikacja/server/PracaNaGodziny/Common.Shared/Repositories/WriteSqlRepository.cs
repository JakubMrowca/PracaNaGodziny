using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Common.Shared.Repositories
{
    public class WriteSqlRepository<T> : IWriteRepository<T>
        where T : IAggregate, new()
    {
        private readonly DbContext _context;
        private IDbContextTransaction _transaction;

        public WriteSqlRepository(DbContext context)
        {
            _context = context;
        }

        public void StartTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted)
        {
            _transaction = _context.Database.BeginTransactionAsync(new CancellationToken());

        }

        public Task<T> GetSingleAsync(Guid id)
        {
            return _context.Set<T>().Where(x => !x.Arch).FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public virtual Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public virtual void CommitTransaction()
        {
            _transaction.Commit();
        }

        public virtual void RollbackTransaction()
        {
            _transaction.Rollback();
        }

        public void Dispose()
        {
            _context.Dispose();
            _transaction?.Dispose();
        }
    }
}
