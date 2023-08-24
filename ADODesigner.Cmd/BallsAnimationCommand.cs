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
    public static class BallsAnimationCommand
    {
        public static void Run()
        {
            BallsAnimationArgs animationArgs = new();
            Directory.CreateDirectory("config");
            if (!File.Exists(@"config\balls_animation.json"))
            {
                File.WriteAllText(@"config\balls_animation.json", JsonSerializer.Serialize(animationArgs));
            }
            else
            {
                animationArgs = JsonSerializer.Deserialize<BallsAnimationArgs>(File.ReadAllText(@"config\balls_animation.json"));
            }

            Console.Write("How many animations do you want to create?: ");
            int countAnimations = Convert.ToInt32(Console.ReadLine());

            List<KeyFrame> keyFrames = new();
            List<Decoration> decorations = new();

            Console.ForegroundColor = ConsoleColor.Yellow;
            List<BallsAnimation> animations = new(countAnimations);

            List<(Vector2, Vector2)> positions = new();
            for (int i = 0; i < countAnimations; i++)
            {
                Console.WriteLine($"Enter four floating-point numbers. ( {i + 1} / {countAnimations} )");
                Vector2 firstPosition = new();
                Vector2 secondPosition = new();
                try
                {
                    Console.Write("First Position X: ");
                    firstPosition.X = Convert.ToSingle(Console.ReadLine());
                    Console.Write("First Position Y: ");
                    firstPosition.Y = Convert.ToSingle(Console.ReadLine());
                    Console.Write("Second Position X: ");
                    secondPosition.X = Convert.ToSingle(Console.ReadLine());
                    Console.Write("Second Position Y: ");
                    secondPosition.Y = Convert.ToSingle(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Wrong format.");
                    Console.ReadKey();
                    return;
                }
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
                    return;
                }

                BallsAnimation ballsAnimation = new(animationArgs);
                
                ballsAnimation.Args.Easing = ease;
                positions.Add((firstPosition, secondPosition));
                //ballsAnimation.Args.FirstFrame.PositionOffset = new(firstPosition.X, firstPosition.Y);
                //ballsAnimation.Args.SecondFrame.PositionOffset = new(secondPosition.X, secondPosition.Y);

                animations.Add(ballsAnimation);
            }
            // 0, 1, 2, 3, 4
            bool isInvert = animationArgs.Invert;
            for (int i = 0; i < animations.Count; i++) 
            {
                animations[i].Args.Floor = animations[i].Args.Duration * i + 1;
                animations[i].Args.FirstFrame.PositionOffset = positions[i].Item1;
                animations[i].Args.SecondFrame.PositionOffset = positions[i].Item2;
                animations[i].Args.Invert = isInvert;
                isInvert = !isInvert;
                Console.WriteLine($"Processing... ({i + 1} / {countAnimations})");
                (KeyFrame[], Decoration[]) processed = animations[i].CreateAnimation();
                keyFrames.AddRange(processed.Item1);
            }

            CustomLevel customLevel = new();

            const int countTiles = 128;
            customLevel.AngleData = new();

            Console.WriteLine("Converting to ADOFAI Level...");

            for (int i = 0; i < countTiles; i++)
            {
                customLevel.AngleData.Add(0);
            }

            for (int i = 0; i < keyFrames.Count; i++)
            {
                MoveDecorations moveDecorations = KeyFrameConverter.Convert(keyFrames[i]);
                customLevel.Actions.Add(moveDecorations);
            }
            
            Console.WriteLine("Writing custom level to json file...");
            File.WriteAllText("result.adofai", JsonSerializer.Serialize(customLevel!));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Complete!");
            Console.WriteLine("Result:");
            Console.WriteLine($"  Count keyframes: {keyFrames.Count}");
            Console.WriteLine($"  Count decorations: {decorations.Count}");
        }
    }
}
