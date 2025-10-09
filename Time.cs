namespace Clock
{
    public abstract class Time
    {
        protected System.Timers.Timer timer;
        protected double secondCounter = 0.0f;
        protected double intervalMilliseconds;
        protected bool isRunning = false;

        public Time(double intervalMilliseconds = 100.0f)
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
            return secondCounter;
        }

        public void ResetTime()
        {
            secondCounter = 0.0f;
        }

        public void setSsecondCounter(double value)
        {
            secondCounter = value;
        }

        public void DrawTimer()
        {
            Console.Clear();
            Console.WriteLine("Time: " + secondCounter + " seconds");
        }
    }
}
