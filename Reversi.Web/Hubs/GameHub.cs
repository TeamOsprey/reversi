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
        //        public string UserId => Context.UserIdentifier;

        public string UserId
        {
            get
            {
                return "TEST";
                //if(!Context.GetHttpContext().Request.Cookies.ContainsKey("ReversiPlayerCookie"))
                // {
                //     var userId = DateTime.Now.TimeOfDay.ToString();
                //     Context.GetHttpContext().Response.Cookies.Append("ReversiPlayerCookie", userId);
                //     return userId;
                // }
                // var userId3 = Context.GetHttpContext().Request.Cookies["ReversiPlayerCookie"];
                // return userId3;
            }
        }
        //public string UserId => "player1";
        //{
        //    get
        //    {
        //        if (Context.User == null)
        //        {
        //            HttpContext.User = new GenericPrincipal(new FormsIdentity(ticket), roles);
        //            HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(
        //                new System.Security.Principal.GenericIdentity(person.LoginName),
        //                new string[] { /* fill roles if any */ });
        //        }
        //        //HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(
        //        //    new System.Security.Principal.GenericIdentity(person.LoginName),
        //        //    new string[] { /* fill roles if any */ });
        //    }
        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

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
