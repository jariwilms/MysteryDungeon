using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MysteryDungeon.Core.Animations;
using System.Configuration;

namespace MysteryDungeon.Core.Characters
{
    public class Sprite : Component
    {
        public Texture2D Texture { get; set; }
        public Rectangle BoundingRectangle { get { return new Rectangle((int)Transform.Position.X, (int)Transform.Position.Y, _unitSize, _unitSize); } }

        protected AnimationPlayer _animationPlayer;
        protected Texture2D _spriteSheet;

        protected int _unitSize;

        public Sprite(Texture2D texture)
        {
            Texture = texture;
            _unitSize = int.Parse(ConfigurationManager.AppSettings.Get("UnitSize"));
        }

        public void SetPosition(Vector2 newPosition)
        {
            Transform.Position = newPosition * new Vector2(_unitSize, _unitSize);
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Texture, Transform.Position, Color.White);
        }
    }
}
