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
            result.PositionOffset[0] = value.PositionOffset.X;
            result.PositionOffset[1] = value.PositionOffset.Y;
            result.RotationOffset = value.RotationOffset;
            result.Scale[0] = value.Scale.X;
            result.Scale[1] = value.Scale.Y;
            result.AngleOffset = value.AngleOffset;
            result.Opacity = value.Opacity;
            result.Duration = value.Duration;
            result.Depth = value.Depth;
            result.Ease = value.Ease.ToString();
            result.Tag = value.Tag;
            result.EventTag = value.EventTag;
            result.Parallax[0] = value.Parallax.X;
            result.Parallax[1] = value.Parallax.Y;
            result.ParallaxOffset[0] = value.ParallaxOffset.X;
            result.ParallaxOffset[1] = value.ParallaxOffset.Y;
            return result;
        }
    }
}
