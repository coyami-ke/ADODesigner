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
        private string tag = String.Empty;
        [ObservableProperty]
        private Vector2 positionOffset = new(0, 0);
        [ObservableProperty]
        private Vector2 scale = new(100, 100);
        [ObservableProperty]
        private float rotationOffset = 0;
        [ObservableProperty]
        private string color = "FFFFFFFF";
        [ObservableProperty]
        private float opacity = 100;
        [ObservableProperty]
        private float depth = -1;
        [ObservableProperty]
        private Vector2 parallax = new(0,0);
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
        private int floor = 0;
        [ObservableProperty]
        private string key = String.Empty;
    }
}
