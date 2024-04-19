using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ADODesigner.Models
{
    public partial class KeyFrameTiles : ObservableObject
    {
        [ObservableProperty]
        private int startTile;
        [ObservableProperty]
        private int endTile;
        [ObservableProperty]
        private int gapLength;
        [ObservableProperty]
        private float duration;

        [ObservableProperty]
        private Vector2 positionOffset;
        [ObservableProperty]
        private float rotationOffset;
        [ObservableProperty]
        private Vector2 scale;
        [ObservableProperty]
        private float opacity;
        [ObservableProperty]
        private float angleOffset;
        [ObservableProperty]
        private Ease ease;
        [ObservableProperty]
        private string eventTag = "";
    }
}
