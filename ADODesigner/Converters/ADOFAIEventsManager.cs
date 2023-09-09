using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ADODesigner.Converters
{
    public static class ADOFAIEventsManager
    {
        private static List<IADOFAIEvent> events = new List<IADOFAIEvent>();

        public static void RegisterEvent(IADOFAIEvent adofaiEvent)
        {
            events.Add(adofaiEvent);
        }
        public static IADOFAIEvent? GetEvent(string eventType)
        {
            IADOFAIEvent? adofaiEvent = null;

            for (int i = 0; i < events.Count; i++)
            {
                if (events[i].EventType == eventType)
                {
                    adofaiEvent = events[i];
                }
            }
            return adofaiEvent;
        }
        public static T? GetEvent<T>(string eventType) where T : IADOFAIEvent
        {
            for (int i = 0; i < events.Count; i++)
            {
                if (events[i].EventType == eventType)
                {
                    return (T)events[i];
                }
            }
            return default;
        }
        public static IADOFAIEvent?[] GetEvents()
        {
            return events.ToArray();
        }
        public static JsonObject? ConvertEventToJson<T>(T value) where T : IADOFAIEvent
        {
            JsonValue? json = JsonValue.Create(value);
            if (json is null) return null;
            return json.AsObject();
        }
    }
}
