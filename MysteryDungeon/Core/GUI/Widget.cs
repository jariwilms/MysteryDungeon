using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MysteryDungeon.Core.GUI
{
    public abstract class Widget
    {
        [Flags]
        public enum ScreenPosition
        {
            None = 0,
            Top = 1,
            Right = 2,
            Bottom = 4,
            Left = 8,
            Center = 16
        }

        public Widget Parent;
        private List<Widget> _children;

        public Texture2D SourceTexture;                             //Source texture for widget drawing
        public Rectangle DestinationRectangle;

        public event Action OnHoverEnter;
        public event Action OnHoverLeave;
        public event Action OnClicked;

        public int Depth;

        public bool IsFocused;
        public bool IsActive;
        public bool IsVisible;
        public bool IsResizable;
        public bool isClickable;

        protected int WindowWidth;
        protected int WindowHeight;

        public static float GlobalScale;
        public float LocalScale;

        public Widget(Widget parent = null, bool isResizable = false)
        {
            if (parent != null)
                parent.AddChild(this);

            _children = new List<Widget>();

            IsResizable = isResizable;

            WindowWidth = MysteryDungeon._windowWidth;
            WindowHeight = MysteryDungeon._windowHeight;
            LocalScale = 1.0f;
        }

        public virtual void Enable()
        {
            IsActive = true;
        }

        public virtual void Disable()
        {
            IsActive = false;
        }

        public virtual void Show()
        {
            IsVisible = true;
        }

        public virtual void Hide()
        {
            IsVisible = false;
        }

        public void AddChild(Widget widget) //setparent vs addchild?
        {
            _children.Add(widget);
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spritebatch);
    }
}
