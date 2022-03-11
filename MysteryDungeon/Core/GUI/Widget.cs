using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace MysteryDungeon.Core.GUI
{
    public abstract class Widget
    {
        public Widget Parent;
        private List<Widget> _children;

        public Rectangle BoundingRectangle { get; protected set; }

        public int Depth;

        public event Action OnHoverEnter;
        public event Action OnHoverLeave;
        public event Action OnClicked;

        public bool IsFocused;
        public bool IsActive;
        public bool IsVisible;
        public bool IsResizable;
        public bool isClickable;

        public Widget(Widget parent = null)
        {
            if (parent != null)
                SetParent(parent);

            _children = new List<Widget>();
        }

        public Widget(int x, int y, int width, int height, bool isResizable = false)
        {
            BoundingRectangle = new Rectangle(x, y, width, height);
            IsResizable = isResizable;
        }

        public void SetParent(Widget widget)
        {
            widget._children.Add(this);
        }

        public abstract void Click();

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

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spritebatch);
    }
}
