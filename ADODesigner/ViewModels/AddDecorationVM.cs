using ADODesigner.Models;
using ADODesigner.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable 1591
namespace ADODesigner.ViewModels
{

    public partial class AddDecorationVM : ObservableObject
    {
        [ObservableProperty]
        private Decoration decoration = new();
        [RelayCommand]
        private void AddDecoration()
        {
            EditorView.Editor.AddDecoration(this.Decoration);
        }
    }
}
