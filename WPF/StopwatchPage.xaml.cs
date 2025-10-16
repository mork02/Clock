using Clock;
using Clock.WPF;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Clock
{
    public partial class StopwatchPage : Page
    {
        private readonly StopwatchTimer stopwatchTimer;

        public StopwatchPage()
        {
            InitializeComponent();

            stopwatchTimer = new StopwatchTimer();

            stopwatchTimer.Tick += (_, __) =>
            {
                if (stopwatchTimer.IsRunning) 
                    TimeLabel.Content = stopwatchTimer.GetTimeString();
            };
        }

        public void OnStartStopClick(object sender, RoutedEventArgs e)
        {
            if (!stopwatchTimer.IsRunning)
            {
                stopwatchTimer.Start();
                StartStopButton.Content = "Stop";
                StartStopButton.Background = Brushes.Red;
            }
            else
            {
                stopwatchTimer.Stop();
                StartStopButton.Content = "Start";
                StartStopButton.Background = Brushes.Green;
            }
        }

        public void OnResetButtonClick(object sender, RoutedEventArgs e)
        {
            stopwatchTimer.Reset();
            TimeLabel.Content = "00:00.00";
        }

        public void OnBackToHomeClick(object sender, RoutedEventArgs e)
            => NavigationService?.GoBack();
    }
}
