namespace OOP
{
    public class Item
    {
        public string name;
        public int quantity = 0, price = 0;
        public Item(string s, int p = 10)
        {
            name = s;
            price = p;
        }
        public string ToString(int n = 1)
        {
            if (n == 2)
                return $"{name}  {quantity}  {price} coins   ";
            return $"{name}:{quantity}   ";
        }
    }
}
