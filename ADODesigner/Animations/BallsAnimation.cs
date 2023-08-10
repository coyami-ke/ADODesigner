﻿using ADODesigner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using static ADODesigner.Animations.MathFunctions;
using static ADODesigner.Animations.EasingFunctions;

namespace ADODesigner.Animations
{
    public class BallsAnimationArgs
    {
        public int AngleOffset { get; set; } = 45;
        public int Count { get; set; } = 5;
        public Ease Easing { get; set; } = Ease.Linear;
        public string Tag { get; set; } = "Ball";
        public KeyFrame FirstFrame { get; set; } = new();
        public KeyFrame SecondFrame { get; set; } = new();
        public bool IsCurve { get; set; } = true;
        public int FrameRate { get; set; } = 60;
        public int Duration { get; set; } = 5;
        public int Floor { get; set; } = 1;
        public bool Invert { get; set; } = false;
        public bool VerticalInvert { get; set; } = false;
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
                for (int s = 0; s < countFrames; s++)
                {
;                   KeyFrame keyFrame = new();
                    keyFrame.Color = Args.SecondFrame.Color;
                    keyFrame.Parallax = Args.SecondFrame.Parallax;
                    keyFrame.ParallaxOffset = Args.SecondFrame.ParallaxOffset;
                    keyFrame.Depth = Args.SecondFrame.Depth;
                    keyFrame.RotationOffset = Args.SecondFrame.RotationOffset;
                    keyFrame.Scale = Args.SecondFrame.Scale;
                    keyFrame.Opacity = Args.SecondFrame.Opacity;
                    keyFrame.EventTag = Args.SecondFrame.EventTag;
                    keyFrame.Duration = 0;
                    keyFrame.AngleOffset = (180 / Args.FrameRate * (s + 1)) + i * Args.AngleOffset;
                    keyFrame.Tag = Args.Tag + i;
                    keyFrame.Key = "BallsAnimation" + decorations[i].Tag + s.ToString();
                    keyFrame.Floor = Args.Floor;

                    Vector2 processedPosition;
                    if (Args.IsCurve)
                    {
                        float t = ApplyFunction(Args.Easing, MathFunctions.Normalize(s, 0, countFrames));
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
                        processedPosition = tPosition * Args.SecondFrame.PositionOffset;
                    }
                    
                    keyFrame.PositionOffset = processedPosition;

                    float tRotation = ApplyFunction(Args.Easing, Normalize(Args.SecondFrame.RotationOffset / countFrames * s, Args.FirstFrame.RotationOffset, Args.SecondFrame.RotationOffset);
                    keyFrame.RotationOffset = Args.SecondFrame.RotationOffset * tRotation;

                    Vector2 tScale = ApplyFunctionVector2(Args.Easing, Normalize(Args.SecondFrame.Scale / countFrames * s, Args.FirstFrame.Scale, Args.SecondFrame.Scale));
                    keyFrame.Scale = Args.SecondFrame.Scale * tScale;

                    Vector2 tParallax = ApplyFunctionVector2(Args.Easing, Normalize(Args.SecondFrame.Parallax / countFrames * s, Args.FirstFrame.Parallax, Args.SecondFrame.Parallax));
                    keyFrame.Parallax = Args.SecondFrame.Parallax * tParallax;

                    Vector2 tParallaxOffset = ApplyFunctionVector2(Args.Easing, Normalize(Args.SecondFrame.ParallaxOffset / countFrames * s, Args.FirstFrame.ParallaxOffset, Args.SecondFrame.ParallaxOffset);
                    keyFrame.ParallaxOffset = Args.SecondFrame.ParallaxOffset * tParallaxOffset;

                    float tOpacity = Normalize(Args.SecondFrame.Opacity / countFrames * s, Args.FirstFrame.Opacity, Args.SecondFrame.Opacity);
                    keyFrame.Opacity = Args.SecondFrame.Opacity * tOpacity;

                    keyFrames.Add(keyFrame);
                }        
            }        
            
            return (keyFrames.ToArray(), decorations.ToArray());
        }
    }
}
