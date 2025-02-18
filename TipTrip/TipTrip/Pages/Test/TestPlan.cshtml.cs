using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using TipTrip.Common.Models;
using TipTrip.Common.Models.TripModel;
using TipTrip.Implements.Interfaces;

namespace TipTrip.Pages.Test
{
    public class TestPlanModel : PageModel
    {
        private readonly ISuggestionService _suggestionService;

        public TestPlanModel(ISuggestionService suggestionService)
        {
            _suggestionService = suggestionService;
        }

        [BindProperty]
        public string messageRequest { get; set; } = "";  

        public ChatResponseModel messageResponse { get; set; }

		[BindProperty]
		public SearchTourModel SearchTour { get; set; } = new SearchTourModel();

		public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) return Page();

            var tripRequest = new TripRequestModel
            {
                Destination = "Hà Nội",
                Budget = 1000000,  
                StartDate = DateTime.Now,  
                EndDate = DateTime.Parse("04/09/2025"),  
                Transportation = "Xe máy", 
                Avoid = new List<string> { }, 
                FavoriteFoods = new List<string> { "Phở", "Bún chả" },  
                Activities = new List<string> { "Tham quan địa danh lịch sử", "Trải nghiệm cà phê" },  
                Preferences = new List<string> { "Ẩm thực", "Văn hóa", "Du lịch mạo hiểm" } 
            };

            // Get personalized trip plan from the service
            messageResponse = await _suggestionService.GetPersonalizedTripAsync(tripRequest);

            return Page();
        }
    }
}
