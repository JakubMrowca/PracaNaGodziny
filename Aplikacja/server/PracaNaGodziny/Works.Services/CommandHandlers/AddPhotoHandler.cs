using Infrastructure.Domain.Commands;
using Infrastructure.Domain.Events;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Works.Models.Domain;
using Works.Models.Storage;
using Works.Services.Services;
using Works.Shared.Commands;

namespace Works.Services.CommandHandlers
{
    public class AddPhotoHandler : ICommandHandler<AddPhotoForEmployer>
    {

        private readonly IPhotoServices _photoService;
        private DbSet<Employer> _emplyers;
        private readonly IEventBus _eventBus;
        private readonly WorkDbContext _workDbContext;

        public AddPhotoHandler(IPhotoServices photoServices, WorkDbContext workDbContext, IEventBus eventBus)
        {

            _eventBus = eventBus;
            _workDbContext = workDbContext;
            _emplyers = _workDbContext.Employers;
            _photoService = photoServices;
        }

        public async Task Handle(AddPhotoForEmployer command, CancellationToken cancellationToken)
        {
            var employer = await _emplyers
                .Where(x => x.Arch == false)
                .Where(x => x.Id == Guid.Parse(command.EmployerId))
                .FirstOrDefaultAsync(cancellationToken);

            var photo = _photoService.ConvertToByteArray(command.Photo);
            employer.Photo = photo;
            _workDbContext.SaveChanges();
        }
    }
}
