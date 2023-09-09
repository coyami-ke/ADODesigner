using ADODesigner.Animations;
using ADODesigner.Converters;
using ADODesigner.Models;
using System.Globalization;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Text.Json.Serialization;
#nullable disable
namespace ADODesigner.Cmd
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string PATH_TO_RESULT = "result.adofai";
            List<IADODesignerCommand> commands = new();
            Console.WriteLine("ADODesigner Command Line by Coyami-Ke");
            commands.Add(new BallsAnimationCommand());
            Console.WriteLine(@"Commands: ");
            for (int i = 0; i < commands.Count; i++)
            {
                Console.WriteLine($@"  ""{commands[i].Command}"": {commands[i].Description}");
            }
            Console.Write("Enter command: ");
            string command = Console.ReadLine();
            for (int i = 0; i < commands.Count; i++)
            {
                if (command == commands[i].Command)
                {
                    CustomLevel level = new();
                    const int countTiles = 128;
                    for (int j = 0; j < countTiles; j++)
                    {
                        level.AngleData.Add(0);
                    }
                    (KeyFrame[], Decoration[]) result = commands[i].Run();

                    Console.WriteLine("Converting to ADOFAI level and writing to " + PATH_TO_RESULT + "...");
                    foreach (var s in result.Item1)
                    {
                        MoveDecorations value = KeyFrameConverter.Convert(s);
                        level.Actions.Add(value);
                    }
                    foreach (var s in result.Item2)
                    {
                        AddDecoration value = DecorationConverter.Convert(s);
                        level.Decorations.Add(value);
                    }
                    JsonSerializerOptions jsonOptions = new();
                    jsonOptions.IncludeFields = true;
                    jsonOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    File.WriteAllText(PATH_TO_RESULT, JsonSerializer.Serialize(level, jsonOptions));
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Complete!");
                    Console.WriteLine("  Number of events: " + level.Actions.Count);
                    Console.WriteLine("  Number of decorations: " + level.Decorations.Count);
                    Console.ReadKey();
                }
            }
        }
    }
}