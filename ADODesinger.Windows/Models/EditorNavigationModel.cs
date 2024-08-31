using ADODesigner.Converters;
using ADODesinger.Windows.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ADODesigner.Windows.Models
{
    public partial class EditorNavigationModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<TimeLineTileNavigation> tiles = new();
        [ObservableProperty]
        private double maxHeightCanvas = 10000;
        [ObservableProperty]
        private double maxWidthCanvas = 100000;
        public EditorNavigationModel(CustomLevel level)
        {
            float sizeTile = 75;

            Vector2 lastPosition = new();
            foreach (var t in level.AngleData)
            {
                float degrees = (float)NCalcHelper.DegreesToRadians(t);
                float xPosition = MathF.Cos(degrees) * sizeTile;
                float yPosition = MathF.Sin(degrees) * sizeTile;

                Tiles.Add(new TimeLineTileNavigation() { SecondPositionX = xPosition, SecondPositionY = yPosition, FirstPositionX = lastPosition.X, FirstPositionY = lastPosition.Y, Opacity = 1 });

                lastPosition = new(xPosition * sizeTile, yPosition * sizeTile);
            }
        }
    }
}
