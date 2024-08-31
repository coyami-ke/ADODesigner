using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ADODesigner.Converters.Events
{
    public class Bookmark
    {
        [JsonPropertyName("eventType")]
        public string EventType { get; set; } = "Bookmark";
        [JsonPropertyName("floor")]
        public int Floor { get; set; } = 1;
    }
}
