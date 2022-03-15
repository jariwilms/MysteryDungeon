using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MysteryDungeon.Core.Interface
{
    public sealed class GUI : IDisposable
    {
        public static readonly GUI Instance = new GUI();

        private ContentManager _content;

        public List<Widget> Widgets;

        private int _windowWidth;
        private int _windowHeight;

        static GUI()
        {

        }

        private GUI()
        {
            Widgets = new List<Widget>();
        }

        public void Initialize(ContentManager content, int windowWidth, int windowHeight)
        {
            _content = content;

            _windowWidth = windowWidth;
            _windowHeight = windowHeight;
        }

        public void Update(GameTime gameTime)
        {
            Widgets.ForEach(widget =>
            {
                widget.Update(gameTime);
            });
        }

        public void DrawWidgets(SpriteBatch spriteBatch)
        {
            Widgets.ForEach(widget =>
            {
                widget.Draw(spriteBatch);
            });
        }

        public void Dispose()
        {
            _content.Unload();
        }
    }
}
