using System;

namespace OOP
{
    class Program
    {
        static int KeyHandler(ConsoleKeyInfo k, Player player, Field field, ref int keyneed, ConsoleKey a = ConsoleKey.Delete)
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
                        player.Move(i * i * ((i + 1) * (field.w - 3) / 2 - player.x + 1), j * j * ((j + 1) * (field.l - 3) / 2 - player.y + 1));
                        while (field[player.y, player.x].cond != 'v')
                        {
                            player.Move(-i, -j);
                        }
                        if (i > 0)
                            player.dir = '>';
                        if (i < 0)
                            player.dir = '<';
                        if (j > 0)
                            player.dir = 'v';
                        if (j < 0)
                            player.dir = '^';
                    }
                    else player.Move(i, j);
                    goto case 0;
                case ConsoleKey.Spacebar:
                    player.Move(2 * i, 2 * j);
                    goto case 0;
                case ConsoleKey.F:
                    if (field[player.y + j, player.x + i].cond == 'i')
                    {
                        field[player.y + j, player.x + i].cond = 'f';
                        field[player.y + j, player.x + i].Update();
                    }
                    else if (field[player.y + j, player.x + i].cond == 'f')
                    {
                        field[player.y + j, player.x + i].cond = 'i';
                        field[player.y + j, player.x + i].Display();
                    }
                    break;
                case ConsoleKey.E:
                    if (player[0] < 1)
                        break;
                    if (player.y + j > 0 && player.y + j < field.l - 1 && player.x + i > 0 && player.x + i < field.w - 1)
                    {
                        field[player.y + 2 * j, player.x + 2 * i].Update();
                        --player[0];
                    }
                    break;
                case ConsoleKey.Z:
                    if (player[2] < 1)
                        break;
                    --player[2];
                    Dot q = field.keys.Light();
                    Console.SetCursorPosition(q.y, q.x);
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.Write(' ');
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case ConsoleKey.X:
                    if (player[3] < 1)
                        break;
                    --player[3];
                    q = field.doors.Light();
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    for (int i1 = q.x; i1 < q.x + 3; ++i1)
                        for (int j1 = q.y; j1 < q.y + 3; ++j1)
                            if (field[i1,j1].content != '#' && field[i1, j1].cond != 'v')
                            {
                                Console.SetCursorPosition(j1, i1);
                                Console.Write(' ');
                            }
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case ConsoleKey.L:
                    if (field[player.y, player.x].content == 'D' && field[player.y, player.x].cond == 'v' && player[1] > 0)
                    {
                        --player[1];
                        Lock.Gen();
                        int y = Lock.Break();
                        if (y == 1)
                            --keyneed;
                        field.Draw();
                        player.Display();
                    }
                    break;
                case 0:
                    int h = field[player.y, player.x].Update();
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
                        if (--player.lives == 0)
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
                        for (int m = 0; m < field.l; ++m)
                            for (int n = 0; n < field.w; ++n)
                                field[m, n].Display();
                    }
                    if (h == 2)
                    {
                        ++player.coinsforlvl;
                    }
                    if (h == 3)
                    {
                        Console.SetCursorPosition(field.w + 5, 8);
                        Console.Write("Oh, here's a key");
                        ++player.keys;
                        System.Threading.Thread.Sleep(500);
                        Console.SetCursorPosition(field.w + 5, 8);
                        Console.Write("                               ");
                        field[player.y, player.x].content = ' ';
                        field[player.y, player.x].Update();
                    }
                    if (h == 4)
                    {
                        Console.SetCursorPosition(field.w + 5, 8);
                        Console.Write("Oh, here's a door");
                        ++player.coinsforlvl;
                        System.Threading.Thread.Sleep(500);
                        Console.SetCursorPosition(field.w + 5, 8);
                        Console.Write("                               ");
                    }
                    break;
            }
            return 1;
        }
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Random rnd = new Random();
            int l, w, s = 0, keyneed;        //field size, screen
            Player player = new Player();
            FileHandler.Read(player);           

            while (true) { 
                if (s == 0)
                {
                    s = Menu.F(player);
                }
                else if (s == 1)
                {
                    Console.Clear();
                    player.coinsforlvl = 0;
                    player.keys = 0;
                    l = rnd.Next(player.lvl * 3 + 10, player.lvl * 3 + 30);
                    w = rnd.Next(player.lvl * 3 + 10, player.lvl * 3 + 30);
                    keyneed = rnd.Next(0, player.lvl / 3 + 2);

                    Field field = new Field(l, w);
                    field.MinesGen();
                    field.KeysGen(keyneed + 3);
                    field.DoorsGen();
                    field.Draw();

                    player.x = rnd.Next(1, w - 1);
                    player.y = rnd.Next(1, l - 1);
                    player.fl = l;
                    player.fw = w;

                    while (field[player.y, player.x].content != ' ')
                    {
                        player.Move(1, 1);
                        if (player.x == w - 1 && player.y == l - 1)
                        {
                            player.x = player.y = 1;
                        }
                    }
                    player.Display();

                    while (s == 1)
                    {
                        Console.SetCursorPosition(w + 5, 0);
                        Console.Write("Mines around: " + field.AdjMines(player.x, player.y));
                        player.Stats(level: true, inv: 1, k: 2, coin: 1, key: true, w: w);
                        ConsoleKeyInfo k = Console.ReadKey(true);
                        if (k.Key == ConsoleKey.Escape)
                        {
                            Console.Clear();
                            s = 0;
                            break;
                        }
                        else if (k.Key == ConsoleKey.Enter && field[player.y, player.x].content == 'D' && field[player.y, player.x].cond == 'v')
                        {
                            if(player.keys < keyneed)
                            {
                                keyneed -= player.keys;
                                player.keys = 0;
                                Console.SetCursorPosition(w + 5, 12);
                                Console.Write($"You need {keyneed} more key");
                                if (keyneed > 1)
                                    Console.Write("s     ");
                                else
                                    Console.Write("      ");
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("LEVEL COMPLETED");
                                Console.ReadKey(true);
                                ++player.lvl;
                                player.coins += player.coinsforlvl;
                                player.keys = 0;
                                s = 0;
                            }
                            
                        }
                        else
                        {
                            int h = field[player.y, player.x].Update();
                            if (k.Key == ConsoleKey.W || k.Key == ConsoleKey.A || k.Key == ConsoleKey.S || k.Key == ConsoleKey.D) 
                            { 
                                s = KeyHandler(k, player, field, ref keyneed);
                            }
                            else s = KeyHandler(Console.ReadKey(true), player, field, ref keyneed, k.Key);
                            player.Display();
                        }
                    }
                }
                else if (s == 2)
                {
                    Instr.F();
                    s = 0;
                }
                else if (s == 3)
                {
                    Console.Clear();
                    Console.WriteLine("Thanks for playing!");
                    FileHandler.Write(player);
                    Console.ReadKey(true);
                    break;
                }
                else if (s == 4)
                {
                    Shop.F(player);
                    s = 0;
                }
                else if (s == 5)
                {
                    Console.Clear();
                    player.Stats(life: false, coin: 0, inv: 1, w: -5);
                    Console.ReadKey(true);
                    s = 0;
                }
            }            
        }
    }
}