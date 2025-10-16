using System.Windows;
using System.Windows.Controls;

namespace Clock.WPF
{
    public partial class CountdownPage : Page
    {
        private readonly List<CountdownTicker> countdownTickers = new();
        private int entryCount = 0;
        private const int MaxEntries = 5;

        private int initialTimeMs = 5 * 60 * 1000;


        public CountdownPage()
        {
            InitializeComponent();
            UpdatePreview();
        }

        public void OnCreateButton(object sender, RoutedEventArgs e)
        {
            if (entryCount >= MaxEntries)
            {
                MessageBox.Show($"Maximale Timer ({entryCount}/{MaxEntries}) erreicht.");
                return;
            }

            CountdownTicker ticker = new CountdownTicker(initialTimeMs);
            countdownTickers.Add(ticker);
            entryCount++;

            string titleText = InputTitle.Text.Trim();

            var row = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(4)
            };

            if (!string.IsNullOrEmpty(titleText))
            {
                var titleBlock = new TextBlock
                {
                    Text = $"{InputTitle.Text}",
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(2, 4, 2, 0)
                };

                row.Children.Add(titleBlock);
            }
            InputTitle.Text = string.Empty;

            var line = new StackPanel { Orientation = Orientation.Horizontal };

            var display = new TextBox
            {
                Text = $"{ticker.GetTimeString()}",
                Margin = new Thickness(2),
                HorizontalAlignment = HorizontalAlignment.Center,
                IsReadOnly = true,
                Width = 100
            };

            var btnRemove = new Button
            {
                Content = "❌",
                Margin = new Thickness(2)
            };

            btnRemove.Tag = (ticker: ticker, row: row);

            btnRemove.Click += OnDeleteClick;

            line.Children.Add(display);
            line.Children.Add(btnRemove);
            row.Children.Add(line);

            TimerContainer.Children.Add(row);

            ticker.Tick += (_, __) =>
            {

                if (ticker.Time >= 0)
                {
                    ticker.Stop();
                
                    display.Dispatcher.Invoke(() => display.Text = $"{ticker.GetTimeString()}");

                    var result = MessageBox.Show(
                        $"{(string.IsNullOrEmpty(titleText) ? "Countdown" : titleText)}! Willst du diesen löschen?",
                        "Countdown Finished",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Information
                    );
                    if (result == MessageBoxResult.Yes)
                    {
                        countdownTickers.Remove(ticker);
                        TimerContainer.Children.Remove(row);
                        entryCount--;
                    }
                }

                display.Dispatcher.Invoke(() => display.Text = $"{ticker.GetTimeString()}");

            };

            ticker.Start();
        }

        private void UpdatePreview()
        {
            if (initialTimeMs < 0) initialTimeMs = 0;

            var ts = TimeSpan.FromMilliseconds(initialTimeMs);


            HoursText.Text = ts.Hours.ToString("00");
            MinutesText.Text = ts.Minutes.ToString("00");
            SecondsText.Text = ts.Seconds.ToString("00");
        }

        private void AddSeconds(int deltaSeconds)
        {
            int next = initialTimeMs + deltaSeconds * 1000;
            if (next < 0) next = 0;
            if (next > int.MaxValue) next = int.MaxValue;

            initialTimeMs = next;
            if (next >= 24 * 3600 * 1000)
                initialTimeMs = 24 * 3600 * 1000 - 1;

            UpdatePreview();
        }

        private void OnPlusHour(object s, RoutedEventArgs e) => AddSeconds(3600);
        private void OnMinusHour(object s, RoutedEventArgs e) => AddSeconds(-3600);
        private void OnPlusMinute(object s, RoutedEventArgs e) => AddSeconds(60);
        private void OnMinusMinute(object s, RoutedEventArgs e) => AddSeconds(-60);
        private void OnPlusSecond(object s, RoutedEventArgs e) => AddSeconds(1);
        private void OnMinusSecond(object s, RoutedEventArgs e) => AddSeconds(-1);

        public void OnDeleteClick(object sender, RoutedEventArgs e)
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
