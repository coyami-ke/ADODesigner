using ADODesigner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
#nullable disable
namespace ADODesigner.Converters
{
    /// <summary>
    /// Class of Level ADOFAI.
    /// </summary>
    public class CustomLevel
    {
        /// <summary>
        /// Angles of tiles
        /// </summary>
        [JsonPropertyName("angleData")]
        public List<float> AngleData { get; set; } = new();
        /// <summary>
        /// Settings for level
        /// </summary>
        [JsonPropertyName("settings")]
        public CustomLevelSettings Settings { get; set; } = new();
        /// <summary>
        /// Decorations
        /// </summary>
        [JsonPropertyName("decorations")]
        public List<AddDecoration> Decorations { get; set; } = new();
        /// <summary>
        /// Actions
        /// </summary>
        [JsonPropertyName("actions")]
        public JsonArray Actions { get; set; } = new();
    }
    public class CustomLevelSettings
    {
        [JsonPropertyName("version")]
        public int Version { get; set; } = 13;

        [JsonPropertyName("artist")]
        public string Artist { get; set; } = "";

        [JsonPropertyName("specialArtistType")]
        public string SpecialArtistType { get; set; } = "None";

        [JsonPropertyName("artistPermission")]
        public string ArtistPermission { get; set; } = "";

        [JsonPropertyName("song")]
        public string Song { get; set; } = "";

        [JsonPropertyName("author")]
        public string Author { get; set; } = "";

        [JsonPropertyName("separateCountdownTime")]
        public string SeparateCountdownTime { get; set; } = "Enabled";

        [JsonPropertyName("previewImage")]
        public string PreviewImage { get; set; } = "";

        [JsonPropertyName("previewIcon")]
        public string PreviewIcon { get; set; } = "";

        [JsonPropertyName("previewIconColor")]
        public string PreviewIconColor { get; set; } = "ffffff";

        [JsonPropertyName("previewSongStart")]
        public int PreviewSongStart { get; set; } = 0;

        [JsonPropertyName("previewSongDuration")]
        public int PreviewSongDuration { get; set; } = 10;

        [JsonPropertyName("seizureWarning")]
        public string SeizureWarning { get; set; } = "Disabled";

        [JsonPropertyName("levelDesc")]
        public string LevelDesc { get; set; } = "";

        [JsonPropertyName("levelTags")]
        public string LevelTags { get; set; } = "";

        [JsonPropertyName("artistLinks")]
        public string ArtistLinks { get; set; } = "";

        [JsonPropertyName("difficulty")]
        public int Difficulty { get; set; } = 1;
        [JsonPropertyName("requiredMods")]
        public List<Object> RequiredMods { get; set; } = new();

        [JsonPropertyName("songFilename")]
        public string SongFilename { get; set; } = "";

        [JsonPropertyName("bpm")]
        public int Bpm { get; set; } = 300;

        [JsonPropertyName("volume")]
        public int Volume { get; set; } = 100;

        [JsonPropertyName("offset")]
        public int Offset { get; set; } = 0;

        [JsonPropertyName("pitch")]
        public int Pitch { get; set; } = 100;

        [JsonPropertyName("hitsound")]
        public string Hitsound { get; set; } = "Kick";

        [JsonPropertyName("hitsoundVolume")]
        public int HitsoundVolume { get; set; } = 100;

        [JsonPropertyName("countdownTicks")]
        public int CountdownTicks { get; set; } = 4;

        [JsonPropertyName("trackColorType")]
        public string TrackColorType { get; set; } = "Single";

        [JsonPropertyName("trackColor")]
        public string TrackColor { get; set; } = "debb7b";

        [JsonPropertyName("secondaryTrackColor")]
        public string SecondaryTrackColor { get; set; } = "ffffff";

        [JsonPropertyName("trackColorAnimDuration")]
        public int TrackColorAnimDuration { get; set; } = 2;

        [JsonPropertyName("trackColorPulse")]
        public string TrackColorPulse { get; set; } = "None";

