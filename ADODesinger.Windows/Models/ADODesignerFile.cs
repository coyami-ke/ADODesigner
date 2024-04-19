using ADODesigner.Models;
using ADODesigner.Windows.Models.TimeLine;
using ADODesinger.Windows.Models.TimeLine;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ADODesinger.Windows.Models
{
    public partial class ADODesignerFile : ObservableObject
    {
        public static JsonSerializerOptions GetDefaultOptions()
        {
            return new JsonSerializerOptions() { IncludeFields = true, WriteIndented = true, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
        }
        [JsonIgnore]
        public Dictionary<string, Type> RegisterTimeLineTypes = new()
        {
            { "keyFrames", typeof(KeyFrameTimeLine) },
            { "ballsAnimations", typeof(BallsAnimationTimeLine) },
            { "frameToFrameAnimations", typeof(FrameToFrameTimeLine) }
        };

        [ObservableProperty]
        private string pathToADOFAILevel = Path.Combine(Environment.CurrentDirectory, "unsaved.adofai");
        [ObservableProperty]
        private int version = 1;
        [ObservableProperty]
        private Dictionary<string, JsonArray> timeLine = new();
        [JsonIgnore]
        public JsonSerializerOptions SerializerOptions { get; } = new JsonSerializerOptions() { IncludeFields = true, WriteIndented = true, };


        public string ToJson()
        {
            return JsonSerializer.Serialize(this, SerializerOptions);
        }
        public static ADODesignerFile? Parse(string json)
        {
            return JsonSerializer.Deserialize<ADODesignerFile>(json, ADODesignerFile.GetDefaultOptions());
        }
        public TimeLineElementModel[] GetTimeLineElements()
        {
            List<TimeLineElementModel> elements = new();
            foreach (var et in this.RegisterTimeLineTypes)
            {
                if (!this.TimeLine.ContainsKey(et.Key)) continue;
                JsonArray objects = this.TimeLine[et.Key];
                foreach (JsonNode? node in objects)
                {
                    TimeLineElementModel? desElement = (TimeLineElementModel?)JsonSerializer.Deserialize(node, et.Value, this.SerializerOptions);
                    if (desElement is not null) elements.Add(desElement);
                }
            }

            return elements.ToArray();
        }
        public void SetTimeLine(TimeLineElementModel[] elements)
        {
            this.TimeLine = new();
            foreach (var saveID in this.RegisterTimeLineTypes.Keys)
            {
                this.TimeLine.Add(saveID, new());
            }
            foreach (var e in elements)
            {
                this.TimeLine[e.SaveID].Add(JsonSerializer.SerializeToNode(e, this.RegisterTimeLineTypes[e.SaveID], this.SerializerOptions));
            }
        }
    }
}
