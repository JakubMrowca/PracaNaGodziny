using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Domain.Queries;
using Microsoft.EntityFrameworkCore;
using Users.Models.Domain;
using Users.Models.Storage;
using Users.Shared.Queries;
using Users.Shared.ValueObjects;

namespace Users.Services.QueryHandlers
{
    public class UsersQueryHandler :
        IQueryHandler<GetUser, UserVm>,
        IQueryHandler<GetUsers, List<UserVm>>
    {
        private readonly IQueryable<User> Users;

        public UsersQueryHandler(UsersDbContext dbContext)
        {
            Users = dbContext.Users;
        }

        public async Task<List<UserVm>> Handle(GetUsers query, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Users
                 .Where(user => user.Arch == false)
                 .Select(user => new UserVm
                 {
                     Id = user.Id,
                     Email = user.Email,
                     Login = user.Login
                 })
                 .ToListAsync(cancellationToken);
        }

        public async Task<UserVm> Handle(GetUser query, CancellationToken cancellationToken)
        {
            return await Users
                .Where(user => user.Arch == false && user.Id == query.Id)
                .Select(user => new UserVm
                {
                    Id = user.Id,
                    Email = user.Email,
                    Login = user.Login
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
