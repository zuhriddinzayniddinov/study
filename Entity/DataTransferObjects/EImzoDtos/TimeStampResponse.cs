using System.Text.Json.Serialization;

namespace EimzoApi.Models
{
    public class TimeStampResponse
    {
        [JsonPropertyName("timeStampTokenB64")]
        public string TimeStampTokenB64 { get; set; }

        [JsonPropertyName("success")] public bool Success { get; set; }

        [JsonPropertyName("reason")] public string Reason { get; set; }
    }
}