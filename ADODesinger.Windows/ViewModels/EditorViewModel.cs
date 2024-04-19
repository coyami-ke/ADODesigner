using Accessibility;
using ADODesigner.Animations;
using ADODesigner.Converters;
using ADODesigner.Windows.Helpers;
using ADODesigner.Windows.Models.TimeLine;
using ADODesinger.Windows.Helpers;
using ADODesinger.Windows.Models;
using ADODesinger.Windows.Models.TimeLine;
using ADODesinger.Windows.ViewModels.Messages;
using ADODesinger.Windows.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Threading;
using System.Xml.Linq;
using Windows.System;

namespace ADODesinger.Windows.ViewModels
{
    public partial class EditorViewModel : ObservableObject
    {
        [ObservableProperty]
        private ADODesignerMainSettings settings = new();

        [ObservableProperty]
        private ObservableCollection<TimeLineElementModel> timeLineElements = new();
        [ObservableProperty]
        private ObservableCollection<ChartTimeLineModel> chartTimeLineModels = new();
        [ObservableProperty]
        private ObservableCollection<VisualTimeLineModel> visualTimeLineModels = new();
        [ObservableProperty]
        private TimeLineElementModel[]? copiedTimeLineElements;

        [ObservableProperty]
        private float widthTimeLineCanvas;
        [ObservableProperty]
        private float heightTimeLineCanvas;
        [ObservableProperty]
        private double timeLineCanvasXCursor;
        [ObservableProperty]
        private double timeLineCanvasYCursor;
        [ObservableProperty]
        private bool timeLineUseAutoTimeLine = true;

        [ObservableProperty]
        private object? propertiesWindow;

        [ObservableProperty]
        private ADODesignerFile aDODesignerFile = new();
        [ObservableProperty]
        private string pathToProject = Path.Combine(Environment.CurrentDirectory, "unsaved.adod");
        [ObservableProperty]
        private int countActions;
        [ObservableProperty]
        private int countDecorations;

        [ObservableProperty]
        private string messageBoxText = "";
        public Stack<ObservableCollection<TimeLineElementModel>> UndoStackTimeLine { get; set; } = new();

        public CustomLevelParser CustomLevelParser { get; set; } = new();

        public EditorViewModel()
        {
            if (File.Exists(this.PathToProject))
            {
                ADODesignerFile? file = ADODesignerFile.Parse(File.ReadAllText(PathToProject));
                if (file != null)
                {
                    this.ADODesignerFile = file;
                    this.TimeLineElements = new();
                    TimeLineElementModel[] elements = this.ADODesignerFile.GetTimeLineElements();
                    foreach (var element in elements)
                    {
                        this.TimeLineElements.Add(element);
                    }
                    this.CustomLevelParser.Parse(File.ReadAllText(this.ADODesignerFile.PathToADOFAILevel));
                }
                UnselectAllTimeLineElements();
            }
            UpdateVisualTimeLine();

            WeakReferenceMessenger.Default.Register<NeedUnselectElementsMessage>(this, (sender, e) =>
            {
                UnselectAllTimeLineElements();
            });
            WeakReferenceMessenger.Default.Register<NeedChangePropertiesWindow>(this, (sender, e) =>
            {
                this.UpdatePropertiesWindow();
            });
            WeakReferenceMessenger.Default.Register<SaveAsProjectMessage>(this, (sender, e) =>
            {
                string oldAdofai = this.ADODesignerFile.PathToADOFAILevel;
                this.ADODesignerFile.PathToADOFAILevel = e.Value.PathToADOFAILevel;
                File.WriteAllText(e.Value.PathToADODesignerFile, this.ADODesignerFile.ToJson());
                File.WriteAllText(e.Value.PathToADOFAILevel, this.CustomLevelParser.ToJson());
                this.ADODesignerFile.PathToADOFAILevel = oldAdofai;
            });
        }
        public void UpdateVisualTimeLine()
        {
            ChartTimeLineModels.Clear();
            VisualTimeLineModels.Clear();
            for (int i = 0; i < CustomLevelParser.ADOFAILevel.AngleData.Count; i++)
            {
                ChartTimeLineModel model = new();
                model.MarginLine = new(i * 100, 0, 0, 0);
                model.Number = i;
                model.MarginText = new(i * 100 + 5, 0, 0, 0);
                if (CustomLevelParser.ADOFAILevel.AngleData[i] != 999) model.TextAngle = CustomLevelParser.ADOFAILevel.AngleData[i].ToString() + "f";
                else model.TextAngle = "M";
                model.MarginTextAngle = new(i * 100 + 50, 0, 0, 0);
                ChartTimeLineModels.Add(model);
            }

            for (int i = 0; i < 32; i++)
            {
                VisualTimeLineModel model = new();
                model.Margin = new(0, i * 40 + 30, 0, 0);
                model.Height = 40;
                model.Width = CustomLevelParser.ADOFAILevel.AngleData.Count * 100 + 1600;
                if (i % 2 == 0)
                {
                    model.BorderBrush = new SolidColorBrush(new Color() { R = 255, G = 255, B = 255, A = 4 });
                }
                else model.BorderBrush = Brushes.Transparent;

                VisualTimeLineModels.Add(model);
            }
            WidthTimeLineCanvas = CustomLevelParser.ADOFAILevel.AngleData.Count * 100 + 1600;
            HeightTimeLineCanvas = 32 * 40 + 30;
            this.CountActions = this.CustomLevelParser.ADOFAILevel.Actions.Count;
            this.CountDecorations = this.CustomLevelParser.ADOFAILevel.Decorations.Count;
        }

