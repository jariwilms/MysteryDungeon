using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Configuration;

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

            Texture2D texture = _content.Load<Texture2D>("tiles/tiny_woods");
            _platformTexture = _content.Load<Texture2D>("tiles/platform");
            _spriteFontArial = _content.Load<SpriteFont>("font");

            _spriteAtlas = new SpriteAtlas<TileType>(texture, new Point(9, 163), 1, 1, 24);

            SetupAtlas();
        }

        private void SetupAtlas()
        {
            _spriteAtlas.AddSprite(TileType.Floor, 13, 1);
            _spriteAtlas.AddSprite(TileType.Wall, 6, 0); //not used yet
            _spriteAtlas.AddSprite(TileType.Block, 4, 1);
            _spriteAtlas.AddSprite(TileType.Pillar, 4, 4);

            _spriteAtlas.AddSprite(TileType.LedgeTop, 4, 0); //rename to wallTop?
            _spriteAtlas.AddSprite(TileType.LedgeRight, 5, 1);
            _spriteAtlas.AddSprite(TileType.LedgeBottom, 4, 2);
            _spriteAtlas.AddSprite(TileType.LedgeLeft, 3, 1);

            _spriteAtlas.AddSprite(TileType.CornerTopLeft, 3, 0);
            _spriteAtlas.AddSprite(TileType.CornerTopRight, 5, 0);
            _spriteAtlas.AddSprite(TileType.CornerBottomRight, 5, 2);
            _spriteAtlas.AddSprite(TileType.CornerBottomLeft, 3, 2);

            _spriteAtlas.AddSprite(TileType.RidgeTopLeft, 6, 0);
            _spriteAtlas.AddSprite(TileType.RidgeTopRight, 6, 0);
            _spriteAtlas.AddSprite(TileType.RidgeBottomRight, 6, 0);
            _spriteAtlas.AddSprite(TileType.RidgeBottomLeft, 6, 0);

            _spriteAtlas.AddSprite(TileType.ConnectorHorizontal, 4, 3);
            _spriteAtlas.AddSprite(TileType.ConnectorVertical, 3, 4);

            _spriteAtlas.AddSprite(TileType.TopCap, 4, 6);
            _spriteAtlas.AddSprite(TileType.RightCap, 5, 7);
            _spriteAtlas.AddSprite(TileType.BottomCap, 4, 8);
            _spriteAtlas.AddSprite(TileType.LeftCap, 3, 7);
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

                    if (tileType == TileType.None)
                        continue;

                    _spriteAtlas.SetCurrentSprite(tileType);

                    Point tilePosition = new Point(x, y) * new Point(_unitSize, _unitSize); //Offset taking unitSize into account

                    spriteBatch.Draw(
                        _spriteAtlas.SourceTexture,                                                                                             //Sprite sheet
                        new Rectangle(tilePosition.X, tilePosition.Y, _spriteAtlas.SourceRectangle.Width, _spriteAtlas.SourceRectangle.Height), //Adjusted position, width and height of tile
                        _spriteAtlas.SourceRectangle,                                                                                           //Current position of chosen sprite in sprite sheet
                        Color.White);
                }
            }

            _tileMap.Rooms.ForEach(room =>
            {
                room.Connectors.ForEach(connector =>
                {
                    Point tilePosition = new Point(connector.Position.X, connector.Position.Y) * new Point(_unitSize, _unitSize);
                    spriteBatch.Draw(_platformTexture, new Rectangle(tilePosition.X, tilePosition.Y, 24, 24), Color.White);
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
