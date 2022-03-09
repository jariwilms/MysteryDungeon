using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MysteryDungeon.Core.Characters
{
    public class Transform
    {
        public Transform()
        {
            Position = Vector2.Zero;
            Rotation = Vector3.Zero;
            Scale = Vector2.One;
        }

        public Vector2 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public Vector2 Scale { get; set; }
    }

    public abstract class Component
    {
        public Transform Transform;

        public Component()
        {
            Transform = new Transform();
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
