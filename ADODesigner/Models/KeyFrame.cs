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
        public KeyFrame()
        {
            Tag = "";
            PositionOffset = new(0, 0);
            Scale = new(100, 100);
            RotationOffset = 0;
            Color = "FFFFFFFF";
            Opacity = 100;
            Depth = -1;
            Parallax = new(0, 0);
            Duration = 1;
            ParallaxOffset = new(100, 100);
            EventTag = "";
            Floor = 1;
            AngleOffset = 0;
            IsSelected = true;
            Ease = Ease.Linear;
        }
        public string Tag { get; set; }
        public Vector2 PositionOffset { get; set; } 
        public Vector2 Scale { get; set; } 
        public float RotationOffset { get; set; }
        public string Color { get; set; }
        public float Opacity { get; set; } 
        public float Depth { get; set; }
        public Vector2 Parallax { get; set; }
        public string EventTag { get; set; } 
        public Ease Ease { get; set; } 
        public float AngleOffset { get; set; } 
        public bool IsSelected { get; set; } 
        public float Duration { get; set; } 
        public int Floor { get; set; } 
        public Vector2 ParallaxOffset { get; set; }

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
