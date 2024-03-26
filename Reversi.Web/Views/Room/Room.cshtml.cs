using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Reversi.WebApp.Views.Room
{
    public class RoomModel : PageModel
    {
        public string UserId { get; set; }
        
        public void OnGet(string userId)
        {
            UserId = userId;
        }
    }
}
