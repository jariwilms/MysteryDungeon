using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MysteryDungeon.Core.UI
{
    public sealed class GUI : IDisposable
    {
        public static readonly GUI Instance = new GUI();

        private ContentManager _content;

        public SpriteBatch SpriteBatch { get; set; }
        private SpriteFont _spriteFont;

        public List<Widget> Widgets { get; set; }
        public List<(string, Vector2)> Strings { get; set; }

        static GUI()
        {

        }

        private GUI()
        {
            Widgets = new List<Widget>();
            Strings = new List<(string, Vector2)>();
        }

        public void Initialize(ContentManager content, SpriteBatch spriteBatch)
        {
            _content = content;

            SpriteBatch = spriteBatch;
            _spriteFont = content.Load<SpriteFont>("font");
        }


        private void DrawWidgets()
        {
            Widgets.ForEach(widget =>
            {
                widget.Draw(SpriteBatch);
            });
        }

        public void QueueStringDraw(string text, Vector2 position)
        {
            Strings.Add(new(text, position));
        }

        private void DrawStrings()
        {
            Strings.ForEach(s => SpriteBatch.DrawString(_spriteFont, s.Item1, s.Item2, Color.White));
        }

        public void Update(GameTime gameTime)
        {
            Widgets.ForEach(widget =>
            {
                widget.Update(gameTime);
            });
        }

        public void Draw()
        {
            SpriteBatch.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointClamp);

            DrawWidgets();
            DrawStrings();

            Strings.Clear();

            SpriteBatch.End();
        }

        public void Dispose()
        {
            _content.Unload();
        }
    }
}
