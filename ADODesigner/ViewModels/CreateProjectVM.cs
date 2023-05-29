using ADODesigner.Models;
using ADODesigner.ViewModels.Messengers;
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
        private void CreateProjectCommand()
        {
            WeakReferenceMessenger.Default.Send<CreateProjectMessenger>(new CreateProjectMessenger(Project));
        }
    }
}
