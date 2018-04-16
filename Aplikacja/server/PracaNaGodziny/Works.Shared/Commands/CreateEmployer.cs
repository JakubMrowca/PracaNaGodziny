using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Domain.Commands;
using Works.Shared.ValueObjects;

namespace Works.Shared.Commands
{
    public class CreateEmployer:ICommand
    {
        public Guid Id { get;  }
        public Guid UserId { get; }
        public EmplyerInfo Data { get; }

        public CreateEmployer(Guid id, Guid userId, EmplyerInfo data)
        {
            Id = id;
            UserId = userId;
            Data = data;
        }
    }
}
