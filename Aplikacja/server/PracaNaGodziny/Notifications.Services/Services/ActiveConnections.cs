using System;
using System.Collections.Generic;
using System.Text;

namespace Notifications.Services.Services
{
    public class ConectionSet
    {
        public Guid UserGuid { get; set; }
        public string ConnectionId { get; set; }
    }
    public interface IActiveConnections
    {
        List<ConectionSet> Conections { get; set; }
    }
    public class ActiveConnections : IActiveConnections
    {
        public List<ConectionSet> Conections { get; set; }
    }
    
}
