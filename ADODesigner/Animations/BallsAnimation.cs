using ADODesigner.Models;
using ADODesigner.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace ADODesigner.Animations
{
    public class BallsAnimationArgs
    {
        public int AngleOffset { get; set; } = 45;
        public int Intensivity { get; set; } = 1;
        public int Count { get; set; } = 5;
        public Ease Easing { get; set; } = Ease.Linear;
        public KeyFrame FirstFrame { get; set; } = new();
        public KeyFrame SecondFrame { get; set; } = new();
        public bool IsCurve { get; set; } = true;
        public int FrameRate { get; set; } = 60;
        public int Duration { get; set; } = 5;
        public int Floor { get; set; } = 1;
        public bool Invert { get; set; } = false;
        public bool VerticalInvert { get; set; } = false;
        public string Tag { get; set; } = "Ball";
    }
    /// <summary>
    /// Class for creating procedural animation of ADOFAI balls.
    /// </summary>
    public class BallsAnimation : BaseAnimation<BallsAnimationArgs>
    {
        public BallsAnimation(BallsAnimationArgs args) : base(args)
        {
            
        }
        public override (KeyFrame[], Decoration[]) CreateAnimation()
        {
            int countFrames = Args.Duration * Args.FrameRate;

            List<Decoration> decorations = new(Args.Count);
            List<KeyFrame> keyFrames = new(countFrames);

            for (int i = 0; i < Args.Count; i++)
            {
                Decoration decoration = new();
                decoration.Position = Args.FirstFrame.PositionOffset;
                decoration.Tag = "Ball" + i;
                decoration.Opacity = Args.FirstFrame.Opacity;
                decoration.Scale = Args.FirstFrame.Scale;
                decorations.Add(decoration);
            }

            if (Args.IsCurve)
            {
                for (int i = 0; i < decorations.Count; i++)
                {
                    for (int s = 0; s < countFrames; s++)
                    {
                        KeyFrame keyFrame = new();
                        keyFrame.Duration = 1 / countFrames;
                        keyFrame.AngleOffset = (180 / Args.FrameRate * (s + 1)) + i * Args.AngleOffset;
                        keyFrame.Tag = Args.Tag + i;
                        keyFrame.Key = "BallsAnimation" + decorations[i].Tag + s.ToString();
                        keyFrame.Floor = Args.Floor;
                        
                        float t = (float)EasingFunctions.ApplyFunction(Args.Easing, MathFunctions.Normalize(s, 0, countFrames));

                        Vector2 processedPosition;
                        if (!Args.Invert)
                        {
                            processedPosition = Bezier.QuadraticBezier(Args.FirstFrame.PositionOffset, new(Args.FirstFrame.PositionOffset.X, Args.SecondFrame.PositionOffset.Y), Args.SecondFrame.PositionOffset, t);
                        }
                        else
                        {
                            processedPosition = Bezier.QuadraticBezier(Args.FirstFrame.PositionOffset, new(Args.SecondFrame.PositionOffset.X, Args.FirstFrame.PositionOffset.Y), Args.SecondFrame.PositionOffset, t);
                        }

                        float processedOpacity = t * Args.SecondFrame.Opacity;
                        Vector2 processedParallax = t * Args.SecondFrame.Parallax;
                        float processedRotation = t * Args.SecondFrame.RotationOffset;
                        Vector2 processedScale = t * Args.SecondFrame.Scale;
                        float processedDepth = t * Args.SecondFrame.Depth;

                        keyFrame.PositionOffset = processedPosition;
                        keyFrame.Opacity = processedOpacity;
                        keyFrame.Depth = processedDepth;
                        keyFrame.RotationOffset = processedRotation;
                        keyFrame.Scale = processedScale;

                        keyFrames.Add(keyFrame);
                    }
                }
            }
            return (keyFrames.ToArray(), decorations.ToArray());
        }
    }
}
