using System;

namespace OOP
{
    class Cell
    {
        public char cond, content;
        public int x, y;

        public Cell(int x, int y, char content = ' ', char cond = 'i')
        {
            this.content = content;
            this.cond = cond;
            this.x = x;
            this.y = y;
        }

        public void Display()
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(x, y);
            switch(cond)
            {
                case 'i':
                    Console.Write('.');
                    break;
                case 'f':
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write('F');
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                default:
                    if (content == 'L')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write('o');
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if(content == '#')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write('#');
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (content == 'D')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write('D');
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                        Console.Write(content);
                    break;
            }
        }
        public int Update()
        {
            if (cond == 'f' || cond == 'v')
            {
                Display();
                return 0;
            }
            cond = 'v';
            if (content == 'L')
            {
                Display();
                return 1;
            }
            Display();
            return 2;
        }
    }
}
/* content - wall #, landmine L, empty  , door D
    cond(ition) - 'v'isible, 'i'nvisible, 'f'lagged*/