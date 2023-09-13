using ADODesigner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable 1591
namespace ADODesigner.Converters
{
    /// <summary>
    /// Basic converter from <see cref="KeyFrame"/>
    /// </summary>
    public static class KeyFrameConverter
    {
        public static MoveDecorations Convert(KeyFrame value)
        {
            MoveDecorations result = new();
            result.Floor = value.Floor;
            if (value.UsePositionOffset)
            {
                result.PositionOffset = new float?[2];
                result.PositionOffset[0] = value.PositionOffset.X;
                result.PositionOffset[1] = value.PositionOffset.Y;
            }
            if (value.UseRotationOffset) result.RotationOffset = value.RotationOffset;
            if (value.UseScale)
            {
                result.Scale = new float?[2];
                result.Scale[0] = value.Scale.X;
                result.Scale[1] = value.Scale.Y;
            }
            result.AngleOffset = value.AngleOffset;
            if (value.UseOpacity)
            {
                result.Opacity = value.Opacity;
            }
            result.Duration = value.Duration;
            if (value.UseDepth) result.Depth = value.Depth;
            result.Ease = value.Ease.ToString();
            result.Tag = value.Tag;
            result.EventTag = value.EventTag;
            if (value.UseParallax)
            {
                result.Parallax = new float?[2];
                result.Parallax[0] = value.Parallax.X;
                result.Parallax[1] = value.Parallax.Y;
            }
            if (value.UseParallaxOffset)
            {
                result.ParallaxOffset = new float?[2];
                result.ParallaxOffset[0] = value.ParallaxOffset.X;
                result.ParallaxOffset[1] = value.ParallaxOffset.Y;
            }
            return result;
        }
        public static KeyFrame Convert(MoveDecorations value)
        {
            KeyFrame result = new();
            result.Floor = value.Floor;
            if (value.PositionOffset is null)
            {

            }
            return result;
        }
    }
}
