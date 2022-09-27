using Microsoft.AspNetCore.SignalR;
using System;
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
        public async Task AddPlayer()
        {
            await Clients.All.SendAsync("AddPlayer", Context.ConnectionId);
        }
        public async Task RemovePlayer()
        {            
            await Clients.All.SendAsync("RemovePlayer", Context.ConnectionId);
        }

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "Game1");
            // TODO: consider call to AddPlayer to be conditional to still needing two players
            //await Clients.All.SendAsync("AddPlayer", Context.ConnectionId); 

            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Game1");        
            await base.OnDisconnectedAsync(exception);
        }

        public class Connections
        {
            public string GroupName { get; set; }
            public string ConnectionId { get; set; }
            public string Color { get; set; }

            public Connections(string groupName, string connectionId, string color)
            {
                GroupName = groupName;
                ConnectionId = connectionId;
                Color = color;
            }
        }
    }
}
