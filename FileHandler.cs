using System;
using System.IO;

namespace OOP
{
    public static class FileHandler
    {
        public static void Read(Player player)
        {
            if (!File.Exists(@"..\..\data.txt"))
            {
                player.Death();
                Write(player);
            }
            using (StreamReader ppp = new StreamReader(@"..\..\data.txt"))
            {
                string g;
                g = ppp.ReadLine();
                player.lvl = int.Parse(g);
                g = ppp.ReadLine();
                player.lives = int.Parse(g);
                g = ppp.ReadLine();
                player.coins = int.Parse(g);
                g = ppp.ReadLine();
                for (int i = 0; i < 4; ++i)
                {
                    player[i] = int.Parse(g);
                    g = ppp.ReadLine();
                }
            }
        }
        public static void Write(Player player)
        {
            using (StreamWriter ppp = new StreamWriter(@"..\..\data.txt", false))
            {
                ppp.Write($"{player.lvl}\n{player.lives}\n{player.coins}\n{player[0]}\n{player[1]}\n{player[2]}\n{player[3]}");
            }
        }
    }
}