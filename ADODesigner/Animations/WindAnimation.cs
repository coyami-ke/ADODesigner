using ADODesigner.Converters;
using ADODesigner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ADODesigner.Animations
{
    public enum LeftOrRight
    {
        Left,
        Right,
    }
    public class WindAnimationArgs
    {
        public LeftOrRight LeftOrRight { get; set; }
        public int Count { get; set; } = 25;
        public int Duration { get; set; } = 10;
        public int Seed { get; set; } = 15012008;
        public float Offset { get; set; } = 10;
        public List<Decoration> Decorations { get; set; } = new();
        public int Rotation { get; set; } = new();
    }
    public class WindAnimation : BaseAnimation<WindAnimationArgs>
    {
        public WindAnimation(WindAnimationArgs args) : base(args)
        {
        }

        public override (KeyFrame[], Decoration[]) CreateAnimation()
        {
            for (int i = 0; i < Args.Count; i++)
            {
                Random random = new(Args.Seed);
                int numberDecoration = random.Next(0, Args.Decorations.Count);
                Decoration decoration = Args.Decorations[numberDecoration];
                float positionX = decoration.Position.X;
                float positionY = decoration.Position.Y;
                positionX += Args.Offset;
                positionY += Args.Offset;
                decoration.Position = new(positionX, positionY);
                decoration.Rotation = random.Next(0, 180);

                List<KeyFrame> keyFramesForDecoration = new();
                //KeyFrame keyFrame = DecorationConverter.ToKeyFrame(decoration);
                //keyFrame.RotationOffset = 0;
            }
            List<KeyFrame> keyFrames = new();
            List<Decoration> decorations = new();
            return (keyFrames.ToArray(), decorations.ToArray());
        }
    }
}
