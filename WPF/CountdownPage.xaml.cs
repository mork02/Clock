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
            entryCount++;

            int initialTime = 3000;

            CountdownTicker ticker = new CountdownTicker(initialTime);

            ticker.Start();

            ticker.Tick += (_, __) =>
            {
                if (ticker.Time >= 0)
                {
                    ticker.Stop();
                }

                TimerContainer.Children
                    .OfType<TextBox>()
                    .ElementAt(countdownTickers.IndexOf(ticker))
                    .Text = ticker.GetTimeString();

            };

            countdownTickers.Add(ticker);

            string text = $"{ticker.GetTimeString()}";

            createTextbox(TimerContainer, text);

        }

        private void createTextbox(Panel parent, string text)
        {
            TextBox textBox = new TextBox
            {
                Text = text,
                Margin = new Thickness(2),
                IsReadOnly = true
            };
            if (InputDescription.Text.Length > 1)
            {
                TextBlock descrription = new TextBlock
                {
                    Text = $"{InputDescription.Text}",
                    Margin = new Thickness(2),
                };
                parent.Children.Add(descrription);
            }
            parent.Children.Add(textBox);
        }

        public void OnBackToHomeClick(object sender, RoutedEventArgs e)
            => NavigationService?.GoBack();
    }
}
