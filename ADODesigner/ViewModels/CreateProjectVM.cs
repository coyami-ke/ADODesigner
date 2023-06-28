using ADODesigner.Models;
using ADODesigner.ViewModels.Messengers;
using ADODesigner.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable
namespace ADODesigner.ViewModels
{
    public partial class CreateProjectVM : ObservableObject
    {
        [ObservableProperty]
        private Project project;

        [RelayCommand]
        private void CreateProject()
        {
            EditorView editor = new();
            editor.Show();
            EditorView.Editor.Project = this.Project;
        }
    }
}
