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
        [property: UsageWindowProperties(AddToWindowProperties = false)]
        private float duration = 1;
        [ObservableProperty]
        private string tag = "";

        [ObservableProperty]
        private string image = "";
        [ObservableProperty]
        private bool useImage = false;

        [ObservableProperty]
        private bool usePositionOffset = true;
        [ObservableProperty]
        private Vector2 positionOffset = new(0,0);

        [ObservableProperty]
        public bool usePivotOffset = false;
        [ObservableProperty]
        private Vector2 pivotOffset = new(0,0);

        [ObservableProperty]
        public bool useRotationOffset = false;
        [ObservableProperty]
        private float rotationOffset = 0;

        [ObservableProperty]
        public bool useScale = false;
        [ObservableProperty]
        private Vector2 scale = new(100, 100);

        [ObservableProperty]
        public bool useOpacity = false;
        [ObservableProperty]
        private float opacity = 100;

        [ObservableProperty]
        private bool useColor = false;
        [ObservableProperty]
        [property: UsageWindowProperties(AddToWindowProperties = true, Name = "Color", IsColor = true)]
        private string color = "";

        [ObservableProperty]
        public bool useDepth = false;
        [ObservableProperty]
        private int depth = -1;

        [ObservableProperty]
        private bool useParallax = false;
        [ObservableProperty]
        private Vector2 parallax = Vector2.Zero;



        [ObservableProperty]
        private bool useParallaxOffset = false;
        [ObservableProperty]
        private Vector2 parallaxOffset = new(0, 0);

        [ObservableProperty]
        private Ease ease = Ease.Linear;
        [ObservableProperty]
        [property: UsageWindowProperties(AddToWindowProperties = false)]
        private int floor = 1;
        [ObservableProperty]
        private string eventTag = String.Empty;
        [ObservableProperty]
        private float angleOffset = 0;
        
        public object Clone()
        {
            return (KeyFrame)this.MemberwiseClone();
        }
    }
}
