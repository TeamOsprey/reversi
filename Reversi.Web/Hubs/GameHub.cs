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
    }
}
