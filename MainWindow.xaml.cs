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

namespace Clock
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private IEnumerable<UIElement> GetAllChildren(DependencyObject parent)
        {
            if (parent == null) yield break;

            foreach (var child in LogicalTreeHelper.GetChildren(parent))
            {
                if (child is DependencyObject dep)
                {
                    yield return (UIElement)dep;

                    foreach (var grandChild in GetAllChildren(dep))
                        yield return grandChild;
                }
            }
        }

        private void HideObjects()
        {
            foreach (var element in GetAllChildren(this))
            {
                element.Visibility = Visibility.Collapsed;
            }
        }

        public void OnStopwatchClick(object sender, RoutedEventArgs e)
        {
            HideObjects();
            this.Stopwatch.Visibility = Visibility.Visible;
        }

        public void OnCountdownClick(object sender, RoutedEventArgs e)
        {
            HideObjects();
            this.Countdown.Visibility = Visibility.Visible;
        }
    }
}
