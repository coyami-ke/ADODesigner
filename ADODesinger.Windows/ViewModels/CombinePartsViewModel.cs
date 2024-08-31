using ADODesigner.Converters;
using ADODesigner.Converters.Events;
using ADODesigner.Windows.Models;
using ADODesigner.Windows.Views;
using ADODesinger.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows;

namespace ADODesigner.Windows.ViewModels
{
    public partial class CombinePartsViewModel : ObservableObject
    {
        [ObservableProperty]
        private string pathToChart = "";
        [ObservableProperty]
        private ObservableCollection<string> parts = new();
        [ObservableProperty]
        private string? selectedPart;

        [RelayCommand]
        private void RemovePart()
        {
            if (SelectedPart is not null) Parts.Remove(SelectedPart);
        }
        [RelayCommand]
        private void OpenChart()
        {
            CommonOpenFileDialog dialog = new();
            dialog.Filters.Add(new CommonFileDialogFilter("ADOFAI Level", ".adofai"));
            dialog.Title = "Select ADOFAI Level";
            Window? window = App.Current.Windows.OfType<CombinePartsView>().SingleOrDefault(w => w.IsActive);
            if (dialog.ShowDialog(window) == CommonFileDialogResult.Ok)
            {
                PathToChart = dialog.FileName;
            }
        }
        [RelayCommand]
        private void AddPart()
        {
            CommonOpenFileDialog dialog = new();
            dialog.Filters.Add(new CommonFileDialogFilter("ADOFAI Level", ".adofai"));
            dialog.Title = "Select ADOFAI Level";
            Window? window = App.Current.Windows.OfType<CombinePartsView>().SingleOrDefault(w => w.IsActive);
            if (dialog.ShowDialog(window) == CommonFileDialogResult.Ok)
            {
                Parts.Add(dialog.FileName);
            }
            var noDublicate = Parts.Distinct().ToArray();
            this.Parts = new(noDublicate);
        }
        [RelayCommand]
        private void CombineParts()
        {
            if (string.IsNullOrEmpty(PathToChart))
            {
                CustomMessageBox.Show("Path to the chart is empty.", "Combine Parts", CustomMessageBoxButton.Ok);
                return;
            }

            CustomLevelParser chart = new();

            chart.Parse(File.ReadAllText(PathToChart));

            try
            {
                foreach (var part in Parts)
                {
                    CustomLevelParser parser = new();
                    parser.Parse(File.ReadAllText(part)); // Gets the CustomLevelParser of the part

                    foreach (var action in parser.ADOFAILevel.Actions.ToArray()) // Gets all ACTIONS 
                    {
                        if (action is null) continue;
                        JsonObject? obj = JsonNode.Parse(action.ToJsonString(CustomLevelParser.GetDefaultJsonOptions()))?.AsObject();
                        if (obj is null) continue;

                        if (obj.TryGetPropertyValue("eventType", out var node) && node is not null)
                        {
                            string type = node.ToString();
                            bool isGameplayEvent = TypesADOFAIEvents.GameplayEvents.Contains(type);

                            if (isGameplayEvent)
                            {
                                parser.ADOFAILevel.Actions.Remove(action);
                            }
                            else
                            {
                                chart.ADOFAILevel.Actions.Add(JsonNode.Parse(action.ToJsonString()));
                            }
                        }
                    }

                    foreach (var decoration in chart.ADOFAILevel.Decorations.ToArray())
                    {
                        if (decoration is not null) chart.ADOFAILevel.Decorations.Add(JsonNode.Parse(decoration.ToJsonString()));
                    }
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(ex.Message, "Combine Parts", CustomMessageBoxButton.Ok);
                return;
            }
            string newChart = Path.GetFileName(PathToChart);
            newChart = PathToChart.Replace(newChart, "");
            newChart = Path.Combine(newChart, "combined.adofai");
            File.WriteAllText(newChart, chart.ToJson());
            CustomMessageBox.Show("Complete!", "Combine Parts", CustomMessageBoxButton.Ok);
        }
    }
}
