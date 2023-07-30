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
            BallsAnimationArgs animationArgs = new();
            if (!File.Exists("balls_animation.json"))
            {
                File.Create("balls_animation.json").Dispose();
                File.WriteAllText("balls_animation.json", JsonSerializer.Serialize<BallsAnimationArgs>(animationArgs));
            }
            else
            {
                FileStream reader = new("balls_animation.json", FileMode.Open);
                animationArgs = JsonSerializer.Deserialize<BallsAnimationArgs>(reader);
            }

            Console.Write("How many animations do you want to create?: ");
            int countAnimations = Convert.ToInt32(Console.ReadLine());

            List<KeyFrame> keyFrames = new();
            List<Decoration> decorations = new();

            Console.ForegroundColor = ConsoleColor.Yellow;
            List<BallsAnimation> animations = new(countAnimations);

            for (int i = 0; i < countAnimations; i++)
            {
                Console.WriteLine($"Enter four floating-point numbers. ( {i + 1} / {countAnimations} )");
                Vector2 firstPosition = Vector2.Zero;
                Vector2 secondPosition = Vector2.Zero;
                Console.Write("First Position X: ");
                firstPosition.X = Convert.ToSingle(Console.ReadLine());
                Console.Write("First Position Y: ");
                firstPosition.Y = Convert.ToSingle(Console.ReadLine());
                Console.Write("Second Position X: "); 
                secondPosition.X = Convert.ToSingle(Console.ReadLine());
                Console.Write("Second Position Y: ");
                secondPosition.Y = Convert.ToSingle(Console.ReadLine());
                Console.Write("Easing function: ");
                Ease ease = Enum.Parse<Ease>(Console.ReadLine());
                BallsAnimation ballsAnimation = new(animationArgs);
                ballsAnimation.Args.Easing = ease;
                ballsAnimation.Args.FirstFrame.PositionOffset = new(firstPosition.X, firstPosition.Y);
                ballsAnimation.Args.SecondFrame.PositionOffset = new(secondPosition.X, secondPosition.Y);
                if (i + 2 % 2 == 0) ballsAnimation.Args.Invert = true;
                animations.Add(ballsAnimation);
            }
            for (int i = 0; i < countAnimations; i++)
            {
                Console.WriteLine($"Processing... ({i + 1} / {countAnimations})");
                animations[i].Args.Floor = animations[i].Args.Duration * i + 1;
                (KeyFrame[], Decoration[]) processed = animations[i].CreateAnimation();
                keyFrames.AddRange(processed.Item1);
                decorations.AddRange(processed.Item2);
            }
            keyFrames.OrderBy(p => p.AngleOffset);

            Console.WriteLine("Converting to ADOFAI level... ");
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
                decorations[i].Scale = new(100, 100);
                decorations[i].Image = "cat.png";
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
            Console.ReadKey();
        }
    }
}