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
        public int Intensivity { get; set; } = 1;
        public int Count { get; set; } = 5;
        public Ease Easing { get; set; } = Ease.Linear;
        public KeyFrame FirstFrame { get; set; } = new();
        public KeyFrame SecondFrame { get; set; } = new();
        public bool IsCurve { get; set; } = true;
        public int FrameRate { get; set; } = 30;
        public int Duration { get; set; } = 5;
        public int Floor { get; set; } = 1;
    }
    /// <summary>
    /// Class for creating procedural animation of ADOFAI balls.
    /// </summary>
    public class BallsAnimation : BaseAnimation<BallsAnimationArgs>
    {
        public BallsAnimation(BallsAnimationArgs args) : base(args)
        {
            
        }
        public override void CreateAnimation()
        {
            List<Decoration> decorations = new();

            int countFrames = Args.Duration * Args.FrameRate;

            for (int i = 0; i < Args.Count; i++)
            {
                Decoration decoration = new();
                decoration.Position = Args.FirstFrame.PositionOffset;
                decoration.Tag = "Ball" + i;
                decoration.Opacity -= i * 8 * Args.Intensivity;
                decoration.Scale -= new Vector2(i * 10, i * 10);
                decorations.Add(decoration);
                EditorView.Editor.AddDecoration(decoration);
            }

            if (Args.IsCurve)
            {
                for (int i = 0; i < decorations.Count; i++)
                {
                    for (int s = 0; s < countFrames; s++)
                    {
                        KeyFrame keyFrame = new();
                        keyFrame.Duration = Args.FrameRate / countFrames;
                        keyFrame.AngleOffset = 180 / Args.FrameRate * (s + 1);
                        keyFrame.Tag = "Ball" + i;
                        keyFrame.Key = "BallsAnimation" + decorations[i].Tag + s.ToString();
                        keyFrame.Floor = Args.Floor;
                        
                        //Vector2 position = new(0,0);
                        //float positionX = (Args.FirstPosition.X - Args.SecondPosition.X) / countFrames * s;
                        //float normalizedPositionX = (positionX - Args.FirstPosition.X) / (Args.SecondPosition.X / Args.FirstPosition.X);
                        //float newPositionX = (float)EasingFunctions.InOutSine(normalizedPositionX * positionX);
                        //position.X = newPositionX;

                        //float positionY = (Args.FirstPosition.Y - Args.SecondPosition.Y) / countFrames * s;
                        //float normalizedPositionY = (positionX - Args.FirstPosition.Y) / (Args.SecondPosition.X / Args.FirstPosition.Y);
                        //float newPositionY = (float)EasingFunctions.InOutSine(normalizedPositionY * positionY);
                        //position.Y = newPositionY;

                        Vector2 position = (Args.FirstFrame.PositionOffset - Args.SecondFrame.PositionOffset) / countFrames * s;
                        Vector2 normalizedPosition = MathFunctions.Normalize(position, Args.FirstFrame.PositionOffset, Args.SecondFrame.PositionOffset);
                        float newPositionX = (float)EasingFunctions.InOutCubic(normalizedPosition.X * position.X);
                        float newPositionY = (float)EasingFunctions.InOutCubic(normalizedPosition.Y * position.Y);

                        keyFrame.PositionOffset = new Vector2(newPositionX, newPositionY);

                        keyFrame.Opacity = Args.SecondFrame.Opacity;
                        keyFrame.Depth = Args.SecondFrame.Depth;
                        keyFrame.RotationOffset = Args.SecondFrame.RotationOffset;
                        keyFrame.Scale = Args.SecondFrame.Scale;

                        EditorView.Editor.AddKeyFrame(keyFrame);
                    }
                }
            }
        }
    }
}
