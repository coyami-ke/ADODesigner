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
    public static class ADOFAIEventConverter
    {
        public static IADOFAIEvent JsonObjectToEvent(JsonObject value)
        {
            IADOFAIEvent result = new MoveDecorations();
            switch ((string)value["eventType"])
            {
                case "MoveDecorations":
                    result = new MoveDecorations();
                    break;
                case "Bloom":
                    result = new Bloom();
                    break;
                case "Twirl":
                    result = new Twirl();
                    break;
            }

            Type type = result.GetType();
            foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic))
            {
                string name = prop.GetCustomAttribute<JsonPropertyNameAttribute>().Name;
                foreach (var element in value)
                {
                    if (element.Key == name)
                    {
                        object propValue = prop.GetValue(null);
                        if (propValue is float)
                        {
                            prop.SetValue(null, (float)value[name]);
                        }
                        if (propValue is int)
                        {
                            prop.SetValue(null, (int)value[name]);
                        }
                        if (propValue is string)
                        {
                            prop.SetValue(null, (string)value[name]);
                        }
                        if (propValue is float[])
                        {
                            float[] array = propValue as float[];
                            for (int i = 0; i < array.Length; i++)
                            {
                                array[i] = (float)value[name];
                                prop.SetValue(null, array);
                            }
                        }
                        if (propValue is int[])
                        {
                            int[] array = propValue as int[];
                            for (int i = 0; i < array.Length; i++)
                            {
                                array[i] = (int)value[name];
                                prop.SetValue(null, array);
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}
