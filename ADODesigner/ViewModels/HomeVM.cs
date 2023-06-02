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
        private ObservableCollection<Project> projects = App.Config.Projects;

        [RelayCommand]
        private void CreateProject()
        {
            CreateProjectView createProjectView = new();
            createProjectView.Show();
            //Project project = new();
            //project.Name = "My Project";
            //project.Path = "D:\\Projects\\ComposerTrack";
            //Projects.Add(project);

            //App.Config.Projects = Projects;
        }
        [RelayCommand]
        private void OpenProject()
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();

            dialog.IsFolderPicker = true;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                Project project = new Project();
                project.Path = dialog.FileName;
                Projects.Add(project);
            }
        }
    }
}
