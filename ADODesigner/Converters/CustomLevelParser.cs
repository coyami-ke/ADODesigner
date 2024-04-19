using ADODesigner.Animations;
using ADODesigner.Models;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ADODesigner.Converters
{
    public class CustomLevelParser : INotifyPropertyChanged
    {
        public const string ADODESIGNER_EVENT_NAME = "ADODesigner_Event";

        private CustomLevel customLevel = new();
        public JsonSerializerOptions SerializerOptions { get; }
        private ADOFAIEventsManager eventsManager;

        public event PropertyChangedEventHandler? PropertyChanged;
        public static JsonSerializerOptions GetDefaultJsonOptions()
        {
            JsonSerializerOptions options = new()
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                AllowTrailingCommas = true,
                IncludeFields = true,
                UnknownTypeHandling = JsonUnknownTypeHandling.JsonNode,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            };
            return options;
        }

        public CustomLevel ADOFAILevel
        {
            get
            {
                return customLevel;
            }
            set
            {
                customLevel = value;
            }
        }
        public CustomLevelParser() 
        {
            eventsManager = new();
            customLevel = new();
            JsonValue? settings = JsonValue.Create(new CustomLevelSettings());
            if (settings is not null)
            {
                customLevel.Settings = settings;
            }
            const int countTiles = 128;
            for (int i = 0; i < countTiles; i++)
            {
                customLevel.AngleData.Add(0);
            }
            SerializerOptions = new();
            SerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            SerializerOptions.AllowTrailingCommas = true;
            SerializerOptions.IncludeFields = true;
            SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            SerializerOptions.UnknownTypeHandling = JsonUnknownTypeHandling.JsonNode;
        }
        public CustomLevelParser(CustomLevel level)
        {
            eventsManager = new();
            customLevel = level;
            SerializerOptions = new();
            SerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            SerializerOptions.AllowTrailingCommas = true;
            SerializerOptions.IncludeFields = true;
            SerializerOptions.UnknownTypeHandling = JsonUnknownTypeHandling.JsonNode;
            SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        }
        
        public void Parse(string json)
        {
            CustomLevel? l = JsonSerializer.Deserialize<CustomLevel>(json, SerializerOptions);
            if (l is not null) ADOFAILevel = l;
        }
        public string ToJson()
        {
            return JsonSerializer.Serialize(customLevel, SerializerOptions);
        }

        public void AddEvent<T>(T value) where T : IADOFAIEvent
        {
            customLevel.Actions.Add(value);
        }
        public void AddEvents<T>(IEnumerable<T> value) where T : IADOFAIEvent
        {
            foreach (var i in value)
            {
                AddEvent<T>(i);
            }
        }
        public void AddEvent(KeyFrame value)
        {
            MoveDecorations moveDecorations = KeyFrameConverter.Convert(value);
            customLevel.Actions.Add(moveDecorations);
        }
        public void AddEvents(IEnumerable<KeyFrame> value)
        {
            foreach (var i in value)
            {
                AddEvent(i);
            }
        }
        public void AddDecoration(Decoration value)
        {
            AddDecoration addDecoration = DecorationConverter.Convert(value);
            customLevel.Decorations.Add(value);
        }
        public void AddDecorations(IEnumerable<Decoration> value)
        {
            foreach (var i in value)
            {
                AddDecoration(i);
            }
        }
        public T[] GetEvents<T>(string eventType) where T : IADOFAIEvent
        {
            List<T> result = new();
            foreach (var i in customLevel.Actions)
            {
                JsonNode? eventTypeNode = i;
                string? eventTypeValueNode = null;
                if (eventTypeNode is not null)
                {
                    eventTypeValueNode = (string?)eventTypeNode["eventType"];
                }
                if (eventType == eventTypeValueNode)
                {
                    T? value = i.Deserialize<T>();
                    if (value is not null) result.Add(value);
                }
            }
            return result.ToArray();
        }
        public void DeleteEvents<T>(string eventType) where T : IADOFAIEvent
        {
            for (int i = 0; i < customLevel.Actions.Count; i++)
            {
                JsonNode? eventTypeNode = i;
                string? eventTypeNodeValue = "";
                if (eventTypeNode is not null)
                {
                    eventTypeNodeValue = (string?)eventTypeNode;
                }
                if (eventType == eventTypeNodeValue)
                {
                    customLevel.Actions.RemoveAt(i);
                }
            }
        }
        public void AddAnimation(IBaseAnimation animation)
        {
            (KeyFrame[], Decoration[]) result = animation.CreateAnimation();
            for (int s = 0; s < result.Item1.Length; s++)
            {
                result.Item1[s].EventTag += ADODESIGNER_EVENT_NAME;
            }
            this.AddEvents(result.Item1);
        }
        public void AddAnimations(IEnumerable<IBaseAnimation> animations)
        {
            foreach (var i in animations)
            {
                (KeyFrame[], Decoration[]) result = i.CreateAnimation();
                for (int s = 0; s < result.Item1.Length; s++)
                {
                    result.Item1[s].EventTag += ADODESIGNER_EVENT_NAME;
                }
                this.AddEvents(result.Item1);
            }
        }
    }
}
