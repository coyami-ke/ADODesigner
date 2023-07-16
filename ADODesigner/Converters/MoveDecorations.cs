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
        public int floor;
        public string eventType;
        public float duration;
        public string tag;
        public float[] positionOffset = new float[2];
        public float[] scale = new float[2];
        public float opacity;
        public float depth;
        public float angleOffset;
        public string ease;
        public string eventTag;
    }
}
