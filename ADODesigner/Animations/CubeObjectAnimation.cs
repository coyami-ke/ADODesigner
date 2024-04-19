using ADODesigner.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ADODesigner.Animations
{
    public class CubeObjectAnimationArgs
    {
        public const float SIZE_PIXEL = 0.0066666666667f;
        public Vector2 Scale { get; set; } = new(150);
        public float Parallax { get; set; } = 50;
        public string ImageRectange { get; set; } = "rectange.png";
        public string ColorLeftSide { get; set; } = "ffffffff";
        public string ColorRightSide { get; set; } = "ffffffff";
        public string ColorFrontSide { get; set; } = "ffffffff";
        public string ColorTopSide { get; set; } = "ffffffff";
        public string Tag { get; set; } = "ADODesigner_Cube";
        public Vector2 Position { get; set; } = new();
    }
    public class CubeObjectAnimation : BaseAnimation<CubeObjectAnimationArgs>
    {

        public CubeObjectAnimation(CubeObjectAnimationArgs args) : base(args)
        {
        }
        public Vector2 GetParallax(int i)
        {
            return new(Args.Parallax + (i * ADOFAIConst.SIZE_PIXEL_IN_TILES * 2f));
        }
        public override (KeyFrame[], Decoration[]) CreateAnimation()
        {
            Decoration frontSide = new();
            frontSide.Scale = Args.Scale;
            frontSide.Color = Args.ColorFrontSide;
            frontSide.Parallax = new(Args.Parallax);
            frontSide.Image = Args.ImageRectange;
            frontSide.Position = Args.Position;
            frontSide.Depth = Convert.ToInt32(Args.Parallax);

            List<Decoration> result = new();
            List<Decoration> rightSide = new();
            List<Decoration> leftSide = new();
            List<Decoration> topSide = new();
            List<Decoration> bottomSide = new();

            // Top Side
            for (int i = 0; i < Args.Scale.Y; i++)
            {
                Decoration deco = new();
                deco.Image = Args.ImageRectange;
                deco.ParallaxOffset = new(0, 0.75f);
                deco.Color = Args.ColorTopSide;

                deco.Scale = new(Args.Scale.X - (i / 2), 1);
                deco.Parallax = GetParallax(i);
                deco.Depth = (int)Args.Parallax;
                deco.Position = new(0, 0);

                deco.Tag = $"{Args.Tag} {Args.Tag}_TopSide";

                topSide.Add(deco);
            }
            // Right Side
            for (int i = 0; i < Args.Scale.X; i++)
            {
                Decoration deco = new();
                deco.Image = Args.ImageRectange;
                deco.ParallaxOffset = new(0.75f, 0);
                deco.Color = Args.ColorRightSide;

                deco.Scale = new(1, Args.Scale.Y - (i / 2));
                deco.Parallax = GetParallax(i);
                deco.Depth = (int)Args.Parallax + 1;
                deco.Position = new(-(i * ADOFAIConst.SIZE_PIXEL_IN_TILES * 0.75f), (i * ADOFAIConst.SIZE_PIXEL_IN_TILES / 1.25f));

                deco.Tag = $"{Args.Tag} {Args.Tag}_RightSide";

                rightSide.Add(deco);
            }
            // Left Side
            for (int i = 0; i < Args.Scale.X; i++)
            {
                Decoration deco = new();
                deco.Image = Args.ImageRectange;
                deco.ParallaxOffset = new(-0.75f, 0);
                deco.Color = Args.ColorLeftSide;

                deco.Scale = new(1, Args.Scale.Y - (i / 2)); ;
                deco.Parallax = GetParallax(i); ;
                deco.Depth = (int)Args.Parallax + 2;
                deco.Position = new(i * ADOFAIConst.SIZE_PIXEL_IN_TILES * 0.75f, (i * ADOFAIConst.SIZE_PIXEL_IN_TILES / 1.25f));

                deco.Tag = $"{Args.Tag} {Args.Tag}_LeftSide";
                leftSide.Add(deco);
                
            }

            result.AddRange(topSide.ToArray());
            result.AddRange(rightSide.ToArray());
            result.AddRange(leftSide.ToArray());
            result.AddRange(bottomSide.ToArray());
            result.Add(frontSide);

            for (int i = 0; i < result.Count; i++)
            {
                Console.WriteLine(result[i].ToString());
                result[i].Position += Args.Position;             
            }

            return (Array.Empty<KeyFrame>(), result.ToArray());
        }
    }
}
