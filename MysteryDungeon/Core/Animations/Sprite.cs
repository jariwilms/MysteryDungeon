using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MysteryDungeon.Core.Animations
{
    public class Sprite
    {
        public Transform Transform;

        public int Width { get; set; }
        public int Height { get; set; }

        public Texture2D SourceTexture { get; set; }
        public Rectangle SourceRectangle { get; set; }

        public SpriteEffects SpriteEffects { get; set; }
        public Rectangle BoundingRectangle { get; set; }

        public Sprite(int width = 24, int height = 24)
        {
            Width = width;
            Height = height;

            BoundingRectangle = new Rectangle(0, 0, Width, Height);
        }
    }
}
