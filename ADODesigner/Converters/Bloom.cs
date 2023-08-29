using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ADODesigner.Converters
{
    public class Bloom : IADOFAIEvent
    {
        [JsonPropertyName("floor")]
        public int Floor { get; set; } = 1;
        [JsonPropertyName("eventType")]
        public string EventType { get; set; } = "Bloom";
        [JsonPropertyName("enabled")]
        public string Enabled { get; set; } = "Enabled";
        [JsonPropertyName("threshold")]
        public int Threshold { get; set; } = 50;
        [JsonPropertyName("intensity")]
        public int Intensity { get; set; } = 100;
        [JsonPropertyName("color")]
        public string Color { get; set; } = "FFFFFF";
        [JsonPropertyName("duration")]
        public int Duration { get; set; } = 0;
        [JsonPropertyName("ease")]
        public string Ease { get; set; } = "Linear";
        [JsonPropertyName("angleOffset")]
        public int AngleOffset { get; set; } = 0;
        [JsonPropertyName("eventTag")]
        public string EventTag { get; set; } = "";
    }
}
