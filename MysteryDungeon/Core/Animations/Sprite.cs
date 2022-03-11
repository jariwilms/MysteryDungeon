using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MysteryDungeon.Core.Characters;

namespace MysteryDungeon.Core.Animations
{
    public class Sprite //Extend met animatedsprite class, of maak aparte class zonder extension?
    {
        public Texture2D Texture;
        public AnimationPlayer AnimationPlayer;
        public Vector2 DrawingPosition { get; set; }
        public Rectangle BoundingRectangle { get { return new Rectangle((int)DrawingPosition.X, (int)DrawingPosition.Y, _unitSize, _unitSize); } }

        private int _unitSize;

        public Sprite(Texture2D texture, int unitSize)
        {
            Texture = texture;
            AnimationPlayer = new AnimationPlayer();
            _unitSize = unitSize;
        }

        public void Update(GameTime gameTime, Transform transform)
        {
            DrawingPosition = transform.Position;
            AnimationPlayer.Update(gameTime);
            AnimationPlayer.DestinationRectangle = new Rectangle((int)DrawingPosition.X, (int)DrawingPosition.Y, _unitSize, _unitSize);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            AnimationPlayer.Draw(spriteBatch);
        }
    }
}
