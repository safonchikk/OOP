using System;

namespace OOP
{
    public static class Instr
    {
        public static void F()
        {
            Console.Clear();
            Console.WriteLine("You have to reach the exit.\n" +
                "There are mines on the floor. When you touch one, one life is lost.\n" +
                "0 lives -> restart of game: 3 new lives, but no coins and progress.\n" +
                "You can always see how many mines are in your neighbour 3x3 square (your cell excluded)\n" +
                "Move with WASD, jump in two blocks with Space + WASD\n" +
                "Shift + WASD gets you to farthest open point in given direction\n" +
                "F + WASD - flag in given direction (flagged cell guaranteed to not explode)(can be removed the same way)\n" +
                "E + WASD - stone in two blocks - opens a cell if it isn't flagged\n" +
                "Enter while standing on trapdoor to finish level\n" +
                "Buy stones and lives in shop (Shift gives you biggest possible amount)\n" +
                "You receive coins for every non-landmine cell opened\n" +
                "If you go to menu with Esc, level progress is lost (coins won't be given and used lives and stones don't return)\n" +
                "Your stats are saving in the end of session, when quiting with Q\n" +
                "Good luck!");
            while (Console.ReadKey(true).Key != ConsoleKey.Escape) { }
            Console.Clear();
            return;
        }
    }
}