        [JsonPropertyName("trackPulseLength")]
        public int TrackPulseLength { get; set; } = 10;

        [JsonPropertyName("trackStyle")]
        public string TrackStyle { get; set; } = "Standard";

        [JsonPropertyName("trackGlowIntensity")]
        public int TrackGlowIntensity { get; set; } = 100;

        [JsonPropertyName("trackAnimation")]
        public string TrackAnimation { get; set; } = "None";

        [JsonPropertyName("beatsAhead")]
        public int BeatsAhead { get; set; } = 3;

        [JsonPropertyName("trackDisappearAnimation")]
        public string TrackDisappearAnimation { get; set; } = "None";

        [JsonPropertyName("beatsBehind")]
        public int BeatsBehind { get; set; } = 4;

        [JsonPropertyName("backgroundColor")]
        public string BackgroundColor { get; set; } = "000000";

        [JsonPropertyName("showDefaultBGIfNoImage")]
        public string ShowDefaultBgIfNoImage { get; set; } = "Enabled";

        [JsonPropertyName("bgImage")]
        public string BgImage { get; set; } = "";

        [JsonPropertyName("bgImageColor")] 
        public string BgImageColor { get; set; } = "ffffff";

        [JsonPropertyName("parallax")]
        public float[] Parallax { get; set; } = new float[2];

        [JsonPropertyName("bgDisplayMode")]
        public string BgDisplayMode { get; set; } = "FitToScreen";

        [JsonPropertyName("lockRot")]
        public string LockRot { get; set; } = "Disabled";

        [JsonPropertyName("loopBG")]
        public string LoopBg { get; set; } = "Disabled";

        [JsonPropertyName("unscaledSize")]
        public int UnscaledSize { get; set; } = 100;

        [JsonPropertyName("relativeTo")]
        public string RelativeTo { get; set; } = "Player";

        [JsonPropertyName("position")]
        public float[] Position { get; set; } = new float[2];

        [JsonPropertyName("rotation")]
        public float Rotation { get; set; } = 0;

        [JsonPropertyName("zoom")]
        public int Zoom { get; set; } = 200;

        [JsonPropertyName("pulseOnFloor")]
        public string PulseOnFloor { get; set; } = "Enabled";

        [JsonPropertyName("bgVideo")]
        public string BgVideo { get; set; } = "";

        [JsonPropertyName("loopVideo")]
        public string LoopVideo { get; set; } = "Disabled";

        [JsonPropertyName("vidOffset")]
        public int VidOffset { get; set; } = 0;

        [JsonPropertyName("floorIconOutlines")]
        public string FloorIconOutlines { get; set; } = "Disabled";

        [JsonPropertyName("stickToFloors")]
        public string StickToFloors { get; set; } = "Enabled";

        [JsonPropertyName("planetEase")]
        public string PlanetEase { get; set; } = "Linear";

        [JsonPropertyName("planetEaseParts")]
        public int PlanetEaseParts { get; set; } = 1;

        [JsonPropertyName("planetEasePartBehavior")]
        public string PlanetEasePartBehavior { get; set; } = "Mirror";
        [JsonPropertyName("defalutTextColor")]
        public string DefaultTextColor { get; set; } = "ffffff";
        [JsonPropertyName("defaultTextShadowColor")]
        public string DefaultTextShadowColor { get; set; } = "00000050";
        [JsonPropertyName("congratsText")]
        public string CongratsText { get; set; } = "";
        [JsonPropertyName("perfectText")]
        public string PerfectText { get; set; } = "";

        [JsonPropertyName("legacyFlash")]
        public bool LegacyFlash { get; set; } = false;

        [JsonPropertyName("legacyCamRelativeTo")]
        public bool LegacyCamRelativeTo { get; set; } = false;

        [JsonPropertyName("legacySpriteTiles")]
        public bool LegacySpriteTiles { get; set; } = false;
    }
}
