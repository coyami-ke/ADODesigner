using ADODesigner.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ADODesigner.GUI.Json
{
    [JsonSourceGenerationOptions(WriteIndented = true, IncludeFields = true)]
    [JsonSerializable(typeof(BallsAnimationArgs))]
    internal partial class BallsAnimationArgsGen : JsonSerializerContext
    {

    }
}
