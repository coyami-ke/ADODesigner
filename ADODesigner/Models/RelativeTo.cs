using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable 1591
namespace ADODesigner.Models
{
    /// <summary>
    /// RelativeTo for decorations
    /// </summary>
    public enum RelativeTo : byte
    {
        Tile,
        Global,
        FirePlanet,
        IcePlanet,
        Camera,
        CameraAspect,
    }
}
