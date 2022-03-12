using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using MysteryDungeon.Core.Animations;

namespace MysteryDungeon.Core.GUI
{
    class GuiManager
    {
        private ContentManager _content;

        private int _windowWidth;
        private int _windowHeight;

        public static List<Widget> Widgets;

        public GuiManager(ContentManager content, int windowWidth, int windowHeight)
        {
            _content = content;

            _windowWidth = windowWidth;
            _windowHeight = windowHeight;

            Widgets = new List<Widget>();
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
    }
}
