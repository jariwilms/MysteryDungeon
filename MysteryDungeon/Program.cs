using System;

namespace MysteryDungeon
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using var game = new MysteryDungeon();
            game.Run();
        }
    }
}
