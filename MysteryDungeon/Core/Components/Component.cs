using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MysteryDungeon.Core.Components
{
    public abstract class Component : IDisposable
    {
        public GameObject Parent { get; protected set; }
        public Transform Transform { get; set; }

        public bool Enabled { get; set; }
        public bool IsVisible { get; set; }

        public static ContentManager Content { get; set; }
        protected const int UnitSize = 24;

        public Component()
        {
            Transform = new Transform();
        }

        public Component(Transform transform) : this()
        {
            Transform = transform;
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);

        public void Dispose()
        {
            Content.Unload();
        }
    }
}
