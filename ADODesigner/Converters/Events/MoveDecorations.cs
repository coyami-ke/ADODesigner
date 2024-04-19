using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ADODesigner.Models;
using ADODesigner.Animations;

#pragma warning disable 1591
namespace ADODesigner.Converters
{
    /// <summary>
    /// ADOFAI Event "Move Decorations"
    /// </summary>
    public class MoveDecorations : IADOFAIEvent
    {
        [JsonPropertyName("floor")]
        public int Floor { get; set; } = 0;
        [JsonPropertyName("duration")]
        public float Duration { get; set; }
        [JsonPropertyName("tag")]
        public string Tag { get; set; } = string.Empty;
        [JsonPropertyName("positionOffset")]
        public float[]? PositionOffset { get; set; } = null;
        [JsonPropertyName("pivotOffset")]
        public float[]? PivotOffset { get; set; } = null;
        [JsonPropertyName("scale")]
        public float[]? Scale { get; set; } = null;
        [JsonPropertyName("opacity")]
        public float? Opacity { get; set; } = null;
        [JsonPropertyName("depth")]
        public float? Depth { get; set; } = null;
        [JsonPropertyName("angleOffset")]
        public float AngleOffset { get; set; } = 0;
        [JsonPropertyName("ease")]
        public string Ease { get; set; } = "Linear";
        [JsonPropertyName("eventTag")]
        public string EventTag { get; set; } = "";
        [JsonPropertyName("parallax")]
        public float[]? Parallax { get; set; } = null;
        [JsonPropertyName("parallaxOffset")]
        public float[]? ParallaxOffset { get; set; } = null;
        [JsonPropertyName("rotationOffset")]
        public float? RotationOffset { get; set; } = null;
        [JsonPropertyName("eventType")]
        public string EventType { get; set; } = "MoveDecorations";
        [JsonPropertyName("color")]
        public string? Color { get; set; } = null;
        [JsonPropertyName("decorationImage")]
        public string? DecorationImage { get; set; } = null;
    }
}
