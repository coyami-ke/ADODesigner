using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace ADODesigner.Windows.Helpers
{
    public static class RemoverEventTag
    {
        public static bool ContainsTag(string tag, string[] properties, JsonObject? json)
        {
            JsonObject? obj = json?.AsObject();
            foreach (var property in properties)
            {
                if (json is not null && obj is not null && obj.ContainsKey(property))
                {
                    string? eventTag = (string?)obj[property];
                    if (eventTag is not null)
                    {
                        string[] tags = eventTag.Split(' ');
                        foreach (var str in tags)
                        {
                            if (str.Trim() == tag) return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
