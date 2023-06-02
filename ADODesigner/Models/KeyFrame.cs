using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
#nullable disable
namespace ADODesigner.Models
{
    public partial class KeyFrame : ObservableObject
    {
        [ObservableProperty]
        private string tag;
        [ObservableProperty]
        private Vector2 positionOffset;
        [ObservableProperty]
        private Vector2 scale;
        [ObservableProperty]
        private float rotationOffset;
        [ObservableProperty]
        private string color;
        [ObservableProperty]
        private float opacity;
        [ObservableProperty]
        private float depth;
        [ObservableProperty]
        private Vector2 parallax;
        [ObservableProperty]
        private RelativeTo relativeTo;
        [ObservableProperty]
        private string eventTag;
        [ObservableProperty]
        private Ease ease;
        [ObservableProperty]
        private float angleOffset;
        [ObservableProperty]
        private bool isSelected;
    }
}
