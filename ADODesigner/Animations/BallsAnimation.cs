using ADODesigner.Models;
using ADODesigner.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ADODesigner.Animations
{
    public partial class BallsAnimation : ObservableObject
    {
        public string Tag { get; set; } = "";
        public int Count { get; set; }

        public bool UsePosition { get; set; } = true;
        public Vector2 PositionOffset { get; set; }
        [UsageWindowProperties(Name = "Addable Position")]
        public Vector2 PositionOffsetPerDeco { get; set; } = new(-1, 0);

        public bool UsePivotOffset { get; set; } = true;
        public Vector2 PivotOffset { get; set; }

        public bool UseRotation { get; set; } = true;
        public float Rotation { get; set; }

        public bool UseScale { get; set; } = true;
        public Vector2 Scale { get; set; } = new(555);
        public Vector2 ScaleDifference { get; set; } = new(0);

        public bool UseOpacity { get; set; } = true;
        public float Opacity { get; set; } = 100;
        public float OpacityDifference { get; set; } = 0;

        public Ease Ease { get; set; } = Ease.Linear;
        [UsageWindowProperties(AddToWindowProperties = false)]
        public float Duration { get; set; } = 1;
        [UsageWindowProperties(Name = "Addable Angle")]
        public float CornerAnglePerDeco { get; set; } = 90;
        public float AngleOffset { get; set; } = 0;
        public string EventTag { get; set; } = "";
        [UsageWindowProperties(AddToWindowProperties = false)]
        public int Floor { get; set; } = 1;
        public KeyFrame[] Animate()
        {
            List<KeyFrame> result = new();
            for (int i = 0; i < Count; i++)
            {
                KeyFrame keyFrame = new()
                {
                    Tag = this.Tag + i,
                    UsePositionOffset = false,
                    UseParallaxOffset = this.UsePosition,
                    UseRotationOffset = this.UseRotation,
                    UsePivotOffset = this.UsePivotOffset,
                    UseOpacity = this.UseOpacity,
                    RotationOffset = this.Rotation,
                    ParallaxOffset = new(this.PositionOffset.X + i * this.PositionOffsetPerDeco.X, this.PositionOffset.Y),
                    PivotOffset = this.PivotOffset,
                    UseScale = true,
                    Duration = this.Duration,
                    Ease = this.Ease,
                    Floor = this.Floor,
                    AngleOffset = this.AngleOffset + i * CornerAnglePerDeco,
                    Scale = this.Scale - this.ScaleDifference * i,
                    Opacity = this.Opacity - this.OpacityDifference * i,
                    EventTag = this.EventTag,
                    UseColor = false,
                };
                result.Add(keyFrame);
            }
            return result.ToArray();
        }

        public object Clone()
        {
            return (BallsAnimation)this.MemberwiseClone();
        }
    }
}
