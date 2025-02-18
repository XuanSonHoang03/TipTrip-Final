using OpenAI.Chat;
using TipTrip.Common.Models.ChatRequestModels;
using TipTrip.Common.Models.TripModel;

namespace TipTrip.Implements.Interfaces
{
	public interface IOpenAIService
	{
		Task<ChatCompletion> GetPersonalizedTripPlanAsync(TripRequestModel requestModel);
	}
}
