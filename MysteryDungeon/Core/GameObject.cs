using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MysteryDungeon.Core
{
    /// <summary>
    /// Represents a transformation in space
    /// </summary>
    public class Transform
    {
        public Vector2 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public Vector2 Scale { get; set; }

        public Transform()
        {
            Position = Vector2.Zero;
            Rotation = Vector3.Zero;
            Scale = Vector2.One;
        }

        public Transform(Vector2 position, Vector3 rotation, Vector2 scale)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }

        public static Transform operator +(Transform a, Transform b)
            => new Transform(a.Position + b.Position, a.Rotation + b.Rotation, a.Scale + b.Scale);
    }

    public abstract class GameObject : Contracts.IUpdatable, Contracts.IDrawable, IDisposable
    {
        public GameObject Parent { get; set; }
        public Transform Transform
        {
            get => Parent == null ? _transform : Parent.Transform + _transform;
            set => _transform = value;
        }
        private Transform _transform;

        public bool IsEnabled;
        public bool IsVisible;

        protected List<Component> Components { get; private set; }

        protected readonly ContentManager Content;

        public GameObject(GameObject parent = null)
        {
            Parent = parent;
            Transform = new Transform();

            Components = new List<Component>();

            Content = new ContentManager(MysteryDungeon.Services, "Content");
        }

        public TComponent AddComponent<TComponent>() where TComponent : Component
        {
            var component = (TComponent)Activator.CreateInstance(typeof(TComponent), this);
            Components.Add(component);

            return component;
        }

        public TComponent GetComponent<TComponent>() where TComponent : Component
        {
            return Components.FirstOrDefault(component => component.GetType() == typeof(TComponent)) as TComponent;
        }

        public void RemoveComponent<TComponent>() where TComponent : Component
        {
            Components.ForEach(component =>
            {
                if (component.GetType() == typeof(TComponent))
                    Components.Remove(component);
            });
        }

        public virtual void Update(GameTime gameTime)
        {
            Components.ForEach(component =>
            {
                if (component.IsEnabled)
                    component.Update(gameTime);
            });
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Components.ForEach(component =>
            {
                if (component.IsVisible)
                    component.Draw(spriteBatch);
            });
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Content.Unload();
        }
    }
}
