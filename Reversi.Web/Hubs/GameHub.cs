using Microsoft.AspNetCore.SignalR;
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
            // assign color to user
            // if BLACK's connection ID is null then set to Context.ConnectionId
            await base.OnConnectedAsync();
        }
    }
}
