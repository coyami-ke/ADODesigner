using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable 1591
namespace ADODesigner.Models
{
    /// <summary>
    /// Decoration model
    /// </summary>
    public partial class Decoration : ObservableObject
    {
        [ObservableProperty]
        private string image = String.Empty;
        [ObservableProperty]
        private Vector2 position = new Vector2(0,0);
        [ObservableProperty]
        private Vector2 scale = new Vector2(100,100);
        [ObservableProperty]
        private float rotation = 0;
        [ObservableProperty]
        private Vector2 pivotOffset = new Vector2(0, 0);
        [ObservableProperty]
        private string color = "FFFFFFFF";
        [ObservableProperty]
        private float opacity = 100;
        [ObservableProperty]
        private float depth = -1;
        [ObservableProperty]
        private Vector2 parallax = new Vector2(0, 0);
        [ObservableProperty]
        private string tag = String.Empty;
        [ObservableProperty]
        private bool locked = false;
        [ObservableProperty]
        private int floor = 0;
        [ObservableProperty]
        private Vector2 tiling = Vector2.Zero;
        [ObservableProperty]
        private bool isSelected = false;
        [ObservableProperty]
        private string iD;
    }
}
