using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reversi.Web.Hubs
{
    public class GameHub : Hub
    {
        public static List<Connections> ConnectionList = new List<Connections>();

        public async Task SendUpdate()
        {
            await Clients.All.SendAsync("ReceiveUpdate");
            // await Clients.Client(player).SendAsync("ReceiveUpdate"); work was started to recognize the player
        }
        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "Game1");
            //await Clients.All.SendAsync("ConnectionId = " + Context.ConnectionId); 
            if (ConnectionList.Count == 0)
            {
                ConnectionList.Add(new Connections("Game1", Context.ConnectionId, "BLACK"));
            }
            else if (ConnectionList.Count == 1)
            {
                ConnectionList.Add(new Connections("Game1", Context.ConnectionId, "WHITE"));
            }
            // assign color to user
            // if BLACK's connection ID is null then set to Context.ConnectionId
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
