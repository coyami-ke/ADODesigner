using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ADODesigner.Converters
{
    public class Twirl : IADOFAIEvent
    {
        [JsonPropertyName("eventType")]
        public string EventType { get; set; } = "Twirl";
        [JsonPropertyName("floor")]
        public int Floor { get; set; } = 1;
    }
}
