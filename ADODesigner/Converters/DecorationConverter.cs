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
        /// Convert <see cref="Decoration"/> to <see cref="AddDecoration"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static AddDecoration Convert(Decoration value)
        {
            AddDecoration result = new();
            result.floor = value.Floor;
            result.color = value.Color;
            result.tag = value.Tag;
            result.rotation = value.Rotation;
            result.opacity = value.Opacity;
            result.decorationImage = value.Image;
            result.position = new float[2] { value.Position.X, value.Position.Y };
            result.scale = new float[2] { value.Scale.X, value.Scale.Y};
            result.tile = new int[2] { (int)value.Tiling.X, (int)value.Tiling.Y };

            return result;
        }
    }
}