        [RelayCommand]
        public void Save() 
        {
            this.ADODesignerFile.SetTimeLine(this.TimeLineElements.ToArray()); // Write the timeline elements to adodesigner file
            File.WriteAllText(this.PathToProject, this.ADODesignerFile.ToJson()); // Write ADODesigner level to file (path to project, content: Json)

            if (File.Exists(this.ADODesignerFile.PathToADOFAILevel)) // if file with adodesigner data exists
            {
                this.CustomLevelParser = new();
                this.CustomLevelParser.Parse(File.ReadAllText(this.ADODesignerFile.PathToADOFAILevel));

                foreach (var a in this.CustomLevelParser.ADOFAILevel.Actions.ToArray())
                {
                    JsonObject? obj = a?.AsObject();
                    if (RemoverEventTag.ContainsTag("ADODesigner_Event", new string[] { "eventTag", "tag" }, obj))
                    {
                        this.CustomLevelParser.ADOFAILevel.Actions.Remove(a);
                    }
                }
                foreach (var e in this.TimeLineElements)
                {
                    foreach (var action in e.GetJsonActions())
                    {
                        if (action is not null && action.ContainsKey("eventTag"))
                        {
                            action["eventTag"] += " ADODesigner_Event";
                        }
                        else if (action is not null && action.ContainsKey("tag"))
                        {
                            action["tag"] += " ADODesigner_Event";
                        }
                        this.CustomLevelParser.ADOFAILevel.Actions.Add(action);
                    }
                    foreach (var action in e.GetJsonDecorations())
                    {
                        this.CustomLevelParser.ADOFAILevel.Decorations.Add(action);
                    }
                }
            }

            File.WriteAllText(this.ADODesignerFile.PathToADOFAILevel, this.CustomLevelParser.ToJson());
            // Update the count of actions
            this.CountActions = this.CustomLevelParser.ADOFAILevel.Actions.Count;
            this.CountDecorations = this.CustomLevelParser.ADOFAILevel.Decorations.Count;
        }
        [RelayCommand]
        public void Open()
        {
            CommonOpenFileDialog dialog = new();
            dialog.Filters.Add(new("ADODesigner File", "*.adod"));
            dialog.Title = "Select ADODesigner File";
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                ADODesignerFile? file = ADODesignerFile.Parse(File.ReadAllText(dialog.FileName));
                if (file != null)
                {
                    this.ADODesignerFile = file;
                    this.TimeLineElements = new();
                    TimeLineElementModel[] elements = this.ADODesignerFile.GetTimeLineElements();
                    foreach (var element in elements)
                    {
                        this.TimeLineElements.Add(element);
                    }
                    this.CustomLevelParser.Parse(File.ReadAllText(this.ADODesignerFile.PathToADOFAILevel));
                }
                UnselectAllTimeLineElements();
            }
            this.PropertiesWindow = null;
            this.UpdateVisualTimeLine();

