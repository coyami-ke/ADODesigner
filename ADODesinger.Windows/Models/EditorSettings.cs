using ADODesigner.Windows.Helpers;
using ADODesigner.Windows.Models.Localization;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ADODesigner.Windows.Models
{
    public partial class EditorSettings : ObservableObject
    {
        public const string FILE_NAME_SETTINGS = "editor.json";
        public static string PATH_TO_SETTINGS = Path.Combine(Environment.CurrentDirectory, ProgramDirectoriesConst.SETTINGS, "editor.json");
        [ObservableProperty]
        private SupportedLanguages language = SupportedLanguages.English;
    
        public static JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true,
        };
        public static EditorSettings? Parse(string json)
        {
            return JsonSerializer.Deserialize<EditorSettings>(json, JsonOptions);
        }
        public void SaveToFile()
        {
            File.WriteAllText(PATH_TO_SETTINGS, JsonSerializer.Serialize(this, JsonOptions));
        }
    }
}
