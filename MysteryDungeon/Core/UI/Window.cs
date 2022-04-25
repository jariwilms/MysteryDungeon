using Microsoft.Xna.Framework;

namespace MysteryDungeon.Core.UI
{
    /// <summary>
    /// Holds information about the game window
    /// </summary>
    public static class Window
    {
        public static int WindowWidth { get; set; }
        public static int WindowHeight { get; set; }

        public static int VirtualWindowWidth { get; set; }
        public static int VirtualWindowHeight { get; set; }

        public static float WindowWidthScale { get; set; }
        public static float WindowHeightScale { get; set; }
        public static Matrix WindowScaleMatrix { get; set; }

        static Window()
        {
            WindowWidth = 800;
            WindowHeight = 600;

            VirtualWindowWidth = WindowWidth;
            VirtualWindowHeight = WindowHeight;

            WindowWidthScale = 1.0f;
            WindowHeightScale = 1.0f;
            WindowScaleMatrix = Matrix.Identity;
        }

        public static void Resize(int newWidth, int newHeight)
        {
            WindowWidth = newWidth;
            WindowHeight = newHeight;
        }

        public static bool IsOutOfBounds(Vector2 position)
        {
            return position.X < 0 || position.X > WindowWidth || position.Y < 0 || position.Y > WindowHeight;
        }
    }
}
