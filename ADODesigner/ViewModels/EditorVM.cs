using ADODesigner.Models;
using ADODesigner.ViewModels.Messengers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable
namespace ADODesigner.ViewModels
{
    public partial class EditorVM : ObservableRecipient, IRecipient<CreateProjectMessenger>
    {
        [ObservableProperty]
        private Project project;
        [ObservableProperty]
        private ObservableCollection<Decoration> decorations;
        protected override void OnActivated()
        {
            Messenger.Register<EditorVM, CreateProjectMessenger>(this, (r, m) => r.Receive(m));
        }
        public void Receive(CreateProjectMessenger message)
        {
        }
    }
}
