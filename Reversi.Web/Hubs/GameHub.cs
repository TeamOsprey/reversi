using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reversi.Web.Hubs
{
    public class GameHub : Hub
    {
        public async Task SendUpdate()
        {
            await Clients.All.SendAsync("ReceiveUpdate");
            // await Clients.Client(player).SendAsync("ReceiveUpdate"); work was started to recognize the player
        }
        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "Game1");
            // TODO: consider call to AddPlayer to be conditional to still needing two players
            await Clients.All.SendAsync("AddPlayer", Context.ConnectionId); 

            await base.OnConnectedAsync();
        }

        public class Connections
        {
            public string Groupname { get; set; }
            public string ConnectionId { get; set; }
            public string Color { get; set; }

            public Connections(string groupname, string connectionId, string color)
            {
                Groupname = groupname;
                ConnectionId = connectionId;
                Color = color;
            }
        }
    }
}
