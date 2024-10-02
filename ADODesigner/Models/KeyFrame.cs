using ADODesigner.Animations;
using ADODesigner.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class KeyFrame : ObservableObject, ICloneable
    {
        [ObservableProperty]
        [property: UsageWindowProperties(AddToWindowProperties = false, LocalizationProperty = "Tag")]
        private float duration = 1;
        [ObservableProperty]
        [property: UsageWindowProperties(LocalizationProperty = "Tag")]
        private string tag = "";

        [property: UsageWindowProperties(Name = "")]
        [ObservableProperty]
        private bool useImage = false;
        [ObservableProperty]
        [property: UsageWindowProperties(LocalizationProperty = "Image", IsImage = true)]
        private string image = "";

        [ObservableProperty]
        [property: UsageWindowProperties(Name = "")]
        private bool usePositionOffset = true;
        [ObservableProperty]
        [property: UsageWindowProperties(LocalizationProperty = "Position")]
        private Vector2 positionOffset = new(0,0);

        [ObservableProperty]
        [property: UsageWindowProperties(Name = "")]
        public bool usePivotOffset = false;
        [ObservableProperty]
        [property: UsageWindowProperties(LocalizationProperty = "PivotOffset")]
        private Vector2 pivotOffset = new(0,0);

        [ObservableProperty]
        [property: UsageWindowProperties(Name = "")]
        public bool useRotationOffset = false;
        [ObservableProperty]
        [property: UsageWindowProperties(LocalizationProperty = "Rotation")]
        private float rotationOffset = 0;

        [ObservableProperty]
        [property: UsageWindowProperties(Name = "")]
        public bool useScale = false;
        [ObservableProperty]
        [property: UsageWindowProperties(LocalizationProperty = "Scale")]
        private Vector2 scale = new(100, 100);

        [ObservableProperty]
        [property: UsageWindowProperties(Name = "")]
        public bool useOpacity = false;
        [ObservableProperty]
        [property: UsageWindowProperties(LocalizationProperty = "Opacity")]
        private float opacity = 100;

        [ObservableProperty]
        [property: UsageWindowProperties(Name = "")]
        private bool useColor = false;
        [ObservableProperty]
        [property: UsageWindowProperties(AddToWindowProperties = true, Name = "Color", IsColor = true, LocalizationProperty = "Color")]
        private string color = "";

        [ObservableProperty]
        [property: UsageWindowProperties(Name = "")]
        public bool useDepth = false;
        [ObservableProperty]
        [property: UsageWindowProperties(LocalizationProperty = "Depth")]
        private int depth = -1;

        [ObservableProperty]
        [property: UsageWindowProperties(Name = "")]
        private bool useParallax = false;
        [ObservableProperty]
        [property: UsageWindowProperties(LocalizationProperty = "Parallax")]
        private Vector2 parallax = Vector2.Zero;

        [ObservableProperty]
        [property: UsageWindowProperties(Name = "")]
        private bool useParallaxOffset = false;
        [ObservableProperty]
        [property: UsageWindowProperties(LocalizationProperty = "ParallaxOffset")]
        private Vector2 parallaxOffset = new(0, 0);

        [ObservableProperty]
        [property: UsageWindowProperties(LocalizationProperty = "Ease")]
        private Ease ease = Ease.Linear;
        [ObservableProperty]
        [property: UsageWindowProperties(AddToWindowProperties = false)]
        private int floor = 1;
        [ObservableProperty]
        [property: UsageWindowProperties(LocalizationProperty = "EventTag")]
        private string eventTag = String.Empty;
        [ObservableProperty]

        [property: UsageWindowProperties(LocalizationProperty = "AngleOffset")]
        private float angleOffset = 0;
        
        public object Clone()
        {
            return (KeyFrame)this.MemberwiseClone();
        }
    }
}
