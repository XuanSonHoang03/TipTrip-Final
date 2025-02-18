using TipTrip.Common.Models;
using TipTrip.Common.Models.ChatRequestModels;
using TipTrip.Common.Models.TripModel;

namespace TipTrip.Implements.Interfaces
{
	public interface ISuggestionService
	{
		Task<ChatResponseModel> GetPersonalizedTripAsync(TripRequestModel request);
	}
}
