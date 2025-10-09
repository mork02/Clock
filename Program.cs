namespace Clock
{

enum Command { Timer, StopTimer, Quit };

    internal class Program
    {
        private static Timer timer;
        private static Stopwatch stopwatch;

        private static ConsoleKey key = ConsoleKey.None;

        static Dictionary<Command, ConsoleKey> KeyBindings = new()
        {
            { Command.Timer, ConsoleKey.T },
            { Command.StopTimer, ConsoleKey.S },
            { Command.Quit, ConsoleKey.Q }
        };

        static void Main(string[] args)
        {
            do
            {
                key = Console.ReadKey(intercept: true).Key;
                if (key == KeyBindings[Command.Timer])
                {
                    // Start Timer
                    if (timer == null)
                        timer = new Timer();
                    
                    timer.StartTimer();
                }
                else if (key == KeyBindings[Command.StopTimer])
                {
                    // Start Stopwatch
                    if (stopwatch == null)
                        stopwatch = new Stopwatch();

                    stopwatch.StartTimer();
                }

            } while (key != KeyBindings[Command.Quit]);


        }
    }
}
