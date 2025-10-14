using System.Windows.Threading;

namespace Clock
{
    public class CountdownTicker : ITicker
    {
        public readonly DispatcherTimer timer;
        private DateTime startTime;
        private double elapsedMs;
        private bool isRunning = false;

        public event EventHandler? Tick;

        public double Time => elapsedMs + (isRunning ? (DateTime.Now - startTime).TotalMilliseconds : 0);


        public int Interval
        {
            get => (int)timer.Interval.TotalMilliseconds;
            set => timer.Interval = TimeSpan.FromMilliseconds(value);
        }
        public bool IsRunning => isRunning;

        public CountdownTicker(int startTimer = 1000) // default 1 sec
        {
            this.startTime = DateTime.Now.AddMilliseconds(startTimer);
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(50)
            };
            timer.Tick += (_, __) => Tick?.Invoke(this, EventArgs.Empty);
        }

        public string GetTimeString()
        {
            TimeSpan t = TimeSpan.FromMilliseconds(Time);
            return t.Days > 0
                ? $"{t:dd\\.hh\\:mm\\:ss\\.ff}"
                : t.Hours > 0
                    ? $"{t:hh\\:mm\\:ss\\.ff}"
                    : $"{t:mm\\:ss\\.ff}";
        }

        public void Start()
        {
            if (isRunning) return;
            timer.Start();
            isRunning = true;
        }

        public void Stop()
        {
            if (!isRunning) return;
            if (Time >= 0) Reset();
            elapsedMs += (DateTime.Now - startTime).TotalMilliseconds;
            timer.Stop();
            isRunning = false;
        }

        public void Reset()
        {
            elapsedMs = 0;
            startTime = DateTime.Now;
        }
    }
}
