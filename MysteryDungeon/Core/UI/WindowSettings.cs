using Microsoft.Xna.Framework;

namespace MysteryDungeon.Core.UI
{
    /// <summary>
    /// Holds information about the game window
    /// </summary>
    public class WindowSettings
    {
        public int WindowWidth;
        public int WindowHeight;

        public int VirtualWindowWidth;
        public int VirtualWindowHeight;

        public readonly float WindowWidthScale;
        public readonly float WindowHeightScale;
        public Matrix WindowScaleMatrix;

        public WindowSettings(int windowWidth, int windowHeight)
        {
            WindowWidth = windowWidth;
            WindowHeight = windowHeight;

            VirtualWindowWidth = windowWidth;
            VirtualWindowHeight = windowHeight;

            WindowWidthScale = 1.0f;
            WindowHeightScale = 1.0f;
            WindowScaleMatrix = Matrix.Identity; //right?

        }
        public WindowSettings(int windowWidth, int windowHeight, int virtualWindowWidth, int virtualWindowHeight) : this(windowWidth, windowHeight)
        {
            VirtualWindowWidth = virtualWindowWidth;
            VirtualWindowHeight = virtualWindowHeight;

            WindowWidthScale = WindowWidth / VirtualWindowWidth;
            WindowHeightScale = WindowHeight / VirtualWindowHeight;
            WindowScaleMatrix = Matrix.CreateScale(WindowHeightScale, WindowWidthScale, 1.0f);
        }
    }
}
