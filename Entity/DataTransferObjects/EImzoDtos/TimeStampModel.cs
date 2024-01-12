using System.Text.Json.Serialization;

namespace EimzoApi.Models
{
    public class TimeStampModel
    {
        [JsonPropertyName("timeStamp")]
        public string TimeStamp { get; set; }
    }
}
