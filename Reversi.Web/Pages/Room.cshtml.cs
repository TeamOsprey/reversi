using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Reversi.WebApp.Pages
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