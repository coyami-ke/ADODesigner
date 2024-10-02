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
        [UsageWindowProperties(LocalizationProperty = "Tag")]
        public string Tag { get; set; } = "";
        [UsageWindowProperties(LocalizationProperty = "Count")]
        public int Count { get; set; }
        [UsageWindowProperties(Name = "")]
        public bool UsePosition { get; set; } = true;
        [UsageWindowProperties(LocalizationProperty = "Position")]
        public Vector2 PositionOffset { get; set; }
        [UsageWindowProperties(LocalizationProperty = "AddablePosition")]
        public Vector2 PositionOffsetPerDeco { get; set; } = new(-1, 0);
        [UsageWindowProperties(Name = "")]
        public bool UsePivotOffset { get; set; } = true;
        [UsageWindowProperties(LocalizationProperty = "PivotOffset")]
        public Vector2 PivotOffset { get; set; }
        [UsageWindowProperties(Name = "")]
        public bool UseRotation { get; set; } = true;
        [UsageWindowProperties(LocalizationProperty = "Rotation")]
        public float Rotation { get; set; }
        [UsageWindowProperties(Name = "")]
        public bool UseScale { get; set; } = true;
        [UsageWindowProperties(LocalizationProperty = "Scale")]
        public Vector2 Scale { get; set; } = new(555);
        [UsageWindowProperties(LocalizationProperty = "ScaleDifference")]
        public Vector2 ScaleDifference { get; set; } = new(0);
        [UsageWindowProperties(Name = "")]
        public bool UseOpacity { get; set; } = true;
        [UsageWindowProperties(LocalizationProperty = "Opacity")]
        public float Opacity { get; set; } = 100;
        [UsageWindowProperties(LocalizationProperty = "OpacityDifference")]
        public float OpacityDifference { get; set; } = 0;
        [UsageWindowProperties(LocalizationProperty = "Ease")]
        public Ease Ease { get; set; } = Ease.Linear;
        [UsageWindowProperties(AddToWindowProperties = false)]
        public float Duration { get; set; } = 1;
        [UsageWindowProperties(Name = "AddableAngle")]
        public float CornerAnglePerDeco { get; set; } = 90;
        [UsageWindowProperties(LocalizationProperty = "AngleOffset")]
        public float AngleOffset { get; set; } = 0;
        [UsageWindowProperties(LocalizationProperty = "EventTag")]
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
