namespace Clock
{
    public class CountdownTimer : TimeBase
    {
        public CountdownTimer() : base()
        {
            timer.Elapsed += OnElapsed;
        }

        private void OnElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            millisecondsTime -= intervalMilliseconds;
            millisecondsTime = Math.Round(millisecondsTime, 2);
            if (millisecondsTime < 0)
            {
                millisecondsTime = 0;
                StopTimer();
            }
        }

    }
}
