using Infrastructure.Domain.Commands;
using Infrastructure.Domain.Events;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Works.Models.Domain;
using Works.Models.Storage;
using Works.Services.Services;
using Works.Shared.Commands;

namespace Works.Services.CommandHandlers
{
    public class AddPhotoHandler : ICommandHandler<AddPhotoForEmployer>
    {

        private readonly IConvertPhoto _photoConverter;
        private DbSet<Employer> _emplyers;
        private readonly IEventBus _eventBus;
        private readonly WorkDbContext _workDbContext;

        public AddPhotoHandler(IConvertPhoto photoServices, WorkDbContext workDbContext, IEventBus eventBus)
        {

            _eventBus = eventBus;
            _workDbContext = workDbContext;
            _emplyers = _workDbContext.Employers;
            _photoConverter = photoServices;
        }

        public async Task<Unit> Handle(AddPhotoForEmployer command, CancellationToken cancellationToken)
        {
            var employer = await _emplyers
                .Where(x => x.Arch == false)
                .Where(x => x.Id == Guid.Parse(command.EmployerId))
                .FirstOrDefaultAsync(cancellationToken);

            var photo = _photoConverter.ConvertToByteArray(command.Photo);
            employer.Photo = photo;
            _workDbContext.SaveChanges();
            return Unit.Value;
        }
    }
}
