using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Sockets;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Reversi.Web.Hubs
{
    public class GameHub : Hub
    {
        public async Task SendUpdate()
        {
            await Clients.All.SendAsync("ReceiveUpdate", "test string");
            // await Clients.Client(player).SendAsync("ReceiveUpdate"); work was started to recognize the player
        }
        public async Task AddPlayer()
        {
            await Clients.All.SendAsync("AddPlayer", "test string");
        }
        public async Task RemovePlayer()
        {            
            await Clients.All.SendAsync("RemovePlayer", "test string");
        }

        public override async Task OnConnectedAsync()
        {
            //await Groups.AddToGroupAsync(UserId, "Game1");
            // TODO: consider call to AddPlayer to be conditional to still needing two players
            await Clients.All.SendAsync("AddPlayer"); 

            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            //await Groups.RemoveFromGroupAsync(UserId, "Game1");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
