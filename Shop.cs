using System;

namespace OOP
{
    public static class Shop
    {
        public static void F(Player player)
        {
            Console.Clear();
            Console.WriteLine("Q\nW\nE\nR\nT");
            player.Stats();
            while (true)
            {
                ConsoleKeyInfo k = Console.ReadKey(true);
                if (k.Key == ConsoleKey.Q)
                {
                    if (k.Modifiers == ConsoleModifiers.Shift)
                    {
                        player.lives += player.coins / 50;
                        player.coins = player.coins % 50;
                    }
                    else if (player.coins >= 50)
                    {
                        ++player.lives;
                        player.coins -= 50;
                    }
                    player.Stats();
                }
                else if (k.Key == ConsoleKey.W)
                {
                    if (k.Modifiers == ConsoleModifiers.Shift)
                    {
                        player[0] += player.coins / player.items[0].price;
                        player.coins = player.coins % player.items[0].price;
                    }
                    else if (player.coins >= player.items[0].price)
                    {
                        ++player[0];
                        player.coins -= player.items[0].price;
                    }
                    player.Stats();
                }
                else if (k.Key == ConsoleKey.E)
                {
                    if (k.Modifiers == ConsoleModifiers.Shift)
                    {
                        player[1] += player.coins / player.items[1].price;
                        player.coins = player.coins % player.items[1].price;
                    }
                    else if (player.coins >= player.items[1].price)
                    {
                        ++player[1];
                        player.coins -= player.items[1].price;
                    }
                    player.Stats();
                }
                else if (k.Key == ConsoleKey.R)
                {
                    if (k.Modifiers == ConsoleModifiers.Shift)
                    {
                        player[2] += player.coins / player.items[2].price;
                        player.coins = player.coins % player.items[2].price;
                    }
                    else if (player.coins >= player.items[2].price)
                    {
                        ++player[2];
                        player.coins -= player.items[2].price;
                    }
                    player.Stats();
                }
                else if (k.Key == ConsoleKey.T)
                {
                    if (k.Modifiers == ConsoleModifiers.Shift)
                    {
                        player[3] += player.coins / player.items[3].price;
                        player.coins = player.coins % player.items[3].price;
                    }
                    else if (player.coins >= player.items[3].price)
                    {
                        ++player[3];
                        player.coins -= player.items[3].price;
                    }
                    player.Stats();
                }
                else if (k.Key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    return;
                }
            }
        }
    }
}