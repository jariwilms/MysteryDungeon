using System.Configuration;
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
        public int UnitSize;

        public Component()
        {
            Transform = new Transform();
            UnitSize = int.Parse(ConfigurationManager.AppSettings.Get("UnitSize"));
        }

        public void SetPosition(Vector2 newPosition)
        {
            Transform.Position = newPosition * new Vector2(UnitSize, UnitSize);
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
