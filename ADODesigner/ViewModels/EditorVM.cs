using ADODesigner.Models;
using ADODesigner.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gstc.Collections.ObservableLists;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ADODesigner.ViewModels
{
    public partial class EditorVM : ObservableRecipient
    {
        #region Properties
        [ObservableProperty]
        private ObservableList<Decoration> decorations = new();
        [ObservableProperty]
        private ObservableList<ObservableList<KeyFrame>> timeLines = new();
        #endregion

        #region Actions with KeyFrames
        public void AddKeyFrame(string key)
        {
            TimeLines[0].Add(new() { Key = key });
        }
        public void AddKeyFrame(KeyFrame keyFrame)
        {
            TimeLines[0].Add(keyFrame);
        }
        public KeyFrame? FindKeyFrame(string key)
        {
            for (int i = 0; TimeLines.Count < 0; i++)
            {
                for (int s = 0; TimeLines[i].Count < 0; s++)
                {
                    if (TimeLines[i][s].Key == key) return TimeLines[i][s];
                }
            }
            return null;
        }

        [RelayCommand]
        private void RemoveKeyFrame(string key)
        {
            for (int i = 0;  i < TimeLines.Count; i++)
            {
                for (int s = 0; i < TimeLines[i].Count; i++)
                {
                    if (TimeLines[i][s].Key == key) TimeLines[i].RemoveAt(s);
                }
            }
        }
        #endregion
        #region Actions with Decorations
        [RelayCommand]
        private void AddDecoration()
        {
            AddDecorationView view = new();
            view.Show();
        }

        public void AddDecoration(Decoration decoration)
        {
            Decorations.Add(decoration);
        }
        #endregion
        #region Constructors
        public EditorVM()
        {
            TimeLines.Add(new());
        }
        #endregion
    }
}
