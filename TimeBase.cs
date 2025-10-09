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

        public void ResetTime()
        {
            millisecondsTime = 0.0f;
        }

        public void setMillisecondsTime(double value)
        {
            millisecondsTime = value;
        }

        public void DrawTimer(TimeSpan time)
        {
            Console.Clear();
            Console.WriteLine($"Time: {time:hh\\:mm\\:ss\\.ff} seconds");
        }
    }
}
