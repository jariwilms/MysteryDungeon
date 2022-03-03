using System;

namespace MysteryDungeon
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using var game = new MysteryDungeon();
            game.Run();
        }
    }
}
