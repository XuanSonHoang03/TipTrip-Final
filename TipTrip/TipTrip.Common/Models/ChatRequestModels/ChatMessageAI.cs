using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TipTrip.Common.Models.ChatRequestModels
{
	public class ChatMessageAI
	{
		[JsonPropertyName("message")]
		public string Message { get; set; }

		[JsonPropertyName("role")]
		public string Role { get; set; }
	}
}
