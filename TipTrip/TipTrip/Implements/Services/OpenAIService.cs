using OpenAI.Chat;
using TipTrip.Common.Models;
using TipTrip.Common.Models.ChatRequestModels;
using TipTrip.Common.Models.TripModel;
using TipTrip.Implements.Interfaces;

namespace TipTrip.Implements.Services
{
    public class OpenAIService : IOpenAIService
    {
        private readonly ChatClient _chatClient;
        private readonly IConfiguration _configuration;

        public OpenAIService(IConfiguration configuration)
        {
            _configuration = configuration;
            var apiKey = configuration["OpenAI:open_ai_key"];
            _chatClient = new ChatClient(model: ModelAI.Model, apiKey: apiKey);
        }

        public async Task<ChatCompletion> GetPersonalizedTripPlanAsync(TripRequestModel requestModel)
        {
            var messages = new List<ChatMessage>
            {
               new SystemChatMessage($"Bạn là một trợ lý lên kế hoạch du lịch cá nhân hóa. " +
                    $"Hãy tạo một lịch trình chi tiết từ {requestModel.PickupPoint} đến {requestModel.Destination} và trả lại phản hồi bằng tiếng Việt. " +
                    $"Hãy lên kế hoạch chi tiết nhất có thể với các địa điểm, món ăn nổi tiếng với ngân sách {requestModel.Budget}. " +
                    $"Hãy chia lịch trình theo từng ngày một cách hợp lý, với các hoạt động theo từng khung giờ. " +
                    $"Đối với phần ghi chú, hãy cung cấp các mẹo hữu ích cho chuyến đi."),
                new UserChatMessage(GeneratePrompt(requestModel))
            };

            ChatCompletionOptions options = new()
            {
                ResponseFormat = ChatResponseFormat.CreateJsonSchemaFormat(
        jsonSchemaFormatName: "trip_planning",
        jsonSchema: BinaryData.FromBytes("""
        {
            "type": "object",
            "properties": {
                "destination": { "type": "string" },
                "start_date": { "type": "string" },
                "end_date": { "type": "string" },
                "transportation": { "type": "string" },
                "itinerary": {
                    "type": "array",
                    "items": {
                        "type": "object",
                        "properties": {
                            "day": { "type": "integer" },
                            "activities": {
                                "type": "array",
                                "items": {
                                    "type": "object",
                                    "properties": {
                                        "time": { "type": "string" },
                                        "location": { "type": "string" },
                                        "food_recommendations": {
                                            "type": "array",
                                            "items": { "type": "string" }
                                        }
                                    },
                                    "required": ["time", "location", "food_recommendations"],
                                    "additionalProperties": false
                                }
                            },
                            "budget": { "type": "number" },
                            "weather": {
                                "type": "object",
                                "properties": {
                                    "temperature": { "type": "string" },
                                    "description": { "type": "string" }
                                },
                                "required": ["temperature", "description"],
                                "additionalProperties": false
                            },
                            "notes": { "type": "string" }
                        },
                        "required": ["day", "activities", "budget", "weather", "notes"],
                        "additionalProperties": false
                    }
                }
            },
            "required": ["destination", "start_date", "end_date", "transportation", "itinerary"],
            "additionalProperties": false
        }
        """u8.ToArray()),
        jsonSchemaIsStrict: true)
            };


            ChatCompletion competition = await _chatClient.CompleteChatAsync(messages, options);
            return competition;
        }

        private string GeneratePrompt(TripRequestModel trip)
        {
            return $"""
            Tôi muốn lên kế hoạch cho một chuyến đi cá nhân hóa.

            - Điểm đến: {trip.Destination}
            - Ngày khởi hành: {trip.StartDate:yyyy-MM-dd}
            - Ngày kết thúc: {trip.EndDate:yyyy-MM-dd}
            - Phương tiện di chuyển: {trip.Transportation}
            - Ngân sách dự kiến: {trip.Budget} VND
            - Tôi thích trải nghiệm: {string.Join(", ", trip.Preferences)}
            - Tôi muốn tránh: {string.Join(", ", trip.Avoid)}
            - Món ăn yêu thích: {string.Join(", ", trip.FavoriteFoods)}
            - Hoạt động mong muốn: {string.Join(", ", trip.Activities)}

            Hãy tạo lịch trình chi tiết theo yêu cầu của tôi, bao gồm:
            - Giờ di chuyển và địa điểm
            - Món ăn nên thử
            - Ngân sách mỗi ngày
            - Dự báo thời tiết
            - Lưu ý quan trọng
            """;
        }
    }
}
