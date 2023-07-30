using ADODesigner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
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
            result.floor = value.Floor;
            result.positionOffset[0] = value.PositionOffset.X;
            result.positionOffset[1] = value.PositionOffset.Y;
            result.scale[0] = value.Scale.X;
            result.scale[1] = value.Scale.Y;
            result.angleOffset = value.AngleOffset;
            result.opacity = value.Opacity;
            result.duration = value.Duration;
            result.depth = value.Depth;
            result.ease = value.Ease.ToString();
            result.tag = value.Tag;
            result.eventTag = value.EventTag;

            return result;
        }
    }
}
