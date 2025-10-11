using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Clock
{
    public abstract class TimeBase
    {
        protected System.Timers.Timer timer;
        protected double millisecondsTime = 0.0f;
        protected double intervalMilliseconds;
        protected bool isRunning = false;

        public TimeBase(double intervalMilliseconds = 50.0f)
        {
            this.intervalMilliseconds = intervalMilliseconds;
            
            timer = new System.Timers.Timer();
        }

        public void StartTimer()
        {
            timer.Interval = intervalMilliseconds;
            timer.AutoReset = true;
            timer.Start();
            isRunning = true;
        }

        public void StopTimer()
        {
            timer.Stop();
            isRunning = false;
        }

        public double GetTime()
        {
            return millisecondsTime;
        }

        public string GetTimeString()
        {
            TimeSpan time = TimeSpan.FromMilliseconds(millisecondsTime);
            return time.Days > 0
                ? $"{time:dd\\.hh\\:mm\\:ss\\.ff}"
                : time.Hours > 0
                    ? $"{time:hh\\:mm\\:ss\\.ff}"
                    : $"{time:mm\\:ss\\.ff}";
        }

        public void ResetTime()
        {
            millisecondsTime = 0.0f;
        }

        public void setMillisecondsTime(double value)
        {
            millisecondsTime = value;
        }
    }
}
