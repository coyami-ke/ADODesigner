using ADODesigner.Animations;
using ADODesigner.Converters;
using ADODesigner.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gstc.Collections.ObservableLists;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using System.Windows;

namespace ADODesigner.GUI.ViewModels
{
    public partial class EditorVM : ObservableRecipient
    {
        [ObservableProperty]
        private ObservableList<BallsAnimationArgs> ballsAnimations = new();
        [ObservableProperty]
        private ObservableList<WindAnimationArgs> windAnimations = new();
        [ObservableProperty]
        private CustomLevel customLevel = new();
        [ObservableProperty]
        private ObservableList<KeyFrame> keyFrames = new();
        [ObservableProperty]
        private ObservableList<Decoration> decoration = new();
        [RelayCommand]
        private void ConvertToADOFAILevel()
        {
            List<KeyFrame> keyFrames = new();
            List<Decoration> decorations = new();
            for (int i = 0; i < BallsAnimations.Count; i++)
            {
                BallsAnimation animation = new(BallsAnimations[i]);
                (KeyFrame[], Decoration[]) result = animation.CreateAnimation();
                keyFrames.AddRange(result.Item1);
                decorations.AddRange(result.Item2);
            }
            for (int i = 0; i < WindAnimations.Count; i++)
            {
                WindAnimation animation = new(WindAnimations[i]);
                (KeyFrame[], Decoration[]) result = animation.CreateAnimation();
                keyFrames.AddRange(result.Item1);
                decorations.AddRange(result.Item2);
            }
            for (int i = 0; i < decorations.Count; i++)
            {
                CustomLevel.Decorations.Add(DecorationConverter.Convert(decorations[i]));
            }
            for (int i = 0; i < keyFrames.Count; i++)
            {
                CustomLevel.Actions.Add(KeyFrameConverter.Convert(keyFrames[i]));
            }
            File.WriteAllText("result.adofai", JsonSerializer.Serialize(CustomLevel));
        }
        [RelayCommand]
        private void OpenADOFAILevel()
        {
            //CommonOpenFileDialog dialog = new("Select adofai level");
            //dialog.DefaultExtension = "adofai";
            //CommonFileDialogResult result = dialog.ShowDialog();
            //if (result == CommonFileDialogResult.Ok) 
            //{
            //    CustomLevel = CustomLevel.Parse(File.ReadAllText(dialog.FileName));
            //    MessageBox.Show(CustomLevel.Events[0].EventType);
            //}
        }
        [RelayCommand]
        private void AddBallsAnimation()
        {

        }
        protected override void OnActivated()
        {

        }
        [RelayCommand]
        private void NewProject()
        {
            CustomLevel = new();
        }
        public EditorVM()
        {
        }
    }
}
