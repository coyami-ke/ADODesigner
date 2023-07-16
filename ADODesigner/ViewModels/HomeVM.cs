using ADODesigner.Models;
using ADODesigner.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
#nullable disable

namespace ADODesigner.ViewModels
{
    public partial class HomeVM : ObservableObject
    {
        [ObservableProperty]
        private Project selectedProject = new();
        [ObservableProperty]
        private ObservableCollection<Project> projects;

        [RelayCommand]
        private void CreateProject()
        {
            CreateProjectView createProjectView = new();
            createProjectView.Show();
        }
        [RelayCommand]
        private void OpenProject()
        {
            CommonOpenFileDialog dialog = new();
            dialog.IsFolderPicker = true;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                JsonSerializerOptions options = new()
                {
                    WriteIndented = true,
                };
                Project project = JsonSerializer.Deserialize<Project>(Path.Combine(dialog.FileName, "main.json"), options);

                Projects.Add(project);

                EditorView editor = new();
                editor.Show();
                EditorView.Editor.Project = project;
            }
        }
        public HomeVM()
        {
            //Projects = App.Config.Projects;
        }
    }
}
