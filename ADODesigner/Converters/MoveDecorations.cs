using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable
#pragma warning disable 1591
namespace ADODesigner.Converters
{
    /// <summary>
    /// ADOFAI Event "Move Decorations"
    /// </summary>
    public class MoveDecorations
    {
        public int floor { get; set; } = 0;
        public string eventType { get; set; } = "MoveDecorations";
        public float duration { get; set; }
        public string tag { get; set; }
        public float[] positionOffset { get; set; } = new float[2];
        public float[] scale { get; set; } = new float[2];
        public float opacity { get; set; }
        public float depth { get; set; }
        public float angleOffset { get; set; }
        public string ease { get; set; }
        public string eventTag { get; set; }
        public float[] parallax { get; set; } = new float[2];
        public float[] parallaxOffset { get; set; } = new float[2];
        public float rotationOffset { get; set; } = 0;
    }
}
