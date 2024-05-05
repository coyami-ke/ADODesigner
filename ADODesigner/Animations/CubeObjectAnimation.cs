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
        public Vector2 Position { get; set; } = new();
        public float SettingSides { get; set; } = 3;
        public float Scale { get; set; } = 150;
        public float Parallax { get; set; } = 50;
        public Vector2 ParallaxOffsetSides { get; set; } = new();
        public float Opacity { get; set; } = 100;
        public int Depth { get; set; } = 50;
        public float ParallaxMultiplier { get; set; } = 4;
        public string ColorLeftSide { get; set; } = "ffffffff";
        public string ColorRightSide { get; set; } = "ffffffff";
        public string ColorFrontSide { get; set; } = "ffffffff";
        public string ColorTopSide { get; set; } = "ffffffff";
        public string Tag { get; set; } = "ADODesigner_Cube";

        public Decoration[] Animate()
        {
            List<Decoration> list = new();

            Decoration frontSide = new();
            frontSide.Scale = new(Scale);
            frontSide.Floor = Floor;
            frontSide.Position = Position;
            frontSide.Color = ColorFrontSide;
            frontSide.Depth = Depth;
            frontSide.Tag = Tag;
            frontSide.Parallax = new(Parallax);
            frontSide.Image = ImageRectangle;

            float tPositionSides = (1 - (Parallax / 100)) * (SettingSides * (Scale / 150)); // 25% = 1.25; 50% = 3; 75% = 12; 90 = 75; 80 = ? // Multiplayer = 5 
            float tOffset = SIZE_PIXEL * (tPositionSides / 2);

            Vector2 parallaxOffsetSides = SIZE_PIXEL * ParallaxOffsetSides;

            // top side
            for (int i = 0; i < Scale; i++)
            {
                Decoration topSide = CreateNewDecoration();
                topSide.Scale = new(Scale - (i / 2), 1);
                topSide.Position = Position;
                topSide.Color = ColorTopSide;
                topSide.Parallax = new(Parallax + SIZE_PIXEL * i * ParallaxMultiplier);
                topSide.ParallaxOffset = new Vector2(0, Scale / 150 - (0.25f * (Scale / 150))) + parallaxOffsetSides * -i;
                topSide.Depth += 3;

                list.Add(topSide);
            }

            // right side
            for (int i = 0; i < Scale; i++)
            {
                Decoration rightSide = CreateNewDecoration();
                rightSide.Scale = new(1, Scale - (i / 2));
                rightSide.Color = ColorRightSide;

                Vector2 newPosition = new(tPositionSides - i * tOffset, i * tOffset);

                rightSide.Position = newPosition + Position;
                rightSide.Parallax = new(Parallax + SIZE_PIXEL * i * ParallaxMultiplier);

                rightSide.ParallaxOffset = parallaxOffsetSides * -i;

                rightSide.Depth += 2;
                list.Add(rightSide);
            }
            // left side
            for (int i = 0; i < Scale; i++)
            {
                Decoration leftSide = CreateNewDecoration();
                leftSide.Scale = new(1, Scale - (i / 2));
                leftSide.Color = ColorLeftSide;

                Vector2 newPosition = new(-(tPositionSides - i * tOffset), i * tOffset);

                leftSide.Position = newPosition + Position;
                leftSide.Parallax = new(Parallax + SIZE_PIXEL * i * ParallaxMultiplier);

                leftSide.ParallaxOffset = parallaxOffsetSides * -i;

                leftSide.Depth += 2;
                list.Add(leftSide);
            }

            list.Add(frontSide);

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
            };
            return decoration;
        }
    }
}
