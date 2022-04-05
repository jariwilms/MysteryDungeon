using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MysteryDungeon.Core
{
    public abstract class Component : Contracts.IUpdatable, Contracts.IDrawable
    {
        public GameObject Parent { get; set; }
        public Transform Transform { get; set; }

        public bool IsEnabled { get; set; }
        public bool IsVisible { get; set; }

        protected readonly ContentManager Content;

        private Component()
        {
            IsEnabled = true;
            IsVisible = true;

            Content = new ContentManager(MysteryDungeon.Services, "Content");
        }

        public Component(GameObject parent) : this()
        {
            Parent = parent;
            Transform = parent.Transform;
        }

        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(SpriteBatch spriteBatch) { }
    }
}
