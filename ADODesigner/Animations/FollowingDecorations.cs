using ADODesigner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ADODesigner.Animations
{
    public class FollowingDecorationsArgs
    {
        public int Count { get; set; } = 12;
        public string Tag { get; set; } = "Rectange";
        public int Offset { get; set; } = 45;
        public Vector2 OffsetPosition { get; set; } = new(12, 12);
        public KeyFrame BaseKeyFrame { get; set; } = new();
    }
    public class FollowingDecorations : BaseAnimation<FollowingDecorationsArgs>
    {
        public FollowingDecorations(FollowingDecorationsArgs args) : base(args)
        {
        }
        public override (KeyFrame[], Decoration[]) CreateAnimation()
        {
            List<KeyFrame> keyFrames = new();
            List<Decoration> decorations = new();

            for (int i = 0; i < Args.Count; i++)
            {
                Decoration decoration = new();
                KeyFrame keyFrame = Args.BaseKeyFrame;
                decoration.ParallaxOffset = i * Args.OffsetPosition;
                keyFrame.AngleOffset += i * Args.Offset;
                keyFrames.Add(keyFrame);
                decorations.Add(decoration);
            }

            return (keyFrames.ToArray(), decorations.ToArray());
        }
    }
}
