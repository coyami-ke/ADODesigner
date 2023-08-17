using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
#pragma warning disable 1591
namespace ADODesigner.Converters
{
    /// <summary>
    /// ADOFAI Event "Add decoration"
    /// </summary>
    public class AddDecoration
    {
        [JsonPropertyName("floor")]
        public int Floor { get; set; } = 0;
        [JsonPropertyName("eventType")]
        public string EventType { get; set; } = "AddDecoration";
        [JsonPropertyName("locked")]
        public bool Locked { get; set; } = false;
        [JsonPropertyName("decorationImage")]
        public string DecorationImage { get; set; } = "";
        [JsonPropertyName("position")]
        public float[] Position { get; set; } = new float[2];
        [JsonPropertyName("relativeTo")]
        public string RelativeTo { get; set; } = "Tile";
        [JsonPropertyName("pivotOffset")]
        public float[] PivotOffset { get; set; } = new float[2];
        [JsonPropertyName("rotation")]
        public float Rotation { get; set; } = 0;
        [JsonPropertyName("lockRotation")]
        public string LockRotation { get; set; } = "Disabled";
        [JsonPropertyName("scale")]
        public float[] Scale { get; set; } = new float[2];
        [JsonPropertyName("lockScale")]
        public string LockScale { get; set; } = "Disabled";
        [JsonPropertyName("tile")]
        public int[] Tile { get; set; } = new int[2];
        [JsonPropertyName("color")]
        public string Color { get; set; } = "ffffff";
        [JsonPropertyName("opacity")]
        public float Opacity { get; set; } = 100;
        [JsonPropertyName("depth")]
        public float Depth { get; set; } = -1;
        [JsonPropertyName("parallax")]
        public float[] Parallax { get; set; } = new float[2];
        [JsonPropertyName("parallaxOffset")]
        public float[] ParallaxOffset { get; set; } = new float[2];
        [JsonPropertyName("tag")]
        public string Tag { get; set; } = "";
        [JsonPropertyName("imageSmoothing")]
        public string ImageSmoothing { get; set; } = "Enabled";
    }
}
