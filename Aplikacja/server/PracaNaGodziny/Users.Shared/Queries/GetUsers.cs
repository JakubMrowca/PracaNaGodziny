using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Domain.Queries;
using Users.Shared.ValueObjects;

namespace Users.Shared.Queries
{
    public class GetUsers: IQuery<List<UserVm>>
    {
    }
}
