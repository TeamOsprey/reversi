using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Reversi.WebApp.Hubs
{
    public class GameHub : Hub
    {
        // todo: See if can remove unused string parameter (hack)
        private const string UnusedParameterToGetAroundRuntimeErrorMystery = "foo";

        public async Task RefreshUITask()
        {
            await Clients.All.SendAsync("RefreshUI");
        }

        public async Task AddPlayerTask(string newUserId)
        {
            await Clients.All.SendAsync("AddPlayer", newUserId);
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

#nullable enable
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            //await Groups.RemoveFromGroupAsync(UserId, "Game1");
            await base.OnDisconnectedAsync(exception);
        }
#nullable disable

    }
}
