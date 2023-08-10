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
            Console.WriteLine("ADODesigner Command Line by Coyami-Ke");
            Console.WriteLine(@"Commands: ");
            Console.WriteLine(@"  ""balls"" : Create an animation of balls.");
            Console.Write("Enter command: ");
            string command = Console.ReadLine();
            if (command == "balls") BallsAnimationCommand.Run();
            //if (command == "following") FollowingDecorationCommand.Run();
            Console.ReadKey();
        }
    }
}