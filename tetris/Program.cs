﻿
namespace tetris
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            game.Start();

            while ( ! game.GameOver)
            {
                // listen to key presses
                if (Console.KeyAvailable)
                {
                    var input = Console.ReadKey(true);

                    switch (input.Key)
                    {
                        // send key presses to the game if it's not paused
                        case ConsoleKey.X:
                        case ConsoleKey.Z:
                        case ConsoleKey.DownArrow:
                        case ConsoleKey.LeftArrow:
                        case ConsoleKey.RightArrow:
                            if (!game.Paused)
                                game.Input(input.Key);
                            break;

                        case ConsoleKey.P:
                            if (game.Paused)
                                game.Resume();
                            else
                                game.Pause();
                            break;

                        case ConsoleKey.Escape:
                            game.Stop();
                            return;
                    }
                }
            }
        }
    }
}