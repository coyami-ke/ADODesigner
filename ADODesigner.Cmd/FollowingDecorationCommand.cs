using ADODesigner.Animations;
using ADODesigner.Converters;
using ADODesigner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
#nullable disable
namespace ADODesigner.Cmd
{
    public static class FollowingDecorationCommand
    {
        public static void Run()
        {
            List<KeyFrame> keyFrames = new();
            List<Decoration> decorations = new();
            FollowingDecorationsArgs animationArgs = new();
            
            Console.WriteLine("Processing...");
            if (!File.Exists("following_animation.json"))
            {
                File.Create("following_animation.json").Dispose();
                File.WriteAllText("following_animation.json", JsonSerializer.Serialize<FollowingDecorationsArgs>(animationArgs));
            }
            else
            {
                FileStream reader = new("following_animation.json", FileMode.Open);
                animationArgs = JsonSerializer.Deserialize<FollowingDecorationsArgs>(reader);
            }
            FollowingDecorations animation = new(animationArgs);
            (KeyFrame[], Decoration[]) result = animation.CreateAnimation();

            keyFrames.AddRange(result.Item1);
            decorations.AddRange(result.Item2);

            CustomLevel customLevel = new();

            const int countTiles = 128;
            customLevel.AngleData = new();

            for (int i = 0; i < countTiles; i++)
            {
                customLevel.AngleData.Add(0);
            }

            for (int i = 0; i < keyFrames.Count; i++)
            {
                keyFrames[i].EventTag = "";
                customLevel.Actions.Add(KeyFrameConverter.Convert(keyFrames[i]));
            }

            for (int i = 0; i < decorations.Count; i++)
            {
                customLevel.Decorations.Add(DecorationConverter.Convert(decorations[i]));
            }

            string json = JsonSerializer.Serialize<CustomLevel>(customLevel);
            Console.WriteLine("Writing to file result.adofai...");
            File.WriteAllText("result.adofai", json);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Complete!");
            Console.WriteLine("Result:");
            Console.WriteLine($"  Count keyframes: {keyFrames.Count}");
            Console.WriteLine($"  Count decorations: {decorations.Count}");
        }
    }
}
