using System;

namespace OOP
{
	public class Field
	{
		public int l, w;
		public Cell[,] f;
		public Doors doors;
		public Mines mines;
		public Keys keys;
		Random rnd = new Random();
		public Field(int l, int w)
		{
			this.l = l;
			this.w = w;
			f = new Cell[l, w];

			for (int i = 1; i < l - 1; ++i)					//main field
				for (int j = 1; j < w - 1; ++j)
					f[i, j] = new Cell(j, i);

			for (int i = 0; i < l; ++i)						//walls
			{
				f[i, 0] = new Cell(0, i, '#', 'v');
				f[i, w - 1] = new Cell(w - 1, i, '#', 'v');
			}
			for (int j = 0; j < w; ++j)
			{
				f[0, j] = new Cell(j, 0, '#', 'v');
				f[l - 1, j] = new Cell(j, l - 1, '#', 'v');
			}
			
		}
		public void DoorsGen()
		{
			while (true)
			{
				doors = new Doors();
				int i = rnd.Next(1, l - 2);
				int j = rnd.Next(1, w - 2);
				if (f[i, j].content == ' ')
				{
					f[i, j].content = 'D';
					doors.Push(new Dot(i, j));
					break;
				}
			}
		}
		public void MinesGen() {
			int c = (l - 2) * (w - 2);
			int n = rnd.Next(c / 10, c / 5);
			mines = new Mines(n);
			for (int i = 1; i < l - 1; ++i)
				for (int j = 1; j < w - 1; ++j)
				{
					if (rnd.Next(0, c) < n)
					{
						f[i, j].content = 'L';
						mines.Push(new Dot(i, j));
						--n;
					}
					--c;
				}
		}
		public void KeysGen(int n)
		{
			int c = (l - 2) * (w - 2) - mines.ar.Length;
			keys = new Keys(n);
			for (int i = 1; i < l - 1; ++i)
				for (int j = 1; j < w - 1 && c > 0; ++j)
				{
					if (rnd.Next(0, c) < n && f[i, j].content == ' ')
					{
						f[i, j].content = 'k';
						f[i, j].cond = 'f';
						keys.Push(new Dot(i, j));
						--n;
					}
					--c;
				}
		}
		public void Draw()
		{
			for (int i = 0; i < l; ++i)
				for (int j = 0; j < w; ++j)
					f[i, j].Display();
		}
		public int AdjMines(int x, int y)
		{
			int c = 0;
			for (int i = -1; i < 2; ++i)
				for (int j = -1; j < 2; ++j)
					if (f[y + i, x + j].content == 'L' && f[y + i, x + j].cond != 'v' && Math.Abs(i) + Math.Abs(j) != 0)
						++c;
			return c;
		}
		public Cell this[int x, int y]
		{
			get
			{
				return f[x, y];
			}
			set
			{
				f[x, y] = value;
			}
		}
	}
}