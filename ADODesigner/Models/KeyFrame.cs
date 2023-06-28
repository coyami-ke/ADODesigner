using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace ADODesigner.Models
{
    /// <summary>
    /// Base class for holding decoration states. 
    /// </summary>
    public partial class KeyFrame : ObservableObject
    {
        [ObservableProperty]
        private string tag = String.Empty;
        [ObservableProperty]
        private Vector2 positionOffset;
        [ObservableProperty]
        private Vector2 scale;
        [ObservableProperty]
        private float rotationOffset;
        [ObservableProperty]
        private string color = "FFFFFFFF";
        [ObservableProperty]
        private float opacity;
        [ObservableProperty]
        private float depth;
        [ObservableProperty]
        private Vector2 parallax;
        [ObservableProperty]
        private RelativeTo relativeTo;
        [ObservableProperty]
        private string eventTag = String.Empty;
        [ObservableProperty]
        private Ease ease = Ease.Leanear;
        [ObservableProperty]
        private float angleOffset;
        [ObservableProperty]
        private bool isSelected;
        [ObservableProperty]
        private float duration;
        [ObservableProperty]
        private int floor;
        [ObservableProperty]
        private string key = String.Empty;
    }
}
