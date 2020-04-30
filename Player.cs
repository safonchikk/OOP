using System;
using System.ComponentModel.DataAnnotations;

namespace OOP
{
    public class Player
    {
        public int x, y, fw, fl, lives, coins, lvl, coinsforlvl, keys;
        public char dir = '*';
        public Item[] items;
        public Player(int x = 1, int y = 1, int lives = 3)
        {
            this.x = x;
            this.y = y;
            this.lives = lives;
            items = new Item[4];
            items[0] = new Item("Stones    ");
            items[1] = new Item("Lockpicks ");
            items[2] = new Item("Keylights ",30);
            items[3] = new Item("Doorlights",50);
        }
        public int this[int i]
        {
            get { return items[i].quantity; }
            set { items[i].quantity = value; }
        }
        public void Display()
        {
            Console.SetCursorPosition(x, y);
            Console.Write(dir);
        }
        public void Move(int i = 0, int j = 0)
        {
            if (x + i > 0 && x + i < fw-1) 
            { 
                x += i;
                if (i > 0)
                    dir = '>';
                if (i < 0)
                    dir = '<';
            }
            if (y + j > 0 && y + j < fl - 1) 
            { 
                y += j;
                if (j > 0)
                    dir = 'v';
                if (j < 0)
                    dir = '^';
            }
        }

        public void Stats(bool level = false, bool life = true, int coin = 2, bool key = false, int inv = 2, int k = 0, int w = 20)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;
            if (inv == 2)
                w = -2;
            Console.SetCursorPosition(5 + w, k);
            if (level)
            {
                Console.Write($"Level:{lvl}     ");
                ++k;
                Console.SetCursorPosition(5 + w, k);
            }
            if (life)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("♥");
                if (lives < 6) 
                    for(int i = 1; i < lives; ++i)
                        Console.Write("♥");
                else
                    Console.Write($"x{lives}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("      ");
                if (inv == 2)
                    Console.Write("50 coins");
                ++k;
                Console.SetCursorPosition(5 + w, k);
            }
            if (inv != 0)
            {
                for (int i = 0; i < 4; ++i)
                {
                    Console.Write(items[i].ToString(inv));
                    ++k;
                    Console.SetCursorPosition(5 + w, k);
                }
            }
            if (coin == 2)
            {
                Console.Write($"Coins:{coins}     ");
                ++k;
                Console.SetCursorPosition(5 + w, k);
            }
            else if (coin == 1)
            {
                Console.Write($"Coins:{coinsforlvl}     ");
                ++k;
                Console.SetCursorPosition(5 + w, k);
            }
            if (key)
            {
                Console.Write($"Keys:{keys}     ");
                ++k;
                Console.SetCursorPosition(5 + w, k);
            }
        }

        public void Death()
        {
            lives = 3;
            lvl = 1;
            coins = 0;
            for (int i = 0; i < 4; ++i)
                items[i].quantity = 0;
        }

    }
}
