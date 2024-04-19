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
    public class ADOFAIEventsManager
    {
        private Dictionary<string, IADOFAIEvent> strings = new();
        private Dictionary<ADOFAIEventsEnum, string> enums = new();
        public void RegisterEvent(string stringType, ADOFAIEventsEnum enumType, IADOFAIEvent adofaiEvent)
        {
            strings.Add(stringType, adofaiEvent);
            enums.Add(enumType, stringType);
        }
        public T? CreateEvent<T>(ADOFAIEventsEnum value) where T : IADOFAIEvent
        {
            if (enums.TryGetValue(value, out string? v) && v is not null)
            {
                if (strings.TryGetValue(v, out IADOFAIEvent? result) && result is not null)
                {
                    T? result2 = (T)result;
                    if (result2 is not null) return result2;
                }
            }
            return default(T);
        }
        public void DefaultInitialization()
        {
            RegisterEvent("MoveDecorations", ADOFAIEventsEnum.MoveDecorations, new MoveDecorations());
        }
    }
}
