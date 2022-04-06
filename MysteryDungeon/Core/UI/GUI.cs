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

        public List<Widget> Widgets { get; set; }
        public List<(string, Vector2)> Strings { get; set; }

        private SpriteBatch _spriteBatch;
        private SpriteFont _spriteFont;
        private readonly ContentManager _content;

        static GUI() { }
        private GUI()
        {
            Widgets = new List<Widget>();
            Strings = new List<(string, Vector2)>();

            _content = new ContentManager(MysteryDungeon.Services, "Content");
        }

        public void Initialize(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
            _spriteFont = _content.Load<SpriteFont>("font");
        }

        private void DrawWidgets()
        {
            Widgets.ForEach(widget =>
            {
                widget.Draw(_spriteBatch);
            });
        }

        public void QueueStringDraw(string text, Vector2 position)
        {
            Strings.Add(new(text, position));
        }

        public void QueueDebugStringDraw(string text)
        {
            Strings.Add(new (text, new Vector2(20, (Strings.Count + 1) * 20)));
        }

        private void DrawStrings()
        {
            Strings.ForEach(s => _spriteBatch.DrawString(_spriteFont, s.Item1, s.Item2, Color.White));
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
            _spriteBatch.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointClamp);

            DrawWidgets();
            DrawStrings();

            Strings.Clear();

            _spriteBatch.End();
        }

        public void Dispose()
        {
            _content.Unload();
        }
    }
}
