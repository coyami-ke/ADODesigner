using ADODesigner.Models;
using ADODesigner.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
#nullable disable
#pragma warning disable 1591
namespace ADODesigner.ViewModels
{
    public partial class CreateProjectVM : ObservableObject
    {
        [ObservableProperty]
        private Project project = new();

        [RelayCommand]
        private void CreateProject()
        {
            //App.Config.Projects.Add(Project);
            Project.Decorations = new();
            Project.TimeLines = new();

            Directory.CreateDirectory(Path.Combine("projects", Project.Name));


            File.Create(Path.Combine("projects", Project.Name, "main.json")).Dispose();
            JsonSerializerOptions options = new()
            {
                WriteIndented = true,
            };
            File.WriteAllText(Path.Combine("projects", Project.Name, "main.json"), JsonSerializer.Serialize<Project>(Project, options));

            EditorView editor = new();
            editor.Show();
            EditorView.Editor.Project = this.Project;
        }
    }
}
