using ADODesigner.Windows.Models.Localization;
using ADODesinger.Windows.ViewModels.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ADODesinger.Windows.ViewModels
{
    public partial class SaveAsProjectViewModel : ObservableObject
    {
        [ObservableProperty]
        private ADODesignerLocalization localization = App.Localization;
        [ObservableProperty]
        private string pathToADODesignerFile = "";
        [ObservableProperty]
        private string pathToADOFAILevel = "";
        [RelayCommand]
        private void Enter()
        {
            WeakReferenceMessenger.Default.Send<SaveAsProjectMessage>(new(this));
        }
        [RelayCommand]
        private void SelectADODesignerFile()
        {
            CommonSaveFileDialog dialog = new();
            dialog.DefaultFileName = Path.GetFileName(PathToADODesignerFile);
            dialog.DefaultDirectory = Path.GetDirectoryName(PathToADODesignerFile);
            dialog.Title = "Select name for adodesigner file";
            dialog.Filters.Add(new("ADODesigner File", ".adod"));
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                this.PathToADODesignerFile = dialog.FileName;
            }
        }
        [RelayCommand]
        private void SelectADOFAILevel()
        {
            CommonSaveFileDialog dialog = new();
            dialog.DefaultDirectory = Path.GetDirectoryName(this.PathToADOFAILevel);
            dialog.DefaultFileName = Path.GetFileName(PathToADOFAILevel);
            dialog.Title = "Select name for adofai file";
            dialog.Filters.Add(new("ADOFAI Level", ".adofai"));
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                this.PathToADOFAILevel = dialog.FileName;
            }
        }
    }
}
