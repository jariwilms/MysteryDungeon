using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MysteryDungeon.Core.Components;
using System;
using System.Collections.Generic;

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

        public List<Component> Components { get; set; }

        public static ContentManager Content { get; set; }
        protected const int UnitSize = 24;

        public GameObject()
        {
            Transform = new Transform();
            Offset = new Transform();

            Components = new List<Component>();
        }

        public void SetPosition(Vector2 newPosition)
        {
            Transform.Position = newPosition * new Vector2(UnitSize, UnitSize);
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
