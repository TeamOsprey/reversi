using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Reversi.Web.Hubs
{
    public class GameHub : Hub
    {
        public async Task SendUpdate(string player)
        {
            await Clients.Client(player).SendAsync("ReceiveUpdate");
            
        }
    }
}
