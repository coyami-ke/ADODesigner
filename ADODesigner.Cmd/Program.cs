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
                    (KeyFrame[], Decoration[]) result = commands[i].Run();

                    Console.WriteLine("Converting to ADOFAI level and writing to " + PATH_TO_RESULT + "...");
                    CustomLevelParser parser = new();
                    parser.AddEvents(result.Item1);
                    parser.AddDecorations(result.Item2);
                    File.WriteAllText(PATH_TO_RESULT, parser.ToJson());
                    Console.WriteLine("Complete!");
                    Console.WriteLine("  Number of events: " + parser.ADOFAILevel.Actions.Count);
                    Console.WriteLine("  Number of decorations: " + parser.ADOFAILevel.Decorations.Count);
                    Console.ReadKey();
                }
            }
        }
    }
}