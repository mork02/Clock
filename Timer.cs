namespace Clock
{
    public class Timer : Time
    {
        public Timer(double intervalMilliseconds = 100.0f) : base(intervalMilliseconds)
        {
            timer.Elapsed += OnElapsed;
        }

        private void OnElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            secondCounter -= intervalMilliseconds / 1000;
            secondCounter = Math.Round(secondCounter, 2);

            DrawTimer();    
        }

    }
}
