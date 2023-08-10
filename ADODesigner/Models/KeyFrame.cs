using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
#pragma warning disable 1591
namespace ADODesigner.Models
{
    /// <summary>
    /// Base class for holding decoration states. 
    /// </summary>
    public partial class KeyFrame : ObservableObject
    {
        [ObservableProperty]
        private string tag = "";
        [ObservableProperty]
        private Vector2 positionOffset = new(0,0);
        [ObservableProperty]
        private Vector2 scale = new(100, 100);
        [ObservableProperty]
        private float rotationOffset = 0;
        [ObservableProperty]
        private string color = "";
        [ObservableProperty]
        private float opacity = 100;
        [ObservableProperty]
        private float depth = -1;
        [ObservableProperty]
        private Vector2 parallax = Vector2.Zero;
        [ObservableProperty]
        private RelativeTo relativeTo = RelativeTo.Tile;
        [ObservableProperty]
        private string eventTag = String.Empty;
        [ObservableProperty]
        private Ease ease = Ease.Linear;
        [ObservableProperty]
        private float angleOffset = 0;
        [ObservableProperty]
        private bool isSelected = false;
        [ObservableProperty]
        private float duration = 1;
        [ObservableProperty]
        private int floor = 1;
        [ObservableProperty]
        private string key = String.Empty;
        [ObservableProperty]
        private Vector2 parallaxOffset = new(0, 0);

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
