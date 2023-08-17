using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable 1591
namespace ADODesigner.Models
{
    /// <summary>
    /// Decoration model
    /// </summary>
    public class Decoration
    {
        public string Image { get; set; } = String.Empty;
        public Vector2 Position { get; set; } = new Vector2(0, 0);
        public Vector2 Scale { get; set; } = new Vector2(100,100);
        public float Rotation { get; set; } = 0;
        public Vector2 PivotOffset { get; set; } = new Vector2(0, 0);
        public string Color { get; set; } = "FFFFFF";
        public float Opacity { get; set; } = 100;
        public float Depth { get; set; } = -1;
        public Vector2 Parallax { get; set; } = new Vector2(0, 0);
        public string Tag { get; set; } = String.Empty;
        public bool Locked { get; set; } = false;
        public int Floor { get; set; } = 0;
        public Vector2 Tiling { get; set; } = Vector2.Zero;
        public bool IsSelected { get; set; } = false;
        public string ID { get; set; } = "";
        public bool LockRotation { get; set; } = false;
        public bool LockScale { get; set; } = false;
        public Vector2 ParallaxOffset { get; set; } = new(0, 0);
        public bool ImageSmoothing { get; set; } = true;
    }
}
