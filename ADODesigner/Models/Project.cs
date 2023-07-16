using CommunityToolkit.Mvvm.ComponentModel;
using Gstc.Collections.ObservableLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
#nullable disable
#pragma warning disable 1591
namespace ADODesigner.Models
{
    /// <summary>
    /// Project class. Used in data storage.
    /// </summary>
    public partial class Project : ObservableObject
    {
        [ObservableProperty]
        private string path;
        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private string pathToADOFAILevel;
        [ObservableProperty]
        private ObservableList<ObservableList<KeyFrame>> timeLines;
        [ObservableProperty]
        private ObservableList<Decoration> decorations;
    }
}
