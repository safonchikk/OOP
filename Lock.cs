using System;

namespace OOP
{
	public static class Lock
	{
		static Random rnd = new Random();
		static string s = "";
		public static void Gen()
		{
			s = "";
			for (int i = 0; i < 4; ++i)
			{
				string s1 = rnd.Next(0, 10).ToString();
				while (s.IndexOf(s1) != -1)
					s1 = rnd.Next(0, 10).ToString();
				s += s1;
			}
		}
		public static int Break()
		{
			Console.Clear();
			Console.CursorVisible = true;
			int a = 0, b = 0, c = 10;
			bool f = false;
			while (c-- > 0)
			{
				string s1 = Console.ReadLine();
				a = b = 0;
				for (int i = 0; i < 4; ++i)
					if (s1.Length != 4 || s1.IndexOf(s1[i]) != i || s1[i] < '0' || s1[i] > '9')
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("Invalid");
						Console.ForegroundColor = ConsoleColor.White;
						f = true;
						break;
					}
				if (f)
				{
					f = false;
					continue;
				}
				for (int i = 0; i < 4; ++i)
					for (int j = 0; j < 4; ++j)
						if (s[i] == s1[j])
							if (i == j)
								++a;
							else
								++b;
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine($"{a}A{b}B");
				Console.ForegroundColor = ConsoleColor.White;
				if (a == 4)
				{
					Console.CursorVisible = false;
					return 1;
				}
			}
			Console.CursorVisible = false;
			return 0;
		}
	}
}