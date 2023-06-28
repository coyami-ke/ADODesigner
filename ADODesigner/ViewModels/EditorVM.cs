using ADODesigner.Models;
using ADODesigner.ViewModels.Messengers;
using ADODesigner.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
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
        [ObservableProperty]
        private KeyFrame selectedKeyFrame = new();
        [ObservableProperty]
        private Decoration selectedDecoration = new();
        [ObservableProperty]
        private Project project;
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
                for (int s = 0; s < TimeLines[i].Count; s++)
                {
                    if (TimeLines[i][s].Key == key) return TimeLines[i][s];
                }
            }
            return null;
        }
        /// <summary>
        /// Find keyframe with key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Index of TimeLines. Index of TimeLines[i]</returns>
        public (int, int) GetKeyFrameIndex(string key)
        {
            for (int i = 0; TimeLines.Count < 0; i++)
            {
                for (int s = 0; TimeLines[i].Count < 0; s++)
                {
                    if (TimeLines[i][s].Key == key) return (i, s);
                }
            }
            return (-1, -1);
        }
        public bool TryFindKeyFrame(string key)
        {
            for (int i = 0; TimeLines.Count < 0; i++)
            {
                for (int s = 0; TimeLines[i].Count > s; s++)
                {
                    if (TimeLines[i][s].Key == key) return true;
                }
            }
            return false;
        }
        public void RemoveKeyFrame(string key)
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
            Project = new();
            TimeLines.Add(new());
        }
        #endregion
    }
}
