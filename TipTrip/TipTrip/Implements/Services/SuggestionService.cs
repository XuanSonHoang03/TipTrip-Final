using TipTrip.Common.Models;
using TipTrip.Common.Models.ChatRequestModels;
using TipTrip.Common.Models.TripModel;
using TipTrip.Implements.Interfaces;

namespace TipTrip.Implements.Services
{
	public class SuggestionService : ISuggestionService
	{
		private readonly IOpenAIService _openAIService;

        public SuggestionService(IOpenAIService openAIService)
        {
            _openAIService = openAIService;
        }

        public async Task<ChatResponseModel> GetPersonalizedTripAsync(TripRequestModel request)
		{
			//todos: Get the model setting and handle logic


			/*var chatRequest = new ChatRequestModel
			{
				Messages = new List<ChatMessageAI>
				{
					new ChatMessageAI { Message = text, Role = SummarizeConstants.UserRole }
				},
				// Add any additional configuration settings for summarization here.
			};*/

			if((request.StartDate >= request.EndDate) || (request.Destination == request.PickupPoint))
			{
				return new ChatResponseModel
				{
					CreateAt = DateTime.Now,
					Model = "Please check again your opinion."
				};
			}

			var resultSummarize = await _openAIService.GetPersonalizedTripPlanAsync(request);

			// Check if the result is null to avoid NullReferenceException
			if (resultSummarize == null)
			{
				throw new InvalidOperationException(ErrorMessage.FailToGetResponse);
			}

            TripContent jsonData = null;

			try
			{
				var resultString = resultSummarize.Content?.Select(x => x.Text)?.FirstOrDefault();
				jsonData = System.Text.Json.JsonSerializer.Deserialize<TripContent>(resultString);
			}
			catch (Exception ex)
			{
				throw;
			}

			var finalSummarize = new ChatResponseModel
			{
				CreateAt = resultSummarize.CreatedAt,
				Model = resultSummarize.Model,
				Id = resultSummarize.Id,
				Role = resultSummarize.Role.ToString(),
				Content = new TripContent
				{
					Destination = jsonData.Destination,
					EndDate = jsonData.EndDate,
					Itinerary = jsonData.Itinerary,
					StartDate = jsonData.StartDate,
					Transportation = jsonData.Transportation
				},
				Usage = new TokenUsage
				{
					OutputTokenCount = resultSummarize.Usage.OutputTokenCount,
					InputTokenCount = resultSummarize.Usage.InputTokenCount,
					TotalTokenCount = resultSummarize.Usage.TotalTokenCount,
				}
			};

			return finalSummarize;
		}
	}
}
