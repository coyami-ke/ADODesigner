using ADODesigner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable 1591
namespace ADODesigner.Converters
{
    /// <summary>
    /// Basic converter from <see cref="Decoration"/> to <see cref="AddDecoration"/>
    /// </summary>
    public static class DecorationConverter
    {
        /// <summary>
        /// Converts <see cref="Decoration"/> to <see cref="AddDecoration"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static AddDecoration Convert(Decoration value)
        {
            AddDecoration result = new();
            result.Floor = value.Floor;
            result.Color = value.Color;
            result.Tag = value.Tag;
            result.Rotation = value.Rotation;
            result.Opacity = value.Opacity;
            result.DecorationImage = value.Image;
            result.Position = new float[2] { value.Position.X, value.Position.Y };
            result.Scale = new float[2] { value.Scale.X, value.Scale.Y};
            result.Tile = new int[2] { (int)value.Tiling.X, (int)value.Tiling.Y };
            result.ParallaxOffset = new float[2] { value.ParallaxOffset.X, value.ParallaxOffset.Y };
            result.PivotOffset = new float[2] { 0, 0 };
            result.ImageSmoothing = "Enabled";

            return result;
        }
    }
}
