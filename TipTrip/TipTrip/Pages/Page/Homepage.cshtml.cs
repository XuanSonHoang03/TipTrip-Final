using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TipTrip.Common.Models;
using TipTrip.Common.Models.TripModel;
using TipTrip.Implements.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TipTrip.Pages.Page
{
    public class HomepageModel : PageModel
    {
        private readonly ISuggestionService _suggestionService;

        public HomepageModel(ISuggestionService suggestionService)
        {
            _suggestionService = suggestionService;
        }

       /* [BindProperty]
        public string MessageRequest { get; set; } = "";*/

        public ChatResponseModel messageResponse { get; set; }

        [BindProperty]
        public SearchTourModel SearchTour { get; set; } = new SearchTourModel();

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelStateKey in ModelState.Keys)
                {
                    var modelStateVal = ModelState[modelStateKey];
                    foreach (var error in modelStateVal.Errors)
                    {
                        Console.WriteLine($"Key: {modelStateKey}, Error: {error.ErrorMessage}");
                    }
                }
                return Page();
            }

            var suggestRequest = new TripRequestModel
            {
                Destination = SearchTour.Location,
                StartDate = SearchTour.StartDate,
                EndDate = SearchTour.EndDate,
                Transportation = SearchTour.Transpotation, 
                Avoid = string.IsNullOrEmpty(SearchTour.Avoid) ? new List<string>() : new List<string> { SearchTour.Avoid },
                Budget = (decimal)SearchTour.Budget,
                Activities = new List<string>(),
                Preferences = new List<string>(),
                NumberOfPeople = SearchTour.NumberOfPeople
            };

            // Get personalized trip plan from the service
            messageResponse = await _suggestionService.GetPersonalizedTripAsync(suggestRequest);

            TempData["MessageResponse"] = System.Text.Json.JsonSerializer.Serialize(messageResponse);

            //return RedirectToPage("/TestResponse/IndexTest"); 
            return RedirectToPage("/Page/SuggestPlan");
        }
    }
}
