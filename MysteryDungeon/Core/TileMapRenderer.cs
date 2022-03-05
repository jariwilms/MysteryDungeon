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
        private SpriteAtlas<TileType> _spriteAtlas;

        private int _unitSize;
        private Texture2D _platformTexture;
        private SpriteFont _spriteFontArial;

        private readonly ContentManager _content;

        public TileMapRenderer(ContentManager content)
        {
            _content = content;
            _unitSize = Int32.Parse(ConfigurationManager.AppSettings.Get("UnitSize"));

            Texture2D texture = _content.Load<Texture2D>("tiles/dungeon tileset");
            _platformTexture = _content.Load<Texture2D>("tiles/Platform");
            _spriteFontArial = _content.Load<SpriteFont>("font");

            _spriteAtlas = new SpriteAtlas<TileType>(texture, 16);

            SetupAtlas();
        }

        private void SetupAtlas()
        {
            _spriteAtlas.AddSprite(TileType.Floor, 32, 48);
            _spriteAtlas.AddSprite(TileType.Wall, 32, 32);
            _spriteAtlas.AddSprite(TileType.Ceiling, 0, 0);

            _spriteAtlas.AddSprite(TileType.LedgeTop, 32, 64);
            _spriteAtlas.AddSprite(TileType.LedgeRight, 16, 32);
            _spriteAtlas.AddSprite(TileType.LedgeBottom, 32, 16);
            _spriteAtlas.AddSprite(TileType.LedgeLeft, 176, 32);

            _spriteAtlas.AddSprite(TileType.CornerTopLeft, 128, 80);
            _spriteAtlas.AddSprite(TileType.CornerTopRight, 64, 80);
            _spriteAtlas.AddSprite(TileType.CornerBottomRight, 48, 16);
            _spriteAtlas.AddSprite(TileType.CornerBottomLeft, 144, 16);

            _spriteAtlas.AddSprite(TileType.RidgeTopLeft, 144, 80);
            _spriteAtlas.AddSprite(TileType.RidgeTopRight, 48, 80);
            _spriteAtlas.AddSprite(TileType.RidgeBottomRight, 48, 0);
            _spriteAtlas.AddSprite(TileType.RidgeBottomLeft, 176, 16);

            _spriteAtlas.AddSprite(TileType.ConnectorHorizontal, 0, 0);
            _spriteAtlas.AddSprite(TileType.ConnectorVertical, 0, 0);

            _spriteAtlas.AddSprite(TileType.TopCap, 0, 0);
            _spriteAtlas.AddSprite(TileType.RightCap, 0, 0);
            _spriteAtlas.AddSprite(TileType.BottomCap, 0, 0);
            _spriteAtlas.AddSprite(TileType.LeftCap, 0, 0);
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
                    TileType tileType = _tileMap.Tiles[x, y].TileType;

                    if (tileType != TileType.None)
                    {
                        _spriteAtlas.SetCurrentSprite(tileType);

                        Vector2 tilePosition = new Vector2(x, y) * new Vector2(_unitSize, _unitSize);

                        spriteBatch.Draw(
                            _spriteAtlas.SourceTexture, 
                            new Rectangle((int)tilePosition.X, (int)tilePosition.Y, _unitSize, _unitSize), 
                            _spriteAtlas.SourceRectangle, 
                            Color.White);
                    }
                }
            }

            _tileMap.Rooms.ForEach(room =>
            {
                room.Connectors.ForEach(connector =>
                {
                    Vector2 tilePosition = new Vector2(connector.Position.Item1, connector.Position.Item2) * new Vector2(_unitSize, _unitSize);
                    spriteBatch.Draw(_platformTexture, new Rectangle((int)tilePosition.X, (int)tilePosition.Y, _unitSize, _unitSize), Color.White);
                });
            });

            //spriteBatch.DrawString(_spriteFontArial, _tileMap.isComplete ? "Complete" : "Not Complete", new Vector2(100, 100), Color.White);
        }

        public void Dispose()
        {
            _content.Unload();
        }
    }
}
