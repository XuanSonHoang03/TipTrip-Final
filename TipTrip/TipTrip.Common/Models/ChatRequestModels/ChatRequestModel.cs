using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TipTrip.Common.Models.ChatRequestModels
{
	public class ChatRequestModel
	{
		[JsonPropertyName("model")]
		public string Model { get; set; }

		[JsonPropertyName("messages")]
		public List<ChatMessageAI> Messages { get; set; }

		[JsonPropertyName("max_tokens")]
		public int MaxTokens { get; set; } = 150;

		[JsonPropertyName("temperature")]
		public double Temperature { get; set; } = 0.7;

		[JsonPropertyName("top_p")]
		public double TopP { get; set; } = 1.0;

		[JsonPropertyName("role")]
		public string Role { get; set; }
	}
}
