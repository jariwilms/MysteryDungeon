using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Configuration;

namespace MysteryDungeon.Core
{
    public class Transform
    {
        public Vector2 Position;
        public Vector3 Rotation;
        public Vector2 Scale;

        public Transform()
        {
            Position = Vector2.Zero;
            Rotation = Vector3.Zero;
            Scale = Vector2.One;
        }
    }

    public abstract class GameComponent
    {
        public Transform Transform;
        public int UnitSize;

        public GameComponent()
        {
            Transform = new Transform();
            UnitSize = Int32.Parse(ConfigurationManager.AppSettings.Get("UnitSize"));
        }

        public void SetPosition(Vector2 newPosition)
        {
            Transform.Position = newPosition * new Vector2(UnitSize, UnitSize);
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
