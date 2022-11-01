using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reversi.Web.Hubs
{
    public class GameHub : Hub
    {
        //        public string UserId => Context.UserIdentifier;
        public string UserId => "player" + DateTime.Now.ToString();

        public async Task SendUpdate()
        {
            await Clients.All.SendAsync("ReceiveUpdate");
            // await Clients.Client(player).SendAsync("ReceiveUpdate"); work was started to recognize the player
        }
        public async Task AddPlayer()
        {
            await Clients.All.SendAsync("AddPlayer", UserId);
        }
        public async Task RemovePlayer()
        {            
            await Clients.All.SendAsync("RemovePlayer", UserId);
        }

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(UserId, "Game1");
            // TODO: consider call to AddPlayer to be conditional to still needing two players
            //await Clients.All.SendAsync("AddPlayer", UserId); 

            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Groups.RemoveFromGroupAsync(UserId, "Game1");        
            await base.OnDisconnectedAsync(exception);
        }
    }
}
