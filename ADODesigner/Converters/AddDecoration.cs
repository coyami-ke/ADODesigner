using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable 1591
namespace ADODesigner.Converters
{
    /// <summary>
    /// ADOFAI Event "Add decoration"
    /// </summary>
    public class AddDecoration
    {
        public int floor { get; set; } = 0;
        public string eventType { get; set; } = "AddDecoration";
        public bool locked { get; set; } = false;
        public string decorationImage { get; set; } = "";
        public float[] position { get; set; } = new float[2];
        public string relativeTo { get; set; } = "Tile";
        public float[] pivotOffset { get; set; } = new float[2];
        public float rotation { get; set; } = 0;
        public string lockRotation { get; set; } = "Disabled";
        public float[] scale { get; set; } = new float[2];
        public string lockScale { get; set; } = "Disabled";
        public int[] tile { get; set; } = new int[2];
        public string color { get; set; } = "ffffff";
        public float opacity { get; set; } = 100;
        public float depth { get; set; } = -1;
        public float[] parallax { get; set; } = new float[2];
        public float[] parallaxOffset { get; set; } = new float[2];
        public string tag { get; set; } = "";
        public string imageSmoothing { get; set; } = "Enabled";
    }
}
