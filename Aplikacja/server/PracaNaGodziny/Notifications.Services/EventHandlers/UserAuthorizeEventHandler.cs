using Infrastructure.Domain.Events;
using Notifications.Shared.Events;
using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Domain.Queries;
using Microsoft.AspNetCore.SignalR;
using Notifications.Services.Services;
using Notifications.Shared.Enums;
using Notifications.Shared.Hubs;
using Users.Shared.Events;
using Works.Shared.Queries;

namespace Notifications.Services
{
    public class UserAuthorizeEventHandler : IEventHandler<UserAuthorized>
    {
        private readonly IQueryBus _queryBus;
        private readonly IHubContext<UserHub> _hub;
        private readonly IActiveConnections _active;

        public UserAuthorizeEventHandler(IQueryBus queryBus, IHubContext<UserHub> hub, IActiveConnections active)
        {
            _queryBus = queryBus;
            _hub = hub;
            _active = active;
        }

        public async Task Handle(UserAuthorized notification, CancellationToken cancellationToken)
        {
            var user = await _queryBus.Send<GetForUser, Works.Shared.ValueObjects.UserVm>(new GetForUser { UserId = notification.UserId });
            var obj = new object[2];
            obj[0] = FrontendEventEnum.UserAuthorized.ToString();
            obj[1] = user;

            await _hub.Clients.Client(notification.ConnectionId).SendCoreAsync("EventEmited", obj);
            var userConnection = _active.Conections.FirstOrDefault(x => x.UserGuid == user.Id);
            if (userConnection != null)
                _active.Conections.Remove(userConnection);

            _active.Conections.Add(new ConectionSet {ConnectionId = notification.ConnectionId,UserGuid = notification.UserId});
        }
    }
}
