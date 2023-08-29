using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.JsonNodes;
using System.Text.Json.Serialization;
#nullable disable
#pragma warning disable 1591
namespace ADODesigner.Converters
{
    /// <summary>
    /// ADOFAI Event "Move Decorations"
    /// </summary>
    public class MoveDecorations
    {
        [JsonPropertyName("floor")]
        public int Floor { get; set; } = 0;
        [JsonPropertyName("duration")]
        public float Duration { get; set; }
        [JsonPropertyName("tag")]
        public string Tag { get; set; }
        [JsonPropertyName("positionOffset")]
        public float[] PositionOffset { get; set; } = new float[2];
        [JsonPropertyName("scale")]
        public float[] Scale { get; set; } = new float[2];
        [JsonPropertyName("opacity")]
        public float Opacity { get; set; }
        [JsonPropertyName("depth")]
        public float Depth { get; set; }
        [JsonPropertyName("angleOffset")]
        public float AngleOffset { get; set; }
        [JsonPropertyName("ease")]
        public string Ease { get; set; }
        [JsonPropertyName("eventTag")]
        public string EventTag { get; set; }
        [JsonPropertyName("parallax")]
        public float[] Parallax { get; set; } = new float[2];
        [JsonPropertyName("parallaxOffset")]
        public float[] ParallaxOffset { get; set; } = new float[2];
        [JsonPropertyName("rotationOffset")]
        public float RotationOffset { get; set; } = 0;
        [JsonPropertyName("eventType")]
        public string EventType { get; set; } = "MoveDecorations";
        [JsonPropertyName("rotationOffset")]
        public float RotationOffset { get; set; } = 0;
        [JsonPropertyName("eventType")]
        public string EventType { get; set; } = "MoveDecorations";
    }
}
