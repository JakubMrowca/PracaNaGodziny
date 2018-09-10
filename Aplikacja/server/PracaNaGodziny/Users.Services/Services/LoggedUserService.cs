using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Works.Shared.ValueObjects;

namespace Users.Services.Services
{
    public interface ILoggedUserService
    {
        UserVm GetLoggedUser();
        void SetLoggedUser(UserVm user);
    }

    public class LoggedUserService : ILoggedUserService
    {
        private UserVm LoggedUser;

        public UserVm GetLoggedUser()
        {
            return LoggedUser;
        }

        public void SetLoggedUser(UserVm user)
        {
            LoggedUser = user;
        }
    }
}
