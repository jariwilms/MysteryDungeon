using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MysteryDungeon.Core.Animations;
using MysteryDungeon.Core.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryDungeon.Core.Components
{
    class SpriteRenderer : Component
    {
        public Sprite Sprite { get; protected set; }
        private Rectangle _destinationRectangle;

        public SpriteRenderer(GameObject parent, Sprite sprite)
        {
            Parent = parent;
            Sprite = sprite;
        }

        public override void Update(GameTime gameTime) //spaghetti
        {
            _destinationRectangle = new Rectangle((int)Parent.Transform.Position.X, (int)Parent.Transform.Position.Y, UnitSize, UnitSize);
            Sprite.AnimationPlayer.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Animation currentAnimation = Sprite.AnimationPlayer.CurrentAnimation;

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
