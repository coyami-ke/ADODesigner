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
        public int floor = 0;
        public string eventType = "AddDecoration";
        public bool locked = false;
        public string decorationImage = "";
        public float[] position = new float[2];
        public string relativeTo = "Tile";
        public float[] pivotOffset = new float[2];
        public float rotation = 0;
        public string lockRotation = "Disabled";
        public float[] scale = new float[2];
        public string lockScale = "Disabled";
        public int[] tile = new int[2];
        public string color = "ffffff";
        public float opacity = 100;
        public float depth = -1;
        public float[] parallax = new float[2];
        public float[] parallaxOffset = new float[2];
        public string tag = "";
        public string imageSmoothing = "Enabled";
    }
}
