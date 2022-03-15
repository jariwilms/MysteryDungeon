using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MysteryDungeon.Core.Extensions
{
    public static class Extensions
    {
        public static Rectangle Scale(this Rectangle rectangle, float scale)
        {
            rectangle.Width = (int)(rectangle.Width * scale);
            rectangle.Height = (int)(rectangle.Height * scale);

            return rectangle;
        }


    }
}
