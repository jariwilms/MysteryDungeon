using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MysteryDungeon.Core.Animations
{
    public class Sprite
    {
        public Transform Transform;

        public Texture2D SourceTexture { get; set; }
        public Rectangle SourceRectangle { get; set; }

        public SpriteEffects SpriteEffects { get; set; }
        public Rectangle BoundingRectangle { get; set; }

        public Sprite()
        {
            BoundingRectangle = new Rectangle(0, 0, 24, 24);
        }
    }
}
