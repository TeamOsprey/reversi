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
        // todo: See if can remove unused string parameter (hack)
        private const string UnusedParameterToGetAroundRuntimeErrorMystery = "foo";

        public async Task RefreshUITask()
        {
            await Clients.All.SendAsync("RefreshUI");
            // await Clients.Client(player).SendAsync("ReceiveUpdate"); work was started to recognize the player
        }
        public async Task AddPlayerTask()
        {
            await Clients.All.SendAsync("AddPlayer"); // , UnusedParameterToGetAroundRuntimeErrorMystery
        }
        public async Task RemovePlayerTask()
        {            
            await Clients.All.SendAsync("RemovePlayer", UnusedParameterToGetAroundRuntimeErrorMystery);
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
