using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MysteryDungeon.Core
{
    class TileMapRenderer : IDisposable
    {
        private TileMap _tileMap;

        private int _unitSize;
        private Texture2D _temp;
        private SpriteFont _font;

        private readonly ContentManager _content;

        public TileMapRenderer(ContentManager content)
        {
            _content = content;

            _unitSize = Int32.Parse(ConfigurationManager.AppSettings.Get("UnitSize"));
            _temp = _content.Load<Texture2D>("tiles/Platform");
            _font = _content.Load<SpriteFont>("font");
        }

        public TileMapRenderer(TileMap tileMap, ContentManager content) : this(content)
        {
            _tileMap = tileMap;
        }

        public void Render(TileMap tileMap)
        {
            _tileMap = tileMap;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            for (int y = 0; y < _tileMap.Height; y++)
            {
                for (int x = 0; x < _tileMap.Width; x++)
                {
                    Texture2D tileTexture = _tileMap.Tiles[x, y].Texture;

                    if (tileTexture != null)
                    {
                        Vector2 tilePosition = new Vector2(x, y) * new Vector2(_unitSize, _unitSize);
                        spriteBatch.Draw(tileTexture, new Rectangle((int)tilePosition.X, (int)tilePosition.Y, _unitSize, _unitSize), Color.White);
                    }
                }
            }

            _tileMap.Rooms.ForEach(room =>
            {
                room.Connectors.ForEach(connector =>
                {
                    Vector2 tilePosition = new Vector2(connector.Position.Item1, connector.Position.Item2) * new Vector2(_unitSize, _unitSize);
                    spriteBatch.Draw(_temp, new Rectangle((int)tilePosition.X, (int)tilePosition.Y, _unitSize, _unitSize), Color.White);
                });
            });

            spriteBatch.DrawString(_font, _tileMap.isComplete ? "Complete" : "Not Complete", new Vector2(100, 100), Color.White);
        }

        public void Dispose()
        {
            _content.Unload();
        }
    }
}
