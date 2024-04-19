using ADODesinger.Windows.ViewModels;
using System;
using System.Collections.Generic;
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

namespace ADODesinger.Windows.Views
{
    /// <summary>
    /// Interaction logic for EditorView.xaml
    /// </summary>
    public partial class EditorView : Window
    {
        public EditorView()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight - 10;
        }

        private void TimeLineCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            EditorViewModel model = (EditorViewModel)this.DataContext;
            Canvas canvas = (Canvas)sender;
            model.TimeLineCanvasXCursor = e.GetPosition(canvas).X;
            model.TimeLineCanvasYCursor = e.GetPosition(canvas).Y;
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var scrollViewer = (ScrollViewer)sender;

            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            {
                scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - e.Delta);
                e.Handled = true;
            }
        }

        private void BorderCloseWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }

        private void BorderMaxWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Normal) WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }

        private void BorderMinWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void BorderMoveWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
