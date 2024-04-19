using ADODesigner.Converters;
using ADODesigner.Models;
using ADODesinger.Windows.Models;
using ADODesinger.Windows.Models.TimeLine;
using ADODesinger.Windows.ViewModels.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace ADODesinger.Windows.ViewModels
{
    public partial class ImportFromAdofaiViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool removeEventsFromAdofai;
        [ObservableProperty]
        private bool importMoveDecoration = true;
        [ObservableProperty]
        private CustomLevelParser customLevel = new();
        [ObservableProperty]
        private IList<TimeLineElementModel> timeLineElements = new ObservableCollection<TimeLineElementModel>();
        [ObservableProperty]
        private int firstFloor = 1;
        [ObservableProperty]
        private int secondFloor = 1;
        [RelayCommand]
        private void Import()
        {
            foreach (var a in CustomLevel.ADOFAILevel.Actions)
            {
                if (a is not null)
                {
                    string? eventType = (string?)a["eventType"];
                    if (eventType is not null)
                    {
                        if (eventType == "MoveDecorations")
                        {
                            int? floor = (int?)a["floor"];
                            if (floor is not null)
                            {
                                if (floor <= this.FirstFloor && floor >= this.SecondFloor)
                                {
                                    MoveDecorations? moveDecorations;
                                    try
                                    {
                                        moveDecorations = a.Deserialize<MoveDecorations>(CustomLevel.SerializerOptions);
                                    }
                                    catch { continue; }
                                    if (moveDecorations is not null)
                                    {
                                        KeyFrame keyFrame = KeyFrameConverter.Convert(moveDecorations);
                                        KeyFrameTimeLine element = new();
                                        element.KeyFrame = keyFrame;
                                        element.Duration = element.KeyFrame.Duration;
                                        element.Floor = element.Floor;
                                        TimeLineElements.Add(element);
                                        if (RemoveEventsFromAdofai) CustomLevel.ADOFAILevel.Actions.Remove(a);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            WeakReferenceMessenger.Default.Send<ImportFromAdofaiMessage>(new(this));
        }
    }
}
