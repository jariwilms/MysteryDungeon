using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MysteryDungeon.Core.Extensions;
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

    public abstract class GameObject
    {
        public Transform Transform;
        public Transform Offset;
        public int UnitSize;

        public GameObject()
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
