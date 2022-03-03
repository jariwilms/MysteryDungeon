using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MysteryDungeon.Core
{
    class Transform
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

    abstract class Component
    {
        public Transform Transform { get { return _transform; } private set { _transform = value; } }
        private Transform _transform;

        public Component()
        {
            Transform = new Transform();
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
