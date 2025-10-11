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
    public partial class StopwatchPage : Page
    {
        private StopwatchTimer stopwatchTimer;
        private bool isRunning = false;

        public StopwatchPage()
        {
            InitializeComponent();
            stopwatchTimer = new StopwatchTimer();
        }

        public void OnStartStopClick(object sender, RoutedEventArgs e)
        {
            isRunning = !isRunning;

            if (isRunning)
            {
                StartStopButton.Content = "Stop";
                StartStopButton.Background = Brushes.Red;
                SyncTimer();
                stopwatchTimer.StartTimer();
            }
            else
            {
                StartStopButton.Content = "Start";
                StartStopButton.Background = Brushes.Green;

                stopwatchTimer.StopTimer();
            }
        }

        public void OnResetButtonClick(object sender, RoutedEventArgs e)
        {
            stopwatchTimer.ResetTime();
            TimeLabel.Content = "00:00.00";
        }

        private async void SyncTimer()
        {
            while (isRunning)
            {
                await Task.Delay(50);
                TimeLabel.Content = stopwatchTimer.GetTimeString();
            }
        }

        public void OnBackToHomeClick(object sender, RoutedEventArgs e)
            => NavigationService?.GoBack();
    }
}
