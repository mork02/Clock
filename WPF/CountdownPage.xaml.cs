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

namespace Clock.WPF
{
    public partial class CountdownPage : Page
    {
        private readonly List<CountdownTicker> countdownTickers = new();
        private int entryCount = 0;
        private const int MaxEntries = 5;

        public CountdownPage()
        {
            InitializeComponent();
        }

        public void OnCreateButton(object sender, RoutedEventArgs e)
        {
            if (entryCount >= MaxEntries)
            {
                MessageBox.Show($"Maximum of {MaxEntries} entries reached.");
                return;
            }

            int initialTime = 3000;
            CountdownTicker ticker = new CountdownTicker(initialTime);
            countdownTickers.Add(ticker);
            entryCount++;

            var row = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(4)
            };

            if (InputDescription.Text.Length > 0)
            {
                row.Children.Add(new TextBlock
                {
                    Text = InputDescription.Text,
                    Margin = new Thickness(2)
                });
            }

            var line = new StackPanel { Orientation = Orientation.Horizontal };

            var display = new TextBox
            {
                Text = $"{ticker.GetTimeString()}",
                Margin = new Thickness(2),
                IsReadOnly = true,
                Width = 100
            };

            var btnRemove = new Button
            {
                Content = "X",
                Margin = new Thickness(2)
            };

            btnRemove.Tag = (ticker: ticker, row: row);

            btnRemove.Click += OnTestClick;

            line.Children.Add(display);
            line.Children.Add(btnRemove);
            row.Children.Add(line);

            TimerContainer.Children.Add(row);

            ticker.Tick += (_, __) =>
            {
                if (ticker.Time >= 0)
                {
                    ticker.Stop();
                }

                display.Dispatcher.Invoke(() => display.Text = $"{ticker.GetTimeString()}");
            };

            ticker.Start();
        }

        public void OnTestClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is ValueTuple<CountdownTicker, StackPanel> data)
            {
                var (ticker, row) = data;
                ticker.Stop();
                countdownTickers.Remove(ticker);
                TimerContainer.Children.Remove(row);
                entryCount--;
            }
        }

        public void OnBackToHomeClick(object sender, RoutedEventArgs e)
            => NavigationService?.GoBack();
    }
}
