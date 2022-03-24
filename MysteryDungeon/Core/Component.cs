using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MysteryDungeon.Core
{
    public abstract class Component
    {
        public GameObject Parent { get; set; }
        public Transform Transform { get; set; }

        public bool Enabled { get; set; }
        public bool IsVisible { get; set; }

        public static ContentManager Content { get; set; }
        protected const int UnitSize = 24;

        private Component()
        {
            Transform = new Transform();

            Enabled = true;
            IsVisible = true;
        }

        public Component(GameObject parent) : this()
        {
            Parent = parent;
            Transform = parent.Transform;
        }

        public abstract void Update(GameTime gameTime);
        public virtual void Draw(SpriteBatch spriteBatch) { }
    }
}
