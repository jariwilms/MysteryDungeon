using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MysteryDungeon.Core.GUI
{
    class GuiManager : IDisposable
    {
        private ContentManager _content;

        private int _windowWidth;
        private int _windowHeight;

        public static List<Widget> Widgets;

        static GuiManager()
        {
            Widgets = new List<Widget>();
        }

        public GuiManager(ContentManager content, int windowWidth, int windowHeight)
        {
            _content = content;

            _windowWidth = windowWidth;
            _windowHeight = windowHeight;
        }

        private void CreateDialogueAtlas()
        {

        }

        public void Update(GameTime gameTime)
        {
            Widgets.ForEach(widget =>
            {
                widget.Update(gameTime);
            });
        }

        public void Draw(SpriteBatch spriteBatch)
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
