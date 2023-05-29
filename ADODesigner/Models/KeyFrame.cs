using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
#nullable disable
namespace ADODesigner.Models
{
    public partial class KeyFrame : ObservableObject
    {
        [ObservableProperty]
        private string tag;
        [ObservableProperty]
        private Vector2 position;
        [ObservableProperty]
        private Vector2 scale;
        [ObservableProperty]
        private float rotation;
        [ObservableProperty]
        private Vector2 pivotOffset;
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
    }
}
