using Accessibility;
using ADODesigner.Animations;
using ADODesigner.Converters;
using ADODesigner.Converters.Events;
using ADODesigner.Windows.Helpers;
using ADODesigner.Windows.Models;
using ADODesigner.Windows.Models.TimeLine;
using ADODesigner.Windows.ViewModels;
using ADODesigner.Windows.ViewModels.Messages;
using ADODesigner.Windows.Views;
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
using System.Diagnostics;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Xml.Linq;
using Windows.System;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

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
        private ObservableCollection<BookmarkTimeLine> tabNavigationEvents = new();
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

        public CustomLevelParser CustomLevelParser { get; set; } = new();

        [ObservableProperty]
        private EditorNavigationModel? editorNavigation;

        [ObservableProperty]
        private bool onSaved;
        [ObservableProperty]
        private bool isTimeLineChanged;

        public event Action? TimeLineElementsChanged;

        public Canvas? TimeLineCanvas { get; set; }

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
            this.TimeLineElementsChanged += () =>
            {
                this.IsTimeLineChanged = true;
            };

            WeakReferenceMessenger.Default.Register<SaveAsProjectMessage>(this, (sender, e) =>
            {
                string oldAdofai = this.ADODesignerFile.PathToADOFAILevel;
                this.ADODesignerFile.PathToADOFAILevel = e.Value.PathToADOFAILevel;
                File.WriteAllText(e.Value.PathToADODesignerFile, this.ADODesignerFile.ToJson());
                File.WriteAllText(e.Value.PathToADOFAILevel, this.CustomLevelParser.ToJson());
                this.ADODesignerFile.PathToADOFAILevel = oldAdofai;
            });
            WeakReferenceMessenger.Default.Register<TimeLineElementChangedMessage>(this, (sender, e) =>
            {
                this.TimeLineElementsChanged?.Invoke();
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

            VisualTimeLineModel firstModel = new();
            firstModel.Margin = new(0);
            firstModel.Height = 30;
            firstModel.Width = CustomLevelParser.ADOFAILevel.AngleData.Count * 100 + 1600;
            firstModel.BorderBrush = Brushes.Transparent;
            VisualTimeLineModels.Add(firstModel);
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

            this.TabNavigationEvents = new();
            foreach (var a in CustomLevelParser.ADOFAILevel.Actions)
            {
                JsonObject? obj = a?.AsObject();
                if (obj is not null && obj.TryGetPropertyValue("eventType", out JsonNode? node) && node is not null)
                {
                    string? type = (string?)node;
                    if (type is not null && type == "Bookmark")
                    {
                        Bookmark? mark = obj.Deserialize<Bookmark>(ADODesignerFile.GetDefaultOptions());
                        if (mark is not null)
                        {
                            this.TabNavigationEvents.Add(new() { Floor = mark.Floor, });
                        }
                    }
                }
            }
            this.EditorNavigation = new(this.CustomLevelParser.ADOFAILevel);
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
                foreach (var a in this.CustomLevelParser.ADOFAILevel.Decorations.ToArray())
                {
                    JsonObject? obj = a?.AsObject();
                    if (RemoverEventTag.ContainsTag("ADODesigner_Event", new string[] { "tag" }, obj))
                    {
                        this.CustomLevelParser.ADOFAILevel.Decorations.Remove(a);
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
                        if (action is not null && action.ContainsKey("tag"))
                        {
                            action["tag"] += " ADODesigner_Event";
                        }
                        this.CustomLevelParser.ADOFAILevel.Decorations.Add(action);
                    }
                }
            }

            File.WriteAllText(this.ADODesignerFile.PathToADOFAILevel, this.CustomLevelParser.ToJson());
            // Update the count of actions
            this.CountActions = this.CustomLevelParser.ADOFAILevel.Actions.Count;
            this.CountDecorations = this.CustomLevelParser.ADOFAILevel.Decorations.Count;

            OnSaved = true;
            OnSaved = false;

            this.IsTimeLineChanged = false;

            GC.Collect();
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
        public void TimeLineElementInit(TimeLineElementModel element)
        {
            element.Floor = GetCursorFloor();
            element.NumberTimeLine = GetCursorTimeLineCursor();
            this.TimeLineElements.Add(element);
            element.Select();
            this.TimeLineElementsChanged?.Invoke();
        }
        [RelayCommand]
        public void AddKeyFrame()
        {
            UnselectAllTimeLineElements();
            KeyFrameTimeLine element = new();
            TimeLineElementInit(element);
        }
        [RelayCommand]
        public void AddBallsAnimation()
        {
            UnselectAllTimeLineElements();
            BallsAnimationTimeLine element = new();
            TimeLineElementInit(element);
        }
        [RelayCommand]
        public void AddFrameToFrameAnimation()
        {
            UnselectAllTimeLineElements();
            FrameToFrameTimeLine element = new();
            TimeLineElementInit(element);
        }
        [RelayCommand]
        public void AddCubeObject()
        {
            UnselectAllTimeLineElements();
            CubeObjectTimeLine element = new();
            TimeLineElementInit(element);
        }
        #region TimeLineElements Commands
        [ObservableProperty]
        private SelectionRectangleModel selectionBox = new();
        [RelayCommand]
        private void TimeLineMouseDown(object obj)
        {
            MouseButtonEventArgs e = (MouseButtonEventArgs)obj;

            if (e.LeftButton == MouseButtonState.Pressed) this.UnselectAllTimeLineElements();
            else if (e.MiddleButton == MouseButtonState.Pressed)
            {
                UnselectAllTimeLineElements();

                SelectionBox.IsMouseDown = true;
                SelectionBox.MouseDownPosition = e.GetPosition((IInputElement)e.Source);

                SelectionBox.CanvasLeft = SelectionBox.MouseDownPosition.X;
                SelectionBox.CanvasTop = SelectionBox.MouseDownPosition.Y;
                SelectionBox.Width = 0;
                SelectionBox.Height = 0;

                SelectionBox.Visibility = Visibility.Visible;
            }
        }

        [RelayCommand]
        private void TimeLineMouseMove(object obj)
        {
            if (!SelectionBox.IsMouseDown)
                return;

            MouseEventArgs e = (MouseEventArgs)obj;

            var sender = e.OriginalSource as FrameworkElement;

            Canvas? canvas = this.TimeLineCanvas;

            if (canvas == null)
            {
                return;
            }

            Point currentMousePos = e.GetPosition(canvas);

            double width = Math.Abs(currentMousePos.X - SelectionBox.MouseDownPosition.X);
            double height = Math.Abs(currentMousePos.Y - SelectionBox.MouseDownPosition.Y);

            SelectionBox.Width = width;
            SelectionBox.Height = height;

            SelectionBox.CanvasLeft = Math.Min(currentMousePos.X, SelectionBox.MouseDownPosition.X);
            SelectionBox.CanvasTop = Math.Min(currentMousePos.Y, SelectionBox.MouseDownPosition.Y);
        }

        [RelayCommand]
        private void TimeLineMouseUp(object obj)
        {
            if (!SelectionBox.IsMouseDown) return;

            UnselectAllTimeLineElements();

            MouseEventArgs e = (MouseEventArgs)obj;

            SelectionBox.IsMouseDown = false;

            SelectionBox.Visibility = Visibility.Hidden;

            var sender = e.OriginalSource as FrameworkElement;

            Canvas? canvas = this.TimeLineCanvas;
            if (canvas is null) return;

            Point mouseUpPos = e.GetPosition(canvas); 

            double startX = SelectionBox.MouseDownPosition.X;
            double startY = SelectionBox.MouseDownPosition.Y;
            double endX = mouseUpPos.X;
            double endY = mouseUpPos.Y;

            if (startX > endX)
            {
                double temp = startX;
                startX = endX;
                endX = temp;
            }
            if (startY > endY)
            {
                double temp = startY;
                startY = endY;
                endY = temp;
            }

            double selectionStartFloor = startX / 100.0;
            double selectionEndFloor = endX / 100.0;
            double selectionStartTimeline = (startY - 30) / 40.0;
            double selectionEndTimeline = (endY - 30) / 40.0;

            foreach (var ec in TimeLineElements)
            {
                double elementStartFloor = ec.Floor;
                double elementEndFloor = ec.Floor + ec.Duration;
                double elementTimeLine = ec.NumberTimeLine;
                double elementEndTimeLine = ec.NumberTimeLine + 1;

                bool isIntersecting = elementEndFloor > selectionStartFloor && elementStartFloor < selectionEndFloor &&
                                      elementEndTimeLine > selectionStartTimeline && elementTimeLine < selectionEndTimeline;

                if (isIntersecting)
                {
                    ec.Select();
                }
            }
            SelectionBox = new();
        }
        [RelayCommand]
        private void TimeLineWClicked()
        {
            if (this.GetCountSelectedTLElements() > 0) 
            foreach (var e in TimeLineElements)
            {
                if (e.IsSelected && e.NumberTimeLine > 0)
                {
                    e.NumberTimeLine--;
                }
            }
            this.TimeLineElementsChangedInvoke();
        }
        [RelayCommand]
        private void TimeLineSClicked()
        {
            if (this.GetCountSelectedTLElements() > 0)
            foreach (var e in TimeLineElements)
            {
                if (e.IsSelected && e.NumberTimeLine < 32)
                {
                    e.NumberTimeLine++;
                }
            }
            this.TimeLineElementsChangedInvoke();
        }
        [RelayCommand]
        private void TimeLineAClicked()
        {
            if (this.GetCountSelectedTLElements() > 0) 
            foreach (var e in TimeLineElements)
            {
                if (e.IsSelected && e.Floor > 0)
                {
                    e.Floor--;
                }
            }
            this.TimeLineElementsChangedInvoke();
        }
        [RelayCommand]
        private void TimeLineDClicked()
        {
            if (this.GetCountSelectedTLElements() > 0) 
            foreach (var e in TimeLineElements)
            {
                if (e.IsSelected && e.Floor < this.CustomLevelParser.ADOFAILevel.AngleData.Count - 1)
                {
                    e.Floor++;
                }
            }
            this.TimeLineElementsChangedInvoke();
        }
        [RelayCommand]
        private void TimeLineQClicked()
        {
            if (this.GetCountSelectedTLElements() > 0)
            {
                int count = 0;
                foreach (var e in TimeLineElements)
                {
                    if (e.IsSelected && e.Duration > 0 && e.IsSupportDuration)
                    {
                        e.Duration--;
                        count++;
                    }
                }
                if (count > 0) this.TimeLineElementsChangedInvoke();
            }
            this.UpdatePropertiesWindow();
        }
        [RelayCommand]
        private void TimeLineEClicked()
        {
            if (this.GetCountSelectedTLElements() > 0)
            {
                int count = 0;
                foreach (var e in TimeLineElements)
                {
                    if (e.IsSelected && e.IsSupportDuration)
                    {
                        e.Duration++;
                        count++;
                    }
                }
                if (count > 0) this.TimeLineElementsChangedInvoke();
            }
            this.UpdatePropertiesWindow();
        }
        [RelayCommand]
        private void TimeLineDublicate()
        {
            List<TimeLineElementModel> dublicate = new();
            if (this.GetCountSelectedTLElements() > 0)
            {
                this.TimeLineElementsChangedInvoke();
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
            }
            foreach (var e in dublicate)
            {
                e.Select();
            }
        }
        [RelayCommand]
        private void TimeLineDeleteClicked()
        {
            if (this.GetCountSelectedTLElements() > 0)
            {
                this.TimeLineElementsChangedInvoke();
                foreach (var e in this.TimeLineElements.ToArray())
                {
                    if (e.IsSelected)
                    {
                        this.TimeLineElements.Remove(e);
                    }
                }
            }
            this.UpdatePropertiesWindow();
        }
        [RelayCommand]
        private void TimeLineSelectAll()
        {
            this.TimeLineElementsChangedInvoke();
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
            this.TimeLineElementsChangedInvoke();
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
            this.TimeLineElementsChangedInvoke();
        }
        [RelayCommand]
        private void ClosingWindow(CancelEventArgs e)
        {
            if (this.IsTimeLineChanged)
            {
                var result = CustomMessageBox.Show("Your level is not saved. Do you want to exit without saving?", "Level is not saved", CustomMessageBoxButton.YesNoCancel);
                if (result == CustomMessageBoxResult.Yes)
                {
                    e.Cancel = false;
                    Environment.Exit(0);
                }
                e.Cancel = true;
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
        public void TimeLineElementsChangedInvoke()
        {
            if (this.GetCountSelectedTLElements() > 0)
            {
                this.TimeLineElementsChanged?.Invoke();
            }
        }

        [RelayCommand]
        public void ShowCombineParts()
        {
            CombinePartsView view = new();
            view.ShowDialog();
        }
        [RelayCommand]
        public void ShowGuide()
        {
            GuideView view = new();
            view.Show();
        }
        public delegate void TimeLineElementMethod(TimeLineElementModel element);
        #endregion
    }
}
