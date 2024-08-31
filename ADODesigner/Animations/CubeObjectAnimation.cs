using ADODesigner.Models;
using ADODesigner.Reflection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ADODesigner.Animations
{
    public class CubeObjectAnimation : ICloneable
    {
        public const float SIZE_PIXEL = ADOFAIConst.SIZE_PIXEL_IN_TILES;
        [UsageWindowProperties(AddToWindowProperties = false)]
        public int Floor { get; set; } = 1;
        public string ImageRectangle { get; set; } = "rectangle.png";
        public string Tag { get; set; } = "ADODesigner_Cube";
        public Vector2 Position { get; set; } = new();
        public float Scale { get; set; } = 150;
        public float Parallax { get; set; } = 50;
        public Vector2 ParallaxOffsetSides { get; set; } = new();
        public float Opacity { get; set; } = 100;
        public int Depth { get; set; } = 50;
        public float ParallaxMultiplier { get; set; } = 4;
        [UsageWindowProperties(AddToWindowProperties = true, IsColor = true, Name = "ColorLeftSide")]
        public string ColorLeftSide { get; set; } = "ffffffff";
        [UsageWindowProperties(AddToWindowProperties = true, IsColor = true, Name = "ColorRightSide")]
        public string ColorRightSide { get; set; } = "ffffffff";
        [UsageWindowProperties(AddToWindowProperties = true, IsColor = true, Name = "ColorFrontSide")]
        public string ColorFrontSide { get; set; } = "ffffffff";
        [UsageWindowProperties(AddToWindowProperties = true, IsColor = true, Name = "ColorTopSide")]
        public string ColorTopSide { get; set; } = "ffffffff";
        public bool EnableTopSide { get; set; } = true;
        public bool EnableFrontSide { get; set; } = true; 
        public bool EnableLeftSide { get; set; } = true;
        public bool EnableRightSide { get; set; } = true;

        public Decoration[] Animate()
        {
            List<Decoration> list = new();

            if (EnableFrontSide)
            {
                Decoration frontSide = new();
                frontSide.Scale = new(Scale);
                frontSide.Floor = Floor;
                frontSide.Position = Position;
                frontSide.Color = ColorFrontSide;
                frontSide.Depth = Depth;
                frontSide.Tag = Tag;
                frontSide.Parallax = new(Parallax);
                frontSide.Image = ImageRectangle;

                list.Add(frontSide);
            }

            Vector2 parallaxOffsetSides = SIZE_PIXEL * ParallaxOffsetSides * 2;

            for (int s = 0; s < Scale; s++)
            {
                int i = s + 1;
                if (EnableTopSide)
                {
                    Decoration topSide = CreateNewDecoration();
                    topSide.Scale = new(Scale - (i / 2), 1);
                    topSide.Position = Position;
                    topSide.Color = ColorTopSide;
                    topSide.Parallax = new(Parallax + SIZE_PIXEL * i * ParallaxMultiplier);
                    topSide.ParallaxOffset = new Vector2(0, Scale / 150 - (0.25f * (Scale / 150))) + parallaxOffsetSides * -i;

                    list.Add(topSide);
                }
                if (EnableRightSide)
                {
                    Decoration rightSide = CreateNewDecoration();
                    rightSide.Scale = new(1, Scale - (i / 2));
                    rightSide.Color = ColorRightSide;

                    float posX = parallaxOffsetSides.X * -i + (Scale / 300 + Scale / 600) - i * SIZE_PIXEL / 2.75f; //
                    float posY = parallaxOffsetSides.Y * -i + (SIZE_PIXEL * i / 2.625f);

                    rightSide.ParallaxOffset = new(posX, posY);
                    rightSide.Parallax = new Vector2(Parallax + SIZE_PIXEL * i * ParallaxMultiplier);

                    rightSide.Depth++;
                    list.Add(rightSide);
                }
                if (EnableLeftSide)
                {
                    Decoration leftSide = CreateNewDecoration();
                    leftSide.Scale = new(1, Scale - (i / 2));
                    leftSide.Color = ColorLeftSide;

                    float posX = parallaxOffsetSides.X * -i + -(Scale / 300 + Scale / 600) + i * SIZE_PIXEL / 2.75f;
                    float posY = parallaxOffsetSides.Y * -i + (SIZE_PIXEL * i / 2.625f);

                    leftSide.ParallaxOffset = new(posX, posY);
                    leftSide.Parallax = new(Parallax + SIZE_PIXEL * i * ParallaxMultiplier);

                    leftSide.Depth++;
                    list.Add(leftSide);
                }
            }

            foreach (var deco in list)
            {
                deco.Position += Position;
            }
            return list.ToArray();
        }
        public object Clone()
        {
            return (CubeObjectAnimation)this.MemberwiseClone();
        }
        public Decoration CreateNewDecoration()
        {
            Decoration decoration = new()
            {
                Floor = Floor,
                Depth = Depth,
                Image = ImageRectangle,
                Tag = Tag,
                Opacity = Opacity,
                Position = Position,
            };
            return decoration;
        }
    }
}
