using System;
using System.Threading;

namespace South_Park
{
    static class Program
    {
        static void Main(string[] args)
        {
            bool only;
            Mutex mutex = new Mutex(true, "South Park", out only);
            mutex.Dispose();

            if (only)
            {
                using (SPGame game = new SPGame())
                {
                    game.Run();
                }
            }
            
        }
    }
}

