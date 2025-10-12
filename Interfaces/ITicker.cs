namespace Clock
{
    interface ITicker
    {
        double Time { get; }
        int Interval { get; set; }
        bool IsRunning { get; }

        string GetTimeString();

        void Start();
        void Stop();
        void Reset();
    }
}
