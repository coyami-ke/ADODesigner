using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable 1591
namespace ADODesigner.Models
{
    /// <summary>
    /// Base class for holding decoration states. 
    /// </summary>
    public partial class KeyFrame
    {
        public string Tag { get; set; } = "";
        public Vector2 PositionOffset { get; set; } = new(0,0);
        public Vector2 Scale { get; set; } = new(100, 100);
        public float RotationOffset { get; set; } = 0;
        public string Color { get; set; } = "";
        public float Opacity { get; set; } = 100;
        public float Depth { get; set; } = -1;
        public Vector2 Parallax { get; set; } = Vector2.Zero;
        public string EventTag { get; set; } = String.Empty;
        public Ease Ease { get; set; } = Ease.Linear;
        public float AngleOffset { get; set; } = 0;
        public bool IsSelected { get; set; } = false;
        public float Duration { get; set; } = 1;
        public int Floor { get; set; } = 1;
        public Vector2 ParallaxOffset { get; set; } = new(0, 0);

        public bool UsePositionOffset { get; set; } = true;
        public bool UseRotationOffset { get; set; } = true;
        public bool UseScale { get; set; } = true;
        public bool UseColor { get; set; } = true;
        public bool UseParallax { get; set; } = true;
        public bool UseParallaxOffset { get; set; } = true;
        public bool UseOpacity { get; set; } = true;
        public bool UseDepth { get; set; }
        public static void GetDescription(KeyFrame keyFrame)
        {
            Console.WriteLine("Duration: " + keyFrame.Duration);
            Console.WriteLine("Angle Offset: " + keyFrame.AngleOffset);
            Console.WriteLine("Position Offset: " + keyFrame.PositionOffset);
            Console.WriteLine("Rotation offset: " + keyFrame.RotationOffset);
            Console.WriteLine("Scale: " + keyFrame.Scale);
            Console.WriteLine("Tag: " + keyFrame.Tag);
            Console.WriteLine("Parallax: " + keyFrame.Parallax);
            Console.WriteLine("Parallax Offset: " + keyFrame.ParallaxOffset);
            Console.WriteLine("Opacity: " + keyFrame.Opacity);
        }
    }
}
