using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MysteryDungeon.Core.Animations;
using System;

namespace MysteryDungeon.Core.Components
{
    public class SpriteRendererComponent : Component
    {
        public Sprite Sprite { get; set; }
        public Color Color { get; set; }

        private Rectangle _destinationRectangle;

        public SpriteRendererComponent(GameObject parent) : base(parent)
        {

        }

        public override void Update(GameTime gameTime)
        {
            _destinationRectangle = new Rectangle((int)Parent.Transform.Position.X, (int)Parent.Transform.Position.Y, UnitSize, UnitSize);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            return;

            //Animation currentAnimation = Sprite.Animator.CurrentAnimation;
            Animation currentAnimation = default; //werkt totaal niet maar de return statement fixt dat wel

            if (currentAnimation == null)
                throw new NullReferenceException("No Sprite has been set");

            int leftDiff = (24 - currentAnimation.SourceRectangle.Width) / 2;
            int bottomDiff = 24 - currentAnimation.SourceRectangle.Height;

            spriteBatch.Draw(
                currentAnimation.SourceTexture,
                new Rectangle(_destinationRectangle.X + leftDiff, _destinationRectangle.Y + bottomDiff - 4, currentAnimation.SourceRectangle.Width, currentAnimation.SourceRectangle.Height),
                currentAnimation.SourceRectangle,
                Color.White, 0.0f, Vector2.Zero, effects: currentAnimation.SpriteEffects, 0.0f);
        }
    }
}
