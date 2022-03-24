using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

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

        protected List<Component> Components { get; private set; }

        public static ContentManager Content { get; set; }
        protected const int UnitSize = 24;

        public GameObject()
        {
            Transform = new Transform();
            Components = new List<Component>();
        }

        public Component AddComponent<TComponent>() where TComponent : Component
        {
            var component = (TComponent)Activator.CreateInstance(typeof(TComponent), this);
            Components.Add(component);

            return component;
        }

        public Component GetComponent<TComponent>() where TComponent : Component
        {
            return Components.FirstOrDefault(component => component.GetType() == typeof(TComponent));
        }

        public void RemoveComponent<TComponent>() where TComponent : Component
        {
            Components.ForEach(component =>
            {
                if (component.GetType() == typeof(TComponent))
                    Components.Remove(component);
            });
        }

        public void SetPosition(Vector2 newPosition)
        {
            Transform.Position = newPosition * new Vector2(UnitSize, UnitSize);
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