            this.CountActions = this.CustomLevelParser.ADOFAILevel.Actions.Count;
            this.CountDecorations = this.CustomLevelParser.ADOFAILevel.Decorations.Count;
        }
        [RelayCommand]  
        private void OpenAdofaiFile()
        {
            CommonOpenFileDialog dialog = new();
            dialog.Filters.Add(new("ADOFAI Level", "*.adofai"));
            dialog.Title = "Select ADOFAI Level.";

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string? pathToAdod = Path.GetDirectoryName(dialog.FileName);
                if (pathToAdod is null) return;
                this.PathToProject = Path.Combine(pathToAdod, "level.adod");
                this.ADODesignerFile = new()
                {
                    PathToADOFAILevel = dialog.FileName
                };
                this.TimeLineElements = new(); 
                this.CustomLevelParser.Parse(File.ReadAllText(dialog.FileName));
            }
            this.PropertiesWindow = null;
            this.UpdateVisualTimeLine();

            this.CountActions = this.CustomLevelParser.ADOFAILevel.Actions.Count;
            this.CountDecorations = this.CustomLevelParser.ADOFAILevel.Decorations.Count;
        }
        [RelayCommand]
        private void SaveAs()
        {
            SaveAsProjectView view = new();
            view.DataContext = new SaveAsProjectViewModel()
            {
                PathToADODesignerFile = this.PathToProject,
                PathToADOFAILevel = this.ADODesignerFile.PathToADOFAILevel,
            };
            view.ShowDialog();
        }
        [RelayCommand]
        private void ImportFromAdofai()
        {
            ImportFromAdofaiView view = new();
            ImportFromAdofaiViewModel vm = new()
            {
                CustomLevel = this.CustomLevelParser,
                TimeLineElements = this.TimeLineElements,
            };
            view.DataContext = vm;
            view.ShowDialog();
        }
        [RelayCommand]
        public void UpdatePropertiesWindow()
        {
            if (this.GetCountSelectedTLElements() == 1)
            {
                foreach (var ep in this.TimeLineElements)
                {
                    if (ep.IsSelected)
                    {
                        StackPanel panel = Helpers.PropertiesWindow.FromTimeLineElement(ep);
                        Grid.SetRow(panel, 1);
                        this.PropertiesWindow = panel;
                    }
                }
            }
            else this.PropertiesWindow = null;
        }
        [RelayCommand]
        public void AddKeyFrame()
        {
            UnselectAllTimeLineElements();
            KeyFrameTimeLine element = new();
            element.Floor = GetCursorFloor();
            element.NumberTimeLine = GetCursorTimeLineCursor();
            this.TimeLineElements.Add(element);
            element.Select();
        }
        [RelayCommand]
        public void AddBallsAnimation()
        {
            UnselectAllTimeLineElements();
            BallsAnimationTimeLine element = new();
            element.Floor = GetCursorFloor();
            element.NumberTimeLine = GetCursorTimeLineCursor();
            this.TimeLineElements.Add(element);
            element.Select();  
        }
        [RelayCommand]
        public void AddFrameToFrameAnimation()
        {
            UnselectAllTimeLineElements();
            FrameToFrameTimeLine element = new();
            element.Floor = GetCursorFloor();
            element.NumberTimeLine = GetCursorTimeLineCursor();
            this.TimeLineElements.Add(element);
            element.Select();
        }
        #region TimeLineElements Commands
        [RelayCommand]
        public void TimeLineLeftButtonClicked()
        {
            UnselectAllTimeLineElements();
        }

        [RelayCommand]
        private void TimeLineSClicked()
        {
            foreach (var e in TimeLineElements)
            {
                if (e.IsSelected && e.NumberTimeLine < 31) e.NumberTimeLine++;
            }
        }
        [RelayCommand]
        private void TimeLineWClicked()
        {
            foreach (var e in TimeLineElements)
            {
                if (e.IsSelected && e.NumberTimeLine > 0) e.NumberTimeLine--;
            }
        }
        [RelayCommand]
        private void TimeLineAClicked()
        {
            foreach (var e in TimeLineElements)
            {
                if (e.IsSelected && e.Floor > 0) e.Floor--; 
            }
        }
        [RelayCommand]
        private void TimeLineDClicked()
        {
            foreach (var e in TimeLineElements)
            {
                if (e.IsSelected && e.Floor < this.CustomLevelParser.ADOFAILevel.AngleData.Count - 1) e.Floor++;
            }
        }
        [RelayCommand]
        private void TimeLineQClicked()
        {
            foreach (var e in TimeLineElements)
            {
                if (e.IsSelected && e.Duration > 0 && e.IsSupportDuration)
                {
                    e.Duration--;
                }
            }
            this.UpdatePropertiesWindow();
        }
        [RelayCommand]
        private void TimeLineEClicked()
        {
            foreach (var e in TimeLineElements)
            {
                if (e.IsSelected && e.IsSupportDuration)
                {
                    e.Duration++;
                }
            }
            this.UpdatePropertiesWindow();
        }
        [RelayCommand]
        private void TimeLineDublicate()
        {
            List<TimeLineElementModel> dublicate = new();
            foreach (var e in TimeLineElements.ToArray())
            {
                if (e.IsSelected)
                {
                    TimeLineElementModel model = e.CloneElement();
                    this.TimeLineElements.Add(model);
                    dublicate.Add(model);
                    e.Unselect();
                }
            }
            foreach (var e in dublicate)
            {
                e.Select();
            }
        }
        [RelayCommand]
        private void TimeLineDeleteClicked()
        {
            foreach (var e in this.TimeLineElements.ToArray())
            {
                if (e.IsSelected)
                {
                    this.TimeLineElements.Remove(e);
                }
            }
            this.UpdatePropertiesWindow();
        }
        [RelayCommand]
        private void TimeLineSelectAll()
        {
            Parallel.ForEach(this.TimeLineElements, (e) =>
            {
                if (!e.IsSelected) e.Select();
            });
        }
        [RelayCommand]
        private void CopyTimeLineElements()
        {
            List<TimeLineElementModel> copied = new();
            foreach (var e in this.TimeLineElements.ToArray())
            {
                if (e.IsSelected)
                {
                    copied.Add(e.CloneElement());
                }
            }
            this.CopiedTimeLineElements = copied.ToArray();
        }
        [RelayCommand]
        private void CutTimeLineElements()
        {
            List<TimeLineElementModel> copied = new();
            foreach (var e in this.TimeLineElements.ToArray())
            {
                if (e.IsSelected)
                {
                    copied.Add(e.CloneElement());
                    this.TimeLineElements.Remove(e);
                }
            }
            this.CopiedTimeLineElements = copied.ToArray(); 
        }
        [RelayCommand]
        private void PasteTimeLineElements()
        {
            if (this.CopiedTimeLineElements is null) return;
            int minFloor = 0;
            foreach (var e in this.CopiedTimeLineElements)
            {
                if (e.Floor > minFloor) minFloor = e.Floor;
            }
            UnselectAllTimeLineElements();
            foreach (var e in this.CopiedTimeLineElements)
            {
                e.Floor -= minFloor;
                e.Floor += GetCursorFloor();
                TimeLineElementModel clone = e.CloneElement();
                this.TimeLineElements.Add(clone);
                clone.Select();
            }
        }
        public void UnselectAllTimeLineElements()
        {
            foreach (var e in this.TimeLineElements)
            {
                 if (e.IsSelected) e.UnselectWithoutUpdateProperties();
            }
            this.UpdatePropertiesWindow();
        }
        public bool IsTLElementsSameType<T>() where T : TimeLineElementModel
        {
            if (this.TimeLineElements.Count == 0) return false;
            foreach (var e in this.TimeLineElements)
            {
                if (e.IsSelected && e is not T) return false;
            }
            return true;
        }
        public int GetCountSelectedTLElements()
        {
            int count = 0;
            Parallel.ForEach(this.TimeLineElements, (e) =>
            {
                if (e.IsSelected) count++;
            });
            return count;
        }
        public int GetCursorFloor()
        {
            return (int)this.TimeLineCanvasXCursor / 100;
        }
        public int GetCursorTimeLineCursor()
        {
            if (this.TimeLineUseAutoTimeLine) return (int)(this.TimeLineCanvasYCursor - 30) / 40;
            else return 0;
        }
        #endregion
    }
}
