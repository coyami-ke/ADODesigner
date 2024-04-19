using System.Text.Json.Serialization;

namespace ADODesigner.Converters.Events
{
    public class RepeatEvents
    {
        [JsonPropertyName("eventType")]
        public string EventType { get; set; } = "RepeatEvents";
        [JsonPropertyName("floor")]
        public int Floor { get; set; } = 1;
        [JsonPropertyName("repeatType")]
        public string RepeatType { get; set; } = "Beat";
        [JsonPropertyName("repetitions")]
        public int Repetitions { get; set; }
        [JsonPropertyName("floorCount")]
        public int FloorCount { get; set; }
        [JsonPropertyName("interval")]
        public float Interval { get; set; }
        [JsonPropertyName("executeOnCurrentFloor")]
        public bool ExecuteOnCurrentFloor { get; set; } = false;
        [JsonPropertyName("tag")]
        public string Tag { get; set; } = "";
    }
}
