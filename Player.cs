using System;

namespace OOP
{
    class Player
    {
        public int x, y, fw, fl, lifes, coins, stones, lvl, coinsforlvl;
        public Player(int x = 1, int y = 1, int lifes = 3)
        {
            this.x = x;
            this.y = y;
            this.lifes = lifes;
        }

        public void Move(int i = 0, int j = 0)
        {
            if (x + i > 0 && x + i < fw-1) { x += i; }
            if (y + j > 0 && y + j < fl-1) { y += j; }
        }

        public void Stats(bool s1 = false, bool s2 = false, bool s3 = false, bool s4 = false, bool s5 = false, int k = 0, int w = 20)
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(5 + w, k);
            if (s1)
            {
                Console.Write($"Level:{lvl}     ");
                ++k;
                Console.SetCursorPosition(5 + w, k);
            }
            if (s2)
            {
                Console.Write($"Coins:{coins}     ");
                ++k;
                Console.SetCursorPosition(5 + w, k);
            }
            if (s3)
            {
                Console.Write($"Lifes:{lifes}     ");
                ++k;
                Console.SetCursorPosition(5 + w, k);
            }
            if (s4)
            {
                Console.Write($"Stones:{stones}     ");
                ++k;
                Console.SetCursorPosition(5 + w, k);
            }
            
            if (s5)
            {
                Console.Write($"Coins:{coinsforlvl}     ");
                ++k;
                Console.SetCursorPosition(5 + w, k);
            }
        }

        public void Death()
        {
            lifes = 3;
            lvl = 1;
            coins = 0;
            stones = 0; 
        }

    }
}
