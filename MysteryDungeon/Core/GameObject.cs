using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

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

    public abstract class GameObject : IDisposable
    {
        public Transform Transform;
        public Transform Offset;

        public static ContentManager Content;
        public const int UnitSize = 24;

        public GameObject()
        {
            Transform = new Transform();
            Offset = new Transform();
        }

        public void SetPosition(Vector2 newPosition)
        {
            Transform.Position = newPosition * new Vector2(UnitSize, UnitSize);
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);

        public void Dispose() //die gaat wrs alle content unloaden lol
        {
            Content.Unload();
        }
    }
}
