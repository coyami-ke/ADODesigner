using ADODesigner.Models;
using ADODesigner.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
        public Ease Easing { get; set; } = Ease.Leanear;
        public Vector2 FirstPosition { get; set; } = Vector2.Zero;
        public Vector2 SecondPosition { get; set; } = Vector2.Zero;
        public bool IsSinusoid { get; set; } = true;
        public int FrameRate { get; set; } = 30;
        public int Duration { get; set; } = 10;
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
                decoration.Position = Args.FirstPosition;
                decoration.Tag = "Ball" + i;
                decoration.Opacity -= i * 8 * Args.Intensivity;
                decorations.Add(decoration);
                EditorView.Editor.AddDecoration(decoration);
            }

            if (Args.IsSinusoid)
            {
                for (int i = 0; i < decorations.Count; i++)
                {
                    for (int s = 0; s < countFrames; s++)
                    {
                        KeyFrame keyFrame = new();
                        keyFrame.Duration = Args.Duration / countFrames;
                        keyFrame.Tag = "Ball" + i;
                        keyFrame.AngleOffset = keyFrame.Duration / 180;
                        keyFrame.Key = "BallsAnimation" + decorations[i].Tag + s.ToString();


                        Vector2 position = new(0,0);

                        float positionX = (Args.FirstPosition.X - Args.SecondPosition.X) / countFrames * s;
                        float normalizedPositionX = (positionX - Args.FirstPosition.X) / (Args.SecondPosition.X / Args.FirstPosition.X);
                        float newPositionX = (float)EasingFunctions.InOutSine(normalizedPositionX * positionX);
                        position.X = newPositionX;

                        float positionY = (Args.FirstPosition.Y - Args.SecondPosition.Y) / countFrames * s;
                        float normalizedPositionY = (positionX - Args.FirstPosition.Y) / (Args.SecondPosition.X / Args.FirstPosition.Y);
                        float newPositionY = (float)EasingFunctions.InOutSine(normalizedPositionY * positionY);
                        position.Y = newPositionY;

                        keyFrame.PositionOffset = new Vector2(positionX, positionY);

                        EditorView.Editor.AddKeyFrame(keyFrame);
                    }
                }
            }
        }
    }
}
