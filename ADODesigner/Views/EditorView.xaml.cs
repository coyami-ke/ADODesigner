using ADODesigner.Animations;
using ADODesigner.Converters;
using ADODesigner.Models;
using ADODesigner.ViewModels;
using ADODesigner.Views.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.VisualBasic;
using ADODesigner.Core.API;
#nullable disable
namespace ADODesigner.Views
{
    /// <summary>
    /// Main view editor in ADODesigner.
    /// </summary>
    public partial class EditorView : Window
    {
        /// <summary>
        /// The initialized ViewModel for the editor.
        /// </summary>
        public static EditorVM Editor { get; set; }
        public EditorView()
        {
            Editor = new();
            Editor.PropertyChanged += Editor_PropertyChanged;
            DataContextChanged += EditorView_DataContextChanged;
            InitializeComponent();
            panelTimeLines.Children.Add(new TimeLine());
        }
        #region Events

        private void EditorView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Editor = (EditorVM)DataContext;
        }

        private void Editor_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            DataContext = Editor;
        }
        #endregion
    }
}
