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
            if (value.UseOpacity)
            {
                result.Opacity = value.Opacity;
            }
            if (value.UsePositionOffset)
            {
                result.PositionOffset = new float[2];
                result.PositionOffset[0] = value.PositionOffset.X;
                result.PositionOffset[1] = value.PositionOffset.Y;
            }
            if (value.UsePivotOffset)
            {
                result.PivotOffset = new float[2];
                result.PivotOffset[0] = value.PivotOffset.X;
                result.PivotOffset[1] = value.PivotOffset.Y;
            }
            if (value.UseRotationOffset) result.RotationOffset = value.RotationOffset;
            if (value.UseScale)
            {
                result.Scale = new float[2];
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
                result.Parallax = new float[2];
                result.Parallax[0] = value.Parallax.X;
                result.Parallax[1] = value.Parallax.Y;
            }
            if (value.UseParallaxOffset)
            {
                result.ParallaxOffset = new float[2];
                result.ParallaxOffset[0] = value.ParallaxOffset.X;
                result.ParallaxOffset[1] = value.ParallaxOffset.Y;
            }
            if (value.UseColor)
            {
                result.Color = value.Color;
            }
            if (value.UseImage)
            {
                result.DecorationImage = value.Image;
            }
            return result;
        }
        public static KeyFrame Convert(MoveDecorations value)
        {
            KeyFrame result = new();
            result.UsePositionOffset = false;
            if (value.Opacity is not null)
            {
                result.Opacity = (float)value.Opacity;
                result.UseOpacity = true;
            } 
            if (value.PositionOffset is not null)
            {
                result.PositionOffset = new(value.PositionOffset[0], value.PositionOffset[1]);
                result.UsePositionOffset = true;
            }
            if (value.RotationOffset is not null)
            {
                result.Opacity = (float)value.RotationOffset;
                result.UseOpacity = true;
            }
            if (value.Scale is not null)
            {
                result.Scale = new(value.Scale[0], value.Scale[1]);
                result.UseScale = true;
            }
            if (value.Parallax is not null)
            {
                result.Parallax = new(value.Parallax[0], value.Parallax[1]);
                result.UseParallax = true;
            }
            if (value.ParallaxOffset is not null)
            {
                result.ParallaxOffset = new(value.ParallaxOffset[0], value.ParallaxOffset[1]);
                result.UseParallaxOffset = true;
            }
            if (value.Depth is not null)
            {
                result.Depth = (int)value.Depth;
                result.UseDepth = true;
            }
            if (value.PivotOffset is not null)
            {
                result.PivotOffset = new(value.PivotOffset[0], value.PivotOffset[1]);
                result.UsePivotOffset = true;
            }
            if (value.Color is not null)
            {
                result.Color = value.Color;
                result.UseColor = true;
            }
            if (value.DecorationImage is not null)
            {
                result.Image = value.DecorationImage;
                result.UseImage = true;
            }
            result.Tag = value.Tag;
            result.EventTag = value.EventTag;
            result.Floor = value.Floor;
            result.Ease = Enum.Parse<Ease>(value.Ease);
            result.Duration = value.Duration;

            return result;
        }
    }
}
