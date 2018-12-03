using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Works.Shared.ValueObjects;

namespace Users.Services.Services
{
    public interface ILoggedUsersMock
    {
        UserVm GetLoggedUser();
        void SetLoggedUser(UserVm user);
    }

    public class LoggedUsersMock : ILoggedUsersMock
    {
        private UserVm _loggedUser;

        public UserVm GetLoggedUser()
        {
            return _loggedUser;
        }

        public void SetLoggedUser(UserVm user)
        {
            _loggedUser = user;
        }
    }
}
