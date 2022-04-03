using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MysteryDungeon.Core.Animations;

namespace MysteryDungeon.Core.Components
{
    public class SpriteRendererComponent : Component
    {
        public Sprite Sprite { get; set; }


        public Color Color { get; set; }

        private Rectangle _destinationRectangle;

        public SpriteRendererComponent(GameObject parent) : base(parent)
        {
            Color = Color.White;
        }

        public override void Update(GameTime gameTime)
        {
            _destinationRectangle = new Rectangle((int)Parent.Transform.Position.X, (int)Parent.Transform.Position.Y, Sprite.Width, Sprite.Height); //verander naar sprite location, past niet altijd in een tile
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            int leftDiff = (Sprite.Width - Sprite.SourceRectangle.Width) / 2;
            int bottomDiff = (Sprite.Height - Sprite.SourceRectangle.Height);

            spriteBatch.Draw(
                Sprite.SourceTexture,
                new Rectangle(_destinationRectangle.X + leftDiff, _destinationRectangle.Y + bottomDiff - 4, Sprite.SourceRectangle.Width, Sprite.SourceRectangle.Height),
                Sprite.SourceRectangle,
                Color, 0.0f, Vector2.Zero, Sprite.SpriteEffects, 0.0f);
        }
    }
}
