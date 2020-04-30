using System;

namespace OOP
{
    static class Menu
    {
        public static int F(Player player)
        {
            Console.Clear();
            Console.WriteLine("P to play\nE for inventory\nS for shop\nI for instructions\nQ to quit\nEscape anytime to return here");
            player.Stats(level: true, inv: 0);
            while (true)
            {
                ConsoleKeyInfo k = Console.ReadKey(true);
                switch (k.Key)
                {
                    case ConsoleKey.P:
                        return 1;
                    case ConsoleKey.I:
                        return 2;
                    case ConsoleKey.Q:
                        return 3;
                    case ConsoleKey.S:
                        return 4;
                    case ConsoleKey.E:
                        return 5;
                    default:
                        Console.SetCursorPosition(0, 6);
                        Console.WriteLine("Try again");
                        System.Threading.Thread.Sleep(500);
                        Console.SetCursorPosition(0, 6);
                        Console.WriteLine("         ");
                        break; 
                }
            }
            
        }
    }
}