using ADODesigner.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ADODesigner.GUI.Json
{
    [JsonSerializable(typeof(WindAnimationArgs))]
    [JsonSourceGenerationOptions(IncludeFields = true, WriteIndented = true)]
    internal partial class WindAnimationArgsGen : JsonSerializerContext
    {

    }
}
