using System;

namespace OOP
{
    public abstract class Elements
    {
        public Dot[] ar;
        protected int last = 0;
        protected Random rnd = new Random();
        public Elements(int n)
        {
            ar = new Dot[n];
        }
        public Dot this[int i]
        {
            get
            {
                return ar[i];
            }
        }
        public void Push(Dot d)
        {
            ar[last++] = d;
        }
    }

    public class Doors : Elements
    {
        public Doors() : base(1) { }
        public Dot Light()
        {
            Dot p = new Dot(Math.Max(ar[0].x + rnd.Next(-2, 1), 0), Math.Max(ar[0].y + rnd.Next(-2, 1),0));
            return p;
        }
    }
    public class Mines : Elements
    {
        public Mines(int n) : base(n) { }
    }
    public class Keys : Elements
    {
        public Keys(int n) : base(n) { }

        public Dot Light()
        {
            return ar[rnd.Next(0, last)];
        }
    }
}