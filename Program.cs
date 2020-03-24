using System;
using System.IO;

namespace OOP
{
    class Program
    {
        static int AdjMines(ref Cell[,] f, int x, int y)
        {
            int c = 0;
            for (int i = -1; i < 2; ++i)
                for (int j = -1; j < 2; ++j)
                    if (f[y + i, x + j].content == 'L' && f[y + i, x + j].cond != 'v' && Math.Abs(i) + Math.Abs(j) != 0)
                        ++c;
            return c;
        }
        static int KeyHandler(ConsoleKeyInfo k, ref Player player, ref Cell[,] f, ConsoleKey a = ConsoleKey.Delete)
        {
            int i = 0, j = 0;
            switch (k.Key)
            {
                case ConsoleKey.W:
                    j = -1;
                    break;
                case ConsoleKey.A:
                    i = -1;
                    break;
                case ConsoleKey.S:
                    j = 1;
                    break;
                case ConsoleKey.D:
                    i = 1;
                    break;
                default:
                    return 1;
            }
            switch (a)
            {
                case ConsoleKey.Delete:
                    if (k.Modifiers == ConsoleModifiers.Shift)
                    {
                        player.Move(i * i * ((i + 1) * (f.GetLength(1) - 3) / 2 - player.x + 1), j * j * ((j + 1) * (f.GetLength(0) - 3) / 2 - player.y + 1));
                        while (f[player.y,player.x].cond != 'v')
                        {
                            player.Move(-i, -j);
                        }
                    }
                    else player.Move(i, j);
                    goto case 0;
                case ConsoleKey.Spacebar:
                    player.Move(2*i, 2*j);
                    goto case 0;                 
                case ConsoleKey.F:
                    if (f[player.y + j, player.x + i].cond == 'i')
                    {
                        f[player.y + j, player.x + i].cond = 'f';
                        f[player.y + j, player.x + i].Update();
                    }
                    else if (f[player.y + j, player.x + i].cond == 'f')
                    {
                        f[player.y + j, player.x + i].cond = 'i';
                        f[player.y + j, player.x + i].Display();
                    }
                    break;
                case ConsoleKey.E:
                    if (player.stones < 1)
                        break;
                    if (player.y + j > 0 && player.y + j < f.GetLength(0) - 1 && player.x + i > 0 && player.x + i < f.GetLength(1) - 1)
                    {
                        f[player.y + 2 * j, player.x + 2 * i].Update();
                        --player.stones;
                    }
                    break;
                case 0:
                    int h = f[player.y, player.x].Update();
                    if (h == 1)
                    {
                        Console.Clear();
                        Console.Write("________   ______   ______\n" +
                                    "|	| /      \\ /      \\ |\\    /| | | |\n" +
                                    "|      /  |      | |      | | \\  / | | | |\n" +
                                    "|-----{   |      | |      | |  \\/  | | | |\n" +
                                    "|      \\  |      | |      | |      | | | |\n" +
                                    "|_______| \\______/ \\______/ |      | o o o");
                        Console.ReadKey(true);
                        if (--player.lifes == 0)
                        {
                            Console.Clear();
                            Console.Write("         _____              _______     _______  ______\n" +
                                    "\\     / /     \\ |     |      |     \\  | |        |     \\\n" +
                                    " \\   /  |     | |     |      |      | | |        |      | \n" +
                                    "  \\_/   |     | |     |      |      | | |______  |      |\n" +
                                    "   |    |     | |     |      |      | | |        |      |\n" +
                                    "   |    \\_____/ \\_____/     _|______/ | |______ _|______/");
                            Console.ReadKey(true);
                            player.Death();
                            return 0;
                        }
                        Console.Clear();
                        for (int m = 0; m < f.GetLength(0); ++m)
                            for (int n = 0; n < f.GetLength(1); ++n)
                                f[m, n].Display();
                    }
                    if (h == 2)
                    {
                        ++player.coinsforlvl;
                    }
                    break;
            }
            return 1;
        }
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Random rnd = new Random();
            Cell[,] f;          //field
            int l, w, s = 0;        //field size, screen(menu, game, info)
            Player player = new Player();

