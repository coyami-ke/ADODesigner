using ADODesigner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADODesigner.Converters
{
    public static class KeyFrameToMoveDecoration
    {
        public static string Convert(KeyFrame keyFrame)
        {
            return @"       { ""floor"": " + keyFrame.Floor + @", ""eventType"": ""MoveDecorations"", ""duration"": " + keyFrame.Duration + @", ""tag"": " + "\"" + keyFrame.Tag + "\"" + @", ""positionOffset"": [" + keyFrame.PositionOffset.X.ToString().Replace(',', '.') + ", " +  keyFrame.PositionOffset.Y.ToString().Replace(',', '.') + @"], ""scale"": [" + keyFrame.Scale.X + ", " + keyFrame.Scale.Y + @"], ""opacity"": " + keyFrame.Opacity + @", ""depth"": " + keyFrame.Depth + @", ""angleOffset"": " + keyFrame.AngleOffset + @", ""ease"": ""Linear"", ""eventTag"": """", },";
        }
    }
}
