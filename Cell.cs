using System;

namespace OOP
{
    public class Dot
    {
        public int x, y;
        public Dot (int i, int j)
        {
            x = i;
            y = j;
        }
    }
    public class Cell: Dot
    {
        public char cond, content;
        public Cell(int i, int j, char content = ' ', char cond = 'i'): base(i, j)
        {
            this.content = content;
            this.cond = cond;
        }

        public void Display()
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(x, y);
            switch(cond)
            {
                case 'i':
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write(' ');
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case 'f':
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write('F');
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
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
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.Write(' ');
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else if (content == 'D')
                    {
                        Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        Console.Write(' ');
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else if (content == 'k')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.Write('k');
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
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
            if (content == 'k')
            {
                Display();
                return 3;
            }
            if (content == 'D')
            {
                Display();
                return 4;
            }
            Display();
            return 2;
        }
    }
}