using ADODesigner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
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
        public float[] angleData;
        /// <summary>
        /// Settings for level
        /// </summary>
        public CustomLevelSettings settings;
        /// <summary>
        /// Decorations
        /// </summary>
        public List<AddDecoration> decorations;
        /// <summary>
        /// Actions
        /// </summary>
        public JsonArray actions;
    }
    public class CustomLevelSettings
    {
        public int version = 13;
        public string artist = "Artist";
        public string specialArtistType = "None";
        public string song = "Song";
        public string author = "Coyami-Ke";
        public string separateCountdownTime = "";
        public string previewImage = "";
        public string previewIcon = "";
        public string previewIconColor = "FFFFFF";
        public int previewSongStart = 0;
        public int previewSongDuration = 10;
        public string seizureWarning = "Disabled";
        public string levelDesc = "Please, add my nick - Coyami-Ke, if you're using my program :d";
        public string levelTags = "";
        public string artistLinks = "";
        public int difficulty = 1;
        public string requiredMods = "";
        public string songFilename = "";
        public float bpm = 100;
        public int volume = 100;
        public int offset = 0;
        public int pitch = 100;
        public string hitsound = "Kick";
        public int hitsoundVolume = 100;
        public int countdownTicks = 4;
        public string trackColorType;
        public string trackColor = "FFFFFFFF";
        public string secondaryTrackColor = "FFFFFFFF";
        public int trackColorAnimDuration = 2;
        public string trackColorPulse = "FFFFFFFF";
        public int trackPulseLength = 16;
        public string trackStyle = "Neon";
        public string trackTexture = "";
        public int trackTextureScale = 1;
        public int trackGlowIntensity = 100;
        public string trackAnimation = "None";
        public int beatsAhead;
        public string trackDisappearAnimation;
        public int beatsBehind;
        public string backgroundColor;
        public string showDefaultBGIfNoImage;
        public string showDefaultBGTile;
        public string defaultBGTileColor;
        public string defaultBGShapeType;
        public string defaultBGShapeColor;
        public string bgImage;
        public float[] parallax = new float[2]; 
        public string bgDisplayMode;
        public string imageSmoothing;
        public string lockRot;
        public string loopBG;
        public int scalingRatio;
        public string relativeTo;
        public float[] position = new float[2];
        public float rotation;
        public float zoom;
        public int vidOffset;
        public string floorIconOutlines;
        public string stickToFloors;
        public string planetEase;
        public int planetEaseParts;
        public string planetEasePartBehavior;
        public string defaultTextColor;
        public string defaultTextShadowColor;
        public string congratsText;
        public string perfectText;
        public bool legacyFlash;
        public bool legacyCamRelativeTo;
        public bool legacySpriteTiles;
    }
}
