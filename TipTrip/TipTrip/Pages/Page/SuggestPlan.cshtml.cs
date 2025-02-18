using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TipTrip.Common.Models;

namespace TipTrip.Pages.Page
{
    public class SuggestPlanModel : PageModel
    {

        [BindProperty]
        public ChatResponseModel messageResponse { get; set; }


        public IActionResult OnGet()
        {
            if (TempData["MessageResponse"] != null)
            {
                messageResponse = System.Text.Json.JsonSerializer.Deserialize<ChatResponseModel>(TempData["MessageResponse"].ToString());
            }

            return Page();
        }
    }
}