            if (!File.Exists(@"..\..\data.txt"))
                using (StreamWriter ppp = new StreamWriter(@"..\..\data.txt", false))
                    ppp.Write("1\n3\n0\n0");
                
            using (StreamReader ppp = new StreamReader(@"..\..\data.txt"))
            {
                string g;
                g = ppp.ReadLine();
                player.lvl = int.Parse(g);
                g = ppp.ReadLine();
                player.lifes = int.Parse(g);
                g = ppp.ReadLine();
                player.coins = int.Parse(g);
                g = ppp.ReadLine();
                player.stones = int.Parse(g);
            }           

            while (true) { 
                if (s == 0)
                {
                    Console.Clear();
                    Console.WriteLine("P to play\nI for instructions\nQ to quit\nS for shop\nEscape anytime to return here");
                    player.Stats(true, true, true, true);
                    
                    ConsoleKeyInfo k = Console.ReadKey(true);
                    switch (k.Key)
                    {
                            case ConsoleKey.P:
                                s = 1;
                                break;
                            case ConsoleKey.I:
                                s = 2;
                                break;
                            case ConsoleKey.Q:
                                s = 3;
                                break;
                            case ConsoleKey.S:
                                s = 4;
                                break;
                            default:
                            Console.SetCursorPosition(0, 5);
                                Console.WriteLine("Try again");
                                System.Threading.Thread.Sleep(500);
                                break;
                    }
                    continue;
                }
                if (s == 1)
                {
                    Console.Clear();
                    l = rnd.Next(7, 21);
                    w = rnd.Next(7, 21);
                    f = new Cell[l, w];
                    player.coinsforlvl = 0;

                    for (int i = 1; i < l - 1; ++i)
                        for (int j = 1; j < w - 1; ++j)
                            f[i, j] = new Cell(j, i);

                    for (int i = 0; i < l; ++i)
                    {
                        f[i, 0] = new Cell(0, i, '#', 'v');
                        f[i, w - 1] = new Cell(w - 1, i, '#', 'v');
                    }
                    for (int j = 0; j < w; ++j)
                    {
                        f[0, j] = new Cell(j, 0, '#', 'v');
                        f[l - 1, j] = new Cell(j, l - 1, '#', 'v');
                    }

                    int n, c = (l - 2) * (w - 2);
                    n = rnd.Next(c / 10, c / 5);
                    for (int i = 1; i < l - 1; ++i)
                        for (int j = 1; j < w - 1; ++j)
                        {
                            if (rnd.Next(0, c) < n)
                            {
                                f[i, j].content = 'L';
                                --n;
                            }
                            --c;
                        }

                    for (int i = 0; i < l; ++i)
                        for (int j = 0; j < w; ++j)
                            f[i, j].Display();

                    player.x = rnd.Next(1, w - 2);
                    player.y = rnd.Next(1, l - 2);
                    player.fl = l;
                    player.fw = w;
                    while (true)
                    {
                        int a = rnd.Next(1, l - 2);
                        int b = rnd.Next(1, w - 2);
                        if (f[a, b].content == ' ')
                        {
                            f[a, b].content = 'D';
                            break;
                        }
                    }

                    while (f[player.y, player.x].content == 'L' || f[player.y, player.x].content == 'D')
                    {
                        player.Move(1, 1);
                        if (player.x == w - 1 && player.y == l - 1)
                        {
                            player.x = player.y = 1;
                        }
                    }
                    Console.SetCursorPosition(player.x, player.y);
                    Console.Write('*');

                    while (s == 1)
                    {
                        Console.SetCursorPosition(w + 5, 0);
                        Console.Write("Mines around: " + AdjMines(ref f, player.x, player.y));
                        player.Stats(true, false, true, true, true, 2, w);
                        ConsoleKeyInfo k = Console.ReadKey(true);
                        if (k.Key == ConsoleKey.Escape)
                        {
                            Console.Clear();
                            s = 0;
                            break;
                        }
                        else if (k.Key == ConsoleKey.Enter && f[player.y, player.x].content == 'D' && f[player.y, player.x].cond == 'v')
                        {
                            Console.Clear();
                            Console.WriteLine("LEVEL COMPLETED");
                            Console.ReadKey(true);
                            ++player.lvl;
                            player.coins += player.coinsforlvl;
                            s = 0;
                        }
                        else
                        {
                            int h = f[player.y, player.x].Update();
                            if (k.Key == ConsoleKey.W || k.Key == ConsoleKey.A || k.Key == ConsoleKey.S || k.Key == ConsoleKey.D) 
                            { 
                                s = KeyHandler(k, ref player, ref f);
                            }
                            else s = KeyHandler(Console.ReadKey(true), ref player, ref f, k.Key);
                            Console.SetCursorPosition(player.x, player.y);
                            Console.Write('*');
                        }
                    }
                    continue;
                }
                if (s == 2)
                {
                    Console.Clear();
                    Console.WriteLine("You have to reach the exit.\n" +
                        "There are mines on the floor. When you touch one, one life is lost.\n" +
                        "0 lifes -> restart of game: 3 new lifes, but no coins and progress.\n" +
                        "You can always see how many mines are in your neighbour 3x3 square (your cell excluded)\n" +
                        "Move with WASD, jump in two blocks with Space + WASD\n" +
                        "Shift + WASD gets you to farthest open point in given direction\n" +
                        "F + WASD - flag in given direction (flagged cell guaranteed to not explode)(can be removed the same way)\n" +
                        "E + WASD - stone in two blocks - opens a cell if it isn't flagged\n" +
                        "Enter while standing on trapdoor to finish level\n" +
                        "Buy stones and lifes in shop (Shift gives you biggest possible amount)\n" +
                        "You receive coins for every non-landmine cell opened\n" +
                        "If you go to menu with Esc, level progress is lost (coins won't be given and used lives and stones don't return)\n" +
                        "Your stats are saving in the end of session, when quiting with Q\n" +
                        "Good luck!");
                    while (Console.ReadKey(true).Key != ConsoleKey.Escape){}
                    Console.Clear();
                    s = 0;
                    continue;
                }
                if (s == 3)
                {
                    Console.Clear();
                    Console.WriteLine("Thanks for playing!");
                    using (StreamWriter ppp = new StreamWriter(@"..\..\data.txt", false))
                    {
                        ppp.Write($"{player.lvl}\n{player.lifes}\n{player.coins}\n{player.stones}");
                    }
                    Console.ReadKey(true);
                    break;
                }
                if (s == 4)
                {
                    Console.Clear();
                    Console.WriteLine("L to buy life\n(50 coins)");
                    Console.WriteLine("S to buy stone\n(10 coins)");
                    player.Stats(false, true, true, true);
                    while (true)
                    {
                        ConsoleKeyInfo k = Console.ReadKey(true);
                        if (k.Key == ConsoleKey.L)
                        {
                            if (k.Modifiers == ConsoleModifiers.Shift)
                            {
                                player.lifes += player.coins / 50;
                                player.coins = player.coins % 50;
                            }
                            else if (player.coins >= 50)
                            {
                                ++player.lifes;
                                player.coins -= 50;
                            }
                            player.Stats(false, true, true);
                        }
                        else if (k.Key == ConsoleKey.S)
                        {
                            if (k.Modifiers == ConsoleModifiers.Shift)
                            {
                                player.stones += player.coins / 10;
                                player.coins = player.coins % 10;
                            }
                            else if (player.coins >= 10)
                            {
                                ++player.stones;
                                player.coins -= 10;
                            }
                            player.Stats(false, true, true, true);
                        }
                        else if (k.Key == ConsoleKey.Escape)
                        {
                            Console.Clear();
                            s = 0;
                            break;
                        }
                    }
                    continue;
                }
            }            
        }
    }
}