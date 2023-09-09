using ADODesigner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static ADODesigner.Animations.MathFunctions;
using static ADODesigner.Animations.EasingFunctions;

namespace ADODesigner.Animations
{
    public class BallsAnimationArgs
    {
        public int AngleOffset { get; set; } = 90;
        public int Count { get; set; } = 5;
        public Ease Easing { get; set; } = Ease.Linear;
        public string Tag { get; set; } = "cat";
        public float OpacityDifference { get; set; } = 0;
        public Vector2 ScaleDifference { get; set; } = Vector2.Zero;
        public KeyFrame FirstFrame { get; set; } = new();
        public KeyFrame SecondFrame { get; set; } = new();
        public bool IsCurve { get; set; } = true;
        public int FrameRate { get; set; } = 60;
        public int Duration { get; set; } = 4;
        public int Floor { get; set; } = 1;
        public bool Invert { get; set; } = false;
        public int Delay { get; set; } = 0;
        public bool UseParallaxOffset { get; set; } = false;
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
                decoration.Tag += i;
                decoration.Opacity = Args.FirstFrame.Opacity;
                decoration.Scale = Args.FirstFrame.Scale;
                decorations.Add(decoration);
            }
            for (int i = 0; i < decorations.Count; i++)
            {
                for (int j = 0; j < countFrames; j++)
                {
                    int s = j + 1;
                    KeyFrame keyFrame = new();
                    keyFrame.Color = Args.SecondFrame.Color;
                    keyFrame.Parallax = Args.SecondFrame.Parallax;
                    keyFrame.ParallaxOffset = Args.SecondFrame.ParallaxOffset;
                    keyFrame.Depth = Args.SecondFrame.Depth;
                    keyFrame.RotationOffset = Args.SecondFrame.RotationOffset;
                    keyFrame.Scale = Args.SecondFrame.Scale;
                    keyFrame.Opacity = Args.SecondFrame.Opacity;
                    keyFrame.EventTag = Args.SecondFrame.EventTag;
                    keyFrame.Duration = 0;
                    keyFrame.AngleOffset = (180 / Args.FrameRate * (s - 1) + i * Args.AngleOffset) + Args.Delay;
                    keyFrame.Tag = Args.Tag + i;
                    keyFrame.Floor = Args.Floor;
                    keyFrame.UsePositionOffset = Args.SecondFrame.UsePositionOffset;
                    keyFrame.UseRotationOffset = Args.SecondFrame.UseRotationOffset;
                    keyFrame.UseScale = Args.SecondFrame.UseScale;
                    keyFrame.UseParallax = Args.SecondFrame.UseParallax;
                    keyFrame.UseParallaxOffset = Args.SecondFrame.UseParallaxOffset;
                    keyFrame.UseColor = Args.SecondFrame.UseColor;
                    keyFrame.UseDepth = Args.SecondFrame.UseDepth;
                    keyFrame.UseOpacity = Args.SecondFrame.UseOpacity;

                    float t = ApplyFunction(Args.Easing, Normalize(s, 0, countFrames));
                    Vector2 processedPosition = new();
                    if (Args.IsCurve)
                    {
                        if (!Args.Invert)
                        {
                            processedPosition = Bezier.QuadraticBezier(Args.FirstFrame.PositionOffset, new(Args.FirstFrame.PositionOffset.X, Args.SecondFrame.PositionOffset.Y), Args.SecondFrame.PositionOffset, t);
                        }
                        else
                        {
                            processedPosition = Bezier.QuadraticBezier(Args.FirstFrame.PositionOffset, new(Args.SecondFrame.PositionOffset.X, Args.FirstFrame.PositionOffset.Y), Args.SecondFrame.PositionOffset, t);
                        }
                    }
                    else
                    {
                        Vector2 tPosition = Normalize(Args.SecondFrame.PositionOffset / countFrames * s, Args.FirstFrame.PositionOffset, Args.SecondFrame.PositionOffset);
                    }

                    keyFrame.PositionOffset = processedPosition;

                    float tRotation = ApplyFunction(Args.Easing, Normalize(Math.Max(Args.SecondFrame.RotationOffset, Args.FirstFrame.RotationOffset) / countFrames * s, Args.FirstFrame.RotationOffset, Args.SecondFrame.RotationOffset));
                    keyFrame.RotationOffset = Math.Max(Args.SecondFrame.RotationOffset, Args.FirstFrame.RotationOffset) * tRotation;

                    Vector2 tScale = ApplyFunctionVector2(Args.Easing, Normalize(Vector2Extensions.Max(Args.FirstFrame.Scale, Args.SecondFrame.Scale) / countFrames * s, Args.FirstFrame.Scale, Args.SecondFrame.Scale));
                    keyFrame.Scale = Vector2Extensions.Max(Args.SecondFrame.Scale, Args.FirstFrame.Scale) * tScale - Args.ScaleDifference * i;

                    Vector2 tParallax = ApplyFunctionVector2(Args.Easing, Normalize(Vector2Extensions.Max(Args.FirstFrame.Parallax, Args.SecondFrame.Parallax) / countFrames * s, Args.FirstFrame.Parallax, Args.SecondFrame.Parallax)); 
                    keyFrame.Parallax = Vector2Extensions.Max(Args.SecondFrame.Parallax, Args.FirstFrame.Parallax) * tParallax;

                    float tOpacity = ApplyFunction(Args.Easing, Normalize(Math.Max(Args.SecondFrame.Opacity, Args.FirstFrame.Opacity) / countFrames * s, Args.FirstFrame.Opacity, Args.SecondFrame.Opacity));
                    keyFrame.Opacity = Math.Max(Args.SecondFrame.Opacity, Args.FirstFrame.Opacity) * tOpacity - Args.OpacityDifference * i;
                    keyFrames.Add(keyFrame);
                }
            }  
            
            return (keyFrames.ToArray(), decorations.ToArray());
        }
    }
}
