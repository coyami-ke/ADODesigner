using ADODesigner.Models;
using ADODesigner.ViewModels.Buffer;
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
        [ObservableProperty]
        private int durationAnimation = 1000;
        [ObservableProperty]
        private int selectedDurationAnimation;
        [ObservableProperty]
        private BufferStates<ObservableList<ObservableList<KeyFrame>>> bufferTimeLines = new();
        #endregion
        #region Actions with KeyFrames
        /// <summary>
        /// Adds keyframe with installed key.
        /// </summary>
        /// <param name="key"></param>
        public void AddKeyFrame(string key)
        {
            TimeLines[0].Add(new() { Key = key });
        }
        /// <summary>
        /// Adds a keyframe to the editor.
        /// </summary>
        /// <param name="keyFrame"></param>
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
        /// Finds a keyframe using a key. (WARNING! If the method does not find a key frame with a key, then it will return (-1, -1))
        /// </summary>
        /// <param name="key">Key</param>
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
        /// <summary>
        /// Tryes to find a keyframe using a key.
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>If keyframe with key exists, return true, else false</returns>
        public bool TryFindKeyFrame(string key)
        {
            for (int i = 0; TimeLines.Count < i; i++)
            {
                for (int s = 0; TimeLines[i].Count > s; s++)
                {
                    if (TimeLines[i][s].Key == key) return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Removes a keyframe from the editor.
        /// </summary>
        /// <param name="key"></param>
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
        #region RelayCommands
        [RelayCommand]
        private void UndoKeyFrames()
        {
            TimeLines = BufferTimeLines.Buffer.Pop();
        }
        [RelayCommand]
        private void SelectAllKeyFrames()
        {
            for (int i = 0; i < TimeLines.Count; i++)
            {
                Parallel.For(0, TimeLines[i].Count, (s) =>
                {
                    TimeLines[i][s].IsSelected = true;
                });
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
