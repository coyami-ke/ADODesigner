using ADODesigner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;

namespace ADODesigner.Converters
{
    public class CustomLevelParser
    {
        private CustomLevel customLevel = new();
        private JsonSerializerOptions serializerOptions;

        public CustomLevel ADOFAILevel
        {
            get => customLevel;
        }
        public CustomLevelParser()
        {
            customLevel = new();
            const int countTiles = 128;
            for (int i = 0; i < countTiles; i++)
            {
                customLevel.AngleData.Add(0);
            }
            serializerOptions = new();
            serializerOptions.AllowTrailingCommas = true;
            serializerOptions.IncludeFields = true;
            serializerOptions.UnknownTypeHandling = JsonUnknownTypeHandling.JsonNode;
        }
        public CustomLevelParser(CustomLevel level)
        {
            customLevel = level;
            serializerOptions = new();
            serializerOptions.AllowTrailingCommas = true;
            serializerOptions.IncludeFields = true;
            serializerOptions.UnknownTypeHandling = JsonUnknownTypeHandling.JsonNode;
        }
        public CustomLevel? Parse(string json)
        {
            return JsonSerializer.Deserialize<CustomLevel>(json, serializerOptions);
        }
        public string ToJson()
        {
            return JsonSerializer.Serialize(customLevel, serializerOptions);
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
    }
}
