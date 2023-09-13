using ADODesigner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ADODesigner.Animations
{
    public class WindAnimationArgs
    {
        public Decoration Decoration { get; set; } = new();
        public int Count { get; set; } = 10;
        public float MinParallax { get; set; } = 75;
        public float MaxParallax { get; set; } = 95;
        public Vector2 PositionOffset { get; set; } = new();
    }
    public class WindAnimation : BaseAnimation<WindAnimationArgs>
    {
        public WindAnimation(WindAnimationArgs args) : base(args)
        {
        }
        public override (KeyFrame[], Decoration[]) CreateAnimation()
        {
            List<KeyFrame> keyFrames = new();
            List<Decoration> decorations = new();
            return (keyFrames.ToArray(), decorations.ToArray());
        }
    }
}
