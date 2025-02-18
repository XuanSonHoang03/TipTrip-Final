using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TipTrip.Common.Models
{
    public class ChatResponseModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("create_at")]
        public DateTimeOffset CreateAt { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("content")]
        public TripContent Content { get; set; }

        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("usage")]
        public TokenUsage Usage { get; set; }
    }

    public class TripContent
    {
        [JsonPropertyName("destination")]
        public string Destination { get; set; }

        [JsonPropertyName("start_date")]
        public string StartDate { get; set; }

        [JsonPropertyName("end_date")]
        public string EndDate { get; set; }

        [JsonPropertyName("transportation")]
        public string Transportation { get; set; }

        [JsonPropertyName("itinerary")]
        public List<TripDay> Itinerary { get; set; }
    }

    public class TripDay
    {
        [JsonPropertyName("day")]
        public int Day { get; set; }

        [JsonPropertyName("activities")]
        public List<Activity> Activities { get; set; }

        [JsonPropertyName("weather")]
        public WeatherInfo Weather { get; set; }

        [JsonPropertyName("budget")]
        public decimal Budget { get; set; }

        [JsonPropertyName("notes")]
        public string Notes { get; set; }
    }

    public class Activity
    {
        [JsonPropertyName("time")]
        public string Time { get; set; }

        [JsonPropertyName("location")]
        public string Location { get; set; }

        [JsonPropertyName("food_recommendations")]
        public List<string> FoodRecommendations { get; set; }
    }

    public class WeatherInfo
    {
        [JsonPropertyName("temperature")]
        public string Temperature { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class TokenUsage
    {
        [JsonPropertyName("output_token_count")]
        public int OutputTokenCount { get; set; }

        [JsonPropertyName("input_token_count")]
        public int InputTokenCount { get; set; }

        [JsonPropertyName("total_token_count")]
        public int TotalTokenCount { get; set; }
    }
}
