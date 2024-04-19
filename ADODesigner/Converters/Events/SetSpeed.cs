using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ADODesigner.Converters.Events
{
    public class SetSpeed : IADOFAIEvent
    {
        [JsonPropertyName("eventType")]
        public string EventType { get; set; } = "SetSpeed";
        [JsonPropertyName("floor")]
        public int Floor { get; set; } = 1;
        [JsonPropertyName("speedType")]
        public string SpeedType { get; set; } = "";
        [JsonPropertyName("beatsPerMinute")]
        public float BPM { get; set; } = 100;
        [JsonPropertyName("bpmMultiplier")]
        public float BPMMultiplier { get; set; } = 1;
        [JsonPropertyName("angleOffset")]
        public float AngleOffset { get; set; } = 0;
    }
}
