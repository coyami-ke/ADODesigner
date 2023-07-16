using ADODesigner.Converters;
using ADODesigner.Core.API;
using ADODesigner.Models;
using ADODesigner.ViewModels.Buffer;
using ADODesigner.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gstc.Collections.ObservableLists;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
#pragma warning disable 1591
namespace ADODesigner.ViewModels
{
    /// <summary>
    /// Main editor in ADODesigner.
    /// </summary>
    public partial class EditorVM : ObservableObject
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
        [ObservableProperty]
        private BufferStates<ObservableList<Decoration>> bufferDecorations = new();
        #endregion
        #region Actions
        public void Save()
        {

        }
        #endregion
        #region Actions (RelayCommands)
        [RelayCommand]
        private void Editor_Save()
        {
            Save();
        }
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
        public void UndoKeyFrames()
        {
            TimeLines = BufferTimeLines.Buffer.Pop();
        }
        #endregion
        #region Actions with KeyFrames (RelayCommands)
        [RelayCommand]
        private void Editor_UndoKeyFrames()
        {
            TimeLines = BufferTimeLines.Buffer.Pop();
        }
        [RelayCommand]
        private void Editor_SelectAllKeyFrames()
        {
            for (int i = 0; i < TimeLines.Count; i++)
            {
                for (int s = 0; s < TimeLines[i].Count; s++)
                {
                    TimeLines[i][s].IsSelected = true;
                }
            }
        }
        [RelayCommand]
        private void Editor_ConvertToADOFAILevel()
        {
            CommonOpenFileDialog dialog = new();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                CustomLevel level = new CustomLevel();
                for (int i = 0; i < TimeLines.Count; i++)
                {
                    for (int s = 0; s < TimeLines[i].Count; s++)
                    {
                        level.actions.Add(KeyFrameConverter.Convert(TimeLines[i][s]));
                    }
                }

                for (int i = 0; i < Decorations.Count; i++)
                {
                    level.decorations.Add(DecorationConverter.Convert(Decorations[i]));
                }
            }
        }
        [RelayCommand]
        private void Editor_RemoveKeyFrames()
        {
            for (int i = 0; i < TimeLines.Count; i++)
            {
                for (int s = 0; s < TimeLines[i].Count; s++)
                {
                    if (TimeLines[i][s].IsSelected) RemoveKeyFrame(TimeLines[i][s].Tag);
                }
            }
        }
        [RelayCommand]
        private void Editor_AddKeyFrame()
        {
            AddKeyFrame(Guid.NewGuid().ToString());
        }
        [RelayCommand]
        private void Editor_ShiftKeyframeToRight()
        {
            for (int i = 0; i < TimeLines.Count; i++)
            {
                for (int s = 0; s < TimeLines[i].Count; s++)
                {
                    if (TimeLines[i][s].IsSelected) TimeLines[i][s].Floor++;
                }
            }
        }
        [RelayCommand]
        private void Editor_ShiftKeyframeToLeft()
        {
            for (int i = 0; i < TimeLines.Count; i++)
            {
                for (int s = 0; s < TimeLines[i].Count; s++)
                {
                    if (TimeLines[i][s].IsSelected) TimeLines[i][s].Floor--;
                }
            }
        }
        #endregion
        #region Actions with Decorations
        /// <summary>
        /// Adds a new decoration
        /// </summary>
        /// <param name="decoration"></param>
        public void AddDecoration(Decoration decoration)
        {
            Decorations.Add(decoration);
        }
        /// <summary>
        /// Changes property IsSelected for <see cref="Decorations"/>
        /// </summary>
        public void SelectAllDecorations()
        {
            for (int i = 0; i < Decorations.Count; i++)
            {
                Decorations[i].IsSelected = true;
            }
        }
        /// <summary>
        /// Removes selected decorations
        /// </summary>
        public void RemoveDecoration()
        {
            for (int i = 0; i < Decorations.Count; i++)
            {
                if (Decorations[i].IsSelected == true) Decorations.RemoveAt(i);
            }
        }
        /// <summary>
        /// Removes decoration using tag
        /// </summary>
        /// <param name="id"></param>
        public void RemoveDecoration(string id)
        {
            for (int i = 0; i < Decorations.Count; i++)
            {
                if (Decorations[i].ID == id) Decorations.RemoveAt(i);
            }
        }
        /// <summary>
        /// Removes last element in <see cref="BufferDecorations"/> and replaces 
        /// </summary>
        public void UndoDecorations()
        {
            Decorations = BufferDecorations.Buffer.Pop();
        }
        #endregion
        #region Actions with Decorations (RelayCommands)
        [RelayCommand]
        private void Editor_AddDecoration()
        {
            AddDecorationView view = new();
            view.Show();
        }
        [RelayCommand]
        private void Editor_SelectAllDecorations()
        {
            SelectAllDecorations();
        }
        [RelayCommand]
        private void Editor_UndoDecorations()
        {
            UndoDecorations();
        }
        #endregion
        #region UI API
        public void InitKeyBindings()
        {
            EditorAPI.AddHotKeyToTimeLine(Editor_SelectAllKeyFrames, Key.A, ModifierKeys.Control);
            EditorAPI.AddHotKeyToTimeLine(Editor_UndoKeyFrames, Key.Z, ModifierKeys.Control);
            EditorAPI.AddHotKeyToTimeLine(Editor_RemoveKeyFrames, Key.Delete, ModifierKeys.None);
            EditorAPI.AddHotKeyToTimeLine(Editor_AddKeyFrame, Key.K, ModifierKeys.Shift);
            EditorAPI.AddHotKeyToTimeLine(Editor_ShiftKeyframeToLeft, Key.Left, ModifierKeys.None);
            EditorAPI.AddHotKeyToTimeLine(Editor_ShiftKeyframeToRight, Key.Right, ModifierKeys.None);

            EditorAPI.AddGlobalHotKey(Editor_Save, Key.S, ModifierKeys.Control);
        }
        #endregion
        #region Constructors
        public EditorVM()
        {
            Project = new();
            TimeLines.Add(new());
            TimeLines.CollectionChanged += TimeLines_CollectionChanged;
            Decorations.CollectionChanged += Decorations_CollectionChanged;
        }

        private void Decorations_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            BufferDecorations.Buffer.Push(Decorations);
        }

        private void TimeLines_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            BufferTimeLines.Buffer.Push(TimeLines);
        }
        #endregion
    }
}
