using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Domain.Events;
using Marten;
using MediatR;
using NUnit.Framework;
using SharpTestsEx;
using Users.Models.Storage;
using Users.Services.CommandHandlers;
using Users.Shared.Commands;
using Users.Shared.ValueObjects;
using Xunit;

namespace Users.Tests.CommandHandlerTests
{
    
    public class UsersCommandHandlerTests
    {
        private  UsersDbContext _context;
        private  IEventBus _eventBus;

        //public UsersCommandHandlerTests(UsersDbContext context, IEventBus eventBus)
        //{
        //    _context = context;
        //    _eventBus = eventBus;
        //}

        [Fact]
        public async Task ShouldCreateUser()
        {
            var tmp = new UsersDbContextFactory();
            _context = tmp.CreateDbContext(null);
            _eventBus = null;

            var target = new UsersCommandHandler(_context, _eventBus);
            var id = Guid.NewGuid();
            var command = new CreateUser(id,
                new UserInfo
                {
                    Email = "9789672@gmail.com",
                    Login = "Koza",
                    Password = "123"
                });
            await target.Handle(command);

            foreach (var entity in _context.Users)
                _context.Users.Remove(entity);
            _context.SaveChanges();

            var createdCompany = await _context.Users.FirstOrDefaultAsync();
            createdCompany.Should().Not.Be.Null();

        }

        public class CreateUserTests
        {
           
            
        }
    }
}
