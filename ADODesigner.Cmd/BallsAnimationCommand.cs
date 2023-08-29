using ADODesigner.Animations;
using ADODesigner.Converters;
using ADODesigner.Models;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json.Nodes;
#nullable disable
namespace ADODesigner.Cmd
{
    public class BallsAnimationCommand : IADODesignerCommand
    {
        public string Command { get; set; } = "balls";
        public string Description { get; set; } = "Create an animation of balls";
        public (KeyFrame[], Decoration[]) Run()
        {
            Command = "balls";
            Description = "Create a Create an animation of balls.";
            BallsAnimationArgs animationArgs = new();
            JsonSerializerOptions options = new();
            options.IncludeFields = true;
            options.WriteIndented = true;
            options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            Directory.CreateDirectory("config");
            if (!File.Exists(@"config\balls_animation.json"))
            {
                File.WriteAllText(@"config\balls_animation.json", JsonSerializer.Serialize(animationArgs, options));
            }
            else
            {
                animationArgs = JsonSerializer.Deserialize<BallsAnimationArgs>(File.ReadAllText(@"config\balls_animation.json"), options);
            }

            Console.Write("How many animations do you want to create?: ");
            int countAnimations = Convert.ToInt32(Console.ReadLine());
            Console.Write("Do you want to use 2.5D mode? Yes - [Y], No - [N] ");
            string answer = Console.ReadLine();
            bool use2point5mode;
            if (answer.ToLower() == "y")
            {
                use2point5mode = true;
            }
            else use2point5mode = false;

            List<KeyFrame> keyFrames = new(countAnimations * animationArgs.FrameRate * animationArgs.Duration);
            List<Decoration> decorations = new(animationArgs.Count);

            Console.ForegroundColor = ConsoleColor.Yellow;

            List<BallsAnimation> animations = new(countAnimations);
            List<(float, float)> positionsZ = new();
            List<(Vector2, Vector2)> positions = new(countAnimations);

            Console.WriteLine("Using 2.5D mode: " + use2point5mode);
            for (int i = 0; i < countAnimations; i++)
            {
                Console.WriteLine($"Enter four floating-point numbers. ( {i + 1} / {countAnimations} )");
                Vector2 firstPosition = new();
                Vector2 secondPosition = new();
                Console.Write("First Position X: ");
                firstPosition.X = Convert.ToSingle(Console.ReadLine());
                Console.Write("First Position Y: ");
                firstPosition.Y = Convert.ToSingle(Console.ReadLine());
                Console.Write("Second Position X: ");
                secondPosition.X = Convert.ToSingle(Console.ReadLine());
                Console.Write("Second Position Y: ");
                secondPosition.Y = Convert.ToSingle(Console.ReadLine());
                Console.Write("Easing function: ");
                Ease ease = Ease.Linear;
                try
                {
                    ease = Enum.Parse<Ease>(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine($"{ease} don't exists.");
                    Console.ReadKey();
                    return (keyFrames.ToArray(), decorations.ToArray());
                }

                BallsAnimation ballsAnimation = new(animationArgs);
                
                ballsAnimation.Args.Easing = ease;
                positions.Add((firstPosition, secondPosition));

                animations.Add(ballsAnimation);
            }

            bool isInvert = animationArgs.Invert;
            for (int i = 0; i < animations.Count; i++) 
            {
                animations[i].Args.Delay += animations[i].Args.Duration * i * 180; // 0 * 5 = 0; 1 * 5 = 5; 2 * 5 = 10; 3 * 5 = 15
                if (i > 1) animations[i].Args.Delay -= (i - 1) * animations[i].Args.Duration * 180;
                animations[i].Args.FirstFrame.PositionOffset = positions[i].Item1;
                animations[i].Args.SecondFrame.PositionOffset = positions[i].Item2;
                animations[i].Args.Invert = isInvert;
                if (use2point5mode)
                {
                    animations[i].Args.FirstFrame.Parallax = new(positionsZ[i].Item1);
                    animations[i].Args.SecondFrame.Parallax = new(positionsZ[i].Item2);
                    animations[i].Args.FirstFrame.Scale /= 100 * (100 - positionsZ[i].Item1);
                    animations[i].Args.SecondFrame.Scale /= 100 * (100 - positionsZ[i].Item2);
                }
                isInvert = !isInvert;
                Console.WriteLine($"Processing... ({i + 1} / {countAnimations})");
                (KeyFrame[], Decoration[]) processed = animations[i].CreateAnimation();
                keyFrames.AddRange(processed.Item1);
            }

            return (keyFrames.ToArray(), decorations.ToArray());
        }
    }
}
