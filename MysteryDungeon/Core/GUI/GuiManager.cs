using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MysteryDungeon.Core.GUI
{
    class GuiManager
    {
        private ContentManager _content;
        private SpriteBatch _spriteBatch;

        private int _screenWidth;
        private int _screenHeight;

        public static List<Widget> Widgets;

        public static Texture2D DialogueBoxTexture;

        public GuiManager(ContentManager content, SpriteBatch spritebatch, int screenWidth, int screenHeight)
        {
            _content = content;
            _spriteBatch = spritebatch;

            _screenWidth = screenWidth;
            _screenHeight = screenHeight;

            //DialogueBoxTexture = _content.Load<Texture2D>("gui/dialogue_box");
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime)
        {
            Widgets.ForEach(widget =>
            {
                widget.Draw(_spriteBatch);
            });
        }
    }
}
