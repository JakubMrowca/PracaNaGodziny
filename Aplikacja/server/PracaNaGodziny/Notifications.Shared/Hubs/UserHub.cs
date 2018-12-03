using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Notifications.Shared.Hubs
{
    public class UserHub: Hub
    {
        public async Task InitHubUser(string user, string message)
        {
            var connectionId = Context.ConnectionId;
            await Clients.Client(connectionId).SendAsync("HubUserInited", user, connectionId);
        }

        public async Task SendResponse(string toUser, string response)
        {

        }
    }

}
