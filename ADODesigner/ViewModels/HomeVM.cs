using ADODesigner.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace ADODesigner.ViewModels
{
    public partial class HomeVM : ObservableObject
    {
        [ObservableProperty]
        private Project selectedProject;
        [ObservableProperty]
        private ObservableCollection<Project> projects;

        [RelayCommand]
        private void CreateProject()
        {

        }
        [RelayCommand]
        private void OpenProject()
        {

        }
    }
}
