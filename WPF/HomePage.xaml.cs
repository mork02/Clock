using Clock.WPF;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Clock
{
    public partial class HomePage : Page
    {
        private readonly StopwatchPage stopwatchPage = new StopwatchPage();
        private readonly CountdownPage countdownPage = new CountdownPage();
        public HomePage()
        {
            InitializeComponent();
        }

        // Navigate to StopwatchPage when Stopwatch button is clicked
        public void OnStopwatchClick(object sender, RoutedEventArgs e)
            => NavigationService?.Navigate(stopwatchPage);

        // Navigate to Countdown when Countdown button is clicked
        public void OnCountdownClick(object sender, RoutedEventArgs e)
            => NavigationService?.Navigate(countdownPage);

    }
}
