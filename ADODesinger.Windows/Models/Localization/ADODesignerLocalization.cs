using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ADODesigner.Windows.Models.Localization
{
    public enum SupportedLanguages
    {
        English,
        Korean,
    }
    public partial class ADODesignerLocalization : ObservableObject
    {
        public const string DIRECTORY_LANG = "Localization/";
        [ObservableProperty]
        private ADODesignerLocalization_Editor editor = new();
        [ObservableProperty]
        private ADODesignerLocalization_File file = new();
        [ObservableProperty]
        private ADODesignerLocalization_CombineParts combineParts = new();
        [ObservableProperty]
        private ADODesinerLocalization_ImportFrom import = new();
        [ObservableProperty]
        private ADODesignerLocalization_TimeLineElements timeLineElements = new();
        [ObservableProperty]
        private ADODesignerLocalization_Messages messages = new();
        [ObservableProperty]
        private ADODesignerLocalization_Guide guide = new();

        public static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true,
        };
        public static ADODesignerLocalization? Parse(string json)
        {
            return JsonSerializer.Deserialize<ADODesignerLocalization>(json, JsonOptions);
        }
        public string ToJson()
        {
            return JsonSerializer.Serialize(this, JsonOptions);
        }
    }
    public class ADODesignerLocalization_Editor
    {
        public string Edit { get; set; } = "Edit";
        public string Copy { get; set; } = "Copy";
        public string Paste { get; set; } = "Paste";
        public string Cut { get; set; } = "Cut";
        public string Dublicate { get; set; } = "Duplicate";
        public string Delete { get; set; } = "Delete";
        public string Select { get; set; } = "Select";
        public string SelectAll { get; set; } = "Select All";
        public string Add { get; set; } = "Add";
        public string Import { get; set; } = "Import";
        public string TimeLine { get; set; } = "Timeline";
        public string AutoSetTimeLine { get; set; } = "Auto Set Timeline";
        public string Properties { get; set; } = "Properties";
        public string Actions { get; set; } = "Actions:";
        public string Decorations { get; set; } = "Decorations:";
        public string Tools { get; set; } = "Tools";
        public string CombinePartsTool { get; set; } = "Combine Parts Tool...";
        public string Help { get; set; } = "Help";
        public string Guide { get; set; } = "Guide...";
        public string ChangeLanguage { get; set; } = "Change Language";
    }
    public class ADODesignerLocalization_File
    {
        public string File { get; set; } = "File";
        public string Save { get; set; } = "Save";
        public string SaveAs { get; set; } = "Save As";
        public string Open { get; set; } = "Open";
        public string ADODesignerFile { get; set; } = "ADODesigner File...";
        public string ADOFAILevel { get; set; } = "ADOFAI Level...";
        public string SaveAsAdofai { get; set; } = "ADOFAI Level";
        public string SaveAsAdod { get; set; } = "ADODesigner Project";
        public string SavedMessage { get; set; } = "Saved!";
    }
    public class ADODesignerLocalization_CombineParts
    {
        public string AddPart { get; set; } = "Add Part";
        public string Remove { get; set; } = "Remove";
        public string Chart { get; set; } = "Chart (Gameplay)";
        public string Combine { get; set; } = "Combine";
        public string CombineParts { get; set; } = "Combine Parts";
    }
    public class ADODesinerLocalization_ImportFrom
    {
        public string Import { get; set; } = "Import";
        public string ImportFromAdofaiLevel { get; set; } = "Import From ADOFAI Level";
        public string FirstFloor { get; set; } = "First Floor";
        public string SecondFloor { get; set; } = "Secons Floor";
        public string ImportMoveDecorations { get; set; } = @"Import ""Move Decoration""";
        public string RemoveDetectedEvents { get; set; } = "Remove Detected Events";
    }
    public class ADODesignerLocalization_TimeLineElements
    {
        public string Tag { get; set; } = "Tag";
        public string Duration { get; set; } = "Duration";
        public string Position { get; set; } = "Position";
        public string Rotation { get; set; } = "Rotation";
        public string Scale { get; set; } = "Scale";
        public string Parallax { get; set; } = "Parallax";
        public string ParallaxOffset { get; set; } = "Parallax Offset";
        public string PivotOffset { get; set; } = "Pivot Offset";
        public string EventTag { get; set; } = "Event Tag";
        public string Image { get; set; } = "Image";
        public string Opacity { get; set; } = "Opacity";
        public string Depth { get; set; } = "Depth";
        public string AngleOffset { get; set; } = "Angle Offset";
        public string Ease { get; set; } = "Ease";
        public string FileExtension { get; set; } = "File Extension";
        public string AddableAngle { get; set; } = "Addable Angle";
        public string CountFrames { get; set; } = "Count Frames";
        public string Repetitions { get; set; } = "Repetitions";
        public string Count { get; set; } = "Count";
        public string AddablePosition { get; set; } = "Addable Position";
        public string ScaleDifference { get; set; } = "Scale Difference";
        public string OpacityDifference { get; set; } = "Opacity Difference";
        public string Color { get; set; } = "Color";
        public string ParallaxMultiplier { get; set; } = "Parallax Multiplier";
        public string OffsetSides { get; set; } = "Offset Sides";
        public string ColorLeftSide { get; set; } = "Color Left Side";
        public string ColorRightSide { get; set; } = "Color Right Side";
        public string ColorTopSide { get; set; } = "Color Left Side";
        public string ColorFrontSide { get; set; } = "Color Front Side";
        public string? GetLocalization(string? property)
        {
            Type type = this.GetType();

            if (property is null) return string.Empty;
            foreach (var prop in type.GetProperties())
            {
                if (prop.Name == property) return (string?)prop.GetValue(this);
            }
            return null;
        }
    }
    public class ADODesignerLocalization_Messages
    {
        public string Ok { get; set; } = "OK";
        public string Cancel { get; set; } = "Cancel";
        public string Yes { get; set; } = "Yes";
        public string No { get; set; } = "No";
        public string LevelIsNotSaved { get; set; } = "Your level is not saved";
        public string LevelIsNotSavedText { get; set; } = "Your level is not saved. Do you want to exit without saving?";
    }
    public class ADODesignerLocalization_Guide
    {
        public string Start { get; set; } = "Start";
        public string Start_HowToGetStarted { get; set; } = "How to get started";
        public string Start_OpenAdofai { get; set; } = "To get started, open ADOFAI level using the File > Open ADOFAI level or by pressing Ctrl+Shift+O";
        public string Start_Save { get; set; } = "Save ADOFAI level using the File > Save or by pressing Ctrl+S";
        public string Start_OpenAdod { get; set; } = "So you can open ADODesigner File using the File > Open ADODesigner File or by pressing Ctrl+O";

        public string FrameByFrame { get; set; } = "Frame-by-Frame animation";
        public string FrameByFrame_Description { get; set; } = "Frame animation is a constant change of the decoration. To use it, add it to the timeline using the menu Add > Frame-by-frame animation or use the hotkey F5";
        public string FrameByFrame_Example { get; set; } = "Example:";
        public string FrameByFrame_n1 { get; set; } = "* Where ";
        public string FrameByFrame_n2 { get; set; } = " is the frame number (i.e. video1, video2, ...)                  ";
        public string FrameByFrame_Formula { get; set; } = "* With each frame the angle ofsset is shifted according to a formula similar to: ";

        public string CombineParts { get; set; } = "Combine Parts";
        public string CombineParts_Tool { get; set; } = "Tool - Combine Parts";
        public string CombineParts_Description { get; set; } = "The tool was created to make collaboration easier. You can open this tool using the menu: Tools > Combine Parts.";
        public string CombineParts_HowToUse { get; set; } = "How to use";
        public string CombineParts_Action1 { get; set; } = "1) Select a chart using the button [...]";
        public string CombineParts_Action2 { get; set; } = "2) Add parts with effects";
        public string CombineParts_Combine { get; set; } = "So you can combine it using [Combine]";
        public string CombineParts_Warning { get; set; } = "*Warning! Only effect events are added to the original chart. Angle and gameplay data are not added.";

        public string TimeLineHotKeys { get; set; } = "Time Line Hot Keys";
        public string TimeLineHotKeys_W { get; set; } = "Increase the number timeline";
        public string TimeLineHotKeys_S { get; set; } = "Decrease the number timeline";
        public string TimeLineHotKeys_A { get; set; } = "Decrease the floor number";
        public string TimeLineHotKeys_D { get; set; } = "Increase the floor number";
        public string TimeLineHotKeys_Q { get; set; } = "Decrease duration number";
        public string TimeLineHotKeys_E { get; set; } = "Increase duration number";
        public string TimeLineHotKeys_MBM { get; set; } = "Rectangular selection of elements";

    }
}
