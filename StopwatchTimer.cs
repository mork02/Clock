namespace Clock
{
    public class StopwatchTimer : TimeBase
    {
        public StopwatchTimer() : base()
        {
            timer.Elapsed += OnElapsed;
        }

        private void OnElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            millisecondsTime += intervalMilliseconds;
            millisecondsTime = Math.Round(millisecondsTime, 2);
        }
    }
}
