using System;

namespace Secret_Hipster
{
    public class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Game(800, 600))
            {
                // Run the game at 60 updates per second
                game.Run(60.0);
            }
        }
    }
}