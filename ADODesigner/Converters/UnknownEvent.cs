using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ADODesigner.Converters
{
    public class UnknownEvent : IADOFAIEvent
    {
        [JsonPropertyName("eventType")]
        public string EventType { get; set; } = "UnknownEvent";
        [JsonPropertyName("floor")]
        public int Floor { get; set; } = 0;
    }
}
