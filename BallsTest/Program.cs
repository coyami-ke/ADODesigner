using ADODesigner.Converters;
using ADODesigner.Models;

namespace BallsTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CustomLevelParser parser = new();
            const string tag = "Ball";
            for (int i = 0; i < 10; i++)
            {
                KeyFrame key = new()
                {
                    Tag = tag + i,
                    UsePositionOffset = false,
                    UseParallaxOffset = true,
                    ParallaxOffset = new(i * 1, 2.5f),
                    Duration = 0,
                    Ease = Ease.OutCubic,
                    Floor = 1,
                };
                parser.AddEvent(key);

                KeyFrame mainKey = new()
                {
                    Tag = tag + i,
                    UsePositionOffset = false,
                    UseParallaxOffset = true,
                    ParallaxOffset = new(i * 1, 5f),
                    Duration = 4,
                    Ease = Ease.OutCubic,
                    Floor = 1,
                    AngleOffset = 45 * (10 - i),
                };
                parser.AddEvent(mainKey);

                KeyFrame mainKey2 = new()
                {
                    Tag = tag + i,
                    UsePositionOffset = false,
                    UseParallaxOffset = true,
                    ParallaxOffset = new(i * 1, 0),
                    Duration = 4,
                    Ease = Ease.InCubic,
                    Floor = 1,
                    AngleOffset = 45 * (10 - i) + 720,
                };
                parser.AddEvent(mainKey2);
            }

            File.WriteAllText("file.adofai", parser.ToJson());

            Console.ReadKey();
        }
    }
}
