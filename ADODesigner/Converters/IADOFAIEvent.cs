using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
#nullable disable
namespace ADODesigner.Converters
{
    public interface IADOFAIEvent
    {
        public string EventType { get; set; }
        public int Floor { get; set; }
        
    }
}
