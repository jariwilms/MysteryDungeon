using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MysteryDungeon.Core.Animations;
using MysteryDungeon.Core.Tiles;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace MysteryDungeon.Core.Map
{
    class TilemapRenderer //Convert to singleton
    {
        public Tilemap Tilemap { get; private set; }

        private SpriteAtlas<TileType> _dungeonAtlas;
        private SpriteAtlas<SpecialTileType> _specialAtlas;
        
        private int _unitSize;

        public TilemapRenderer(ContentManager content)
        {
            _unitSize = Int32.Parse(ConfigurationManager.AppSettings.Get("UnitSize"));

            Texture2D dungeonTexture = content.Load<Texture2D>("tiles/tiny_woods");
            Texture2D specialTexture = content.Load<Texture2D>("tiles/special_tiles");

            _dungeonAtlas = new SpriteAtlas<TileType>(dungeonTexture, new Point(9, 163), 1, 1, 24);
            _specialAtlas = new SpriteAtlas<SpecialTileType>(specialTexture, new Point(0, 0), 1, 1, 24);

            SetupAtlas();
        }

        private void SetupAtlas()
        {
            _dungeonAtlas.AddSprite(TileType.Floor, 13, 1);
            _dungeonAtlas.AddSprite(TileType.Wall, 6, 0); //not used yet
            _dungeonAtlas.AddSprite(TileType.Block, 4, 1);
            _dungeonAtlas.AddSprite(TileType.Pillar, 4, 4);

            _dungeonAtlas.AddSprite(TileType.LedgeTop, 4, 0); //rename to wallTop?
            _dungeonAtlas.AddSprite(TileType.LedgeRight, 5, 1);
            _dungeonAtlas.AddSprite(TileType.LedgeBottom, 4, 2);
            _dungeonAtlas.AddSprite(TileType.LedgeLeft, 3, 1);

            _dungeonAtlas.AddSprite(TileType.CornerTopLeft, 3, 0);
            _dungeonAtlas.AddSprite(TileType.CornerTopRight, 5, 0);
            _dungeonAtlas.AddSprite(TileType.CornerBottomRight, 5, 2);
            _dungeonAtlas.AddSprite(TileType.CornerBottomLeft, 3, 2);

            _dungeonAtlas.AddSprite(TileType.RidgeTopLeft, 6, 0);
            _dungeonAtlas.AddSprite(TileType.RidgeTopRight, 6, 0);
            _dungeonAtlas.AddSprite(TileType.RidgeBottomRight, 6, 0);
            _dungeonAtlas.AddSprite(TileType.RidgeBottomLeft, 6, 0);

            _dungeonAtlas.AddSprite(TileType.ConnectorHorizontal, 4, 3);
            _dungeonAtlas.AddSprite(TileType.ConnectorVertical, 3, 4);

            _dungeonAtlas.AddSprite(TileType.TopCap, 4, 6);
            _dungeonAtlas.AddSprite(TileType.RightCap, 5, 7);
            _dungeonAtlas.AddSprite(TileType.BottomCap, 4, 8);
            _dungeonAtlas.AddSprite(TileType.LeftCap, 3, 7);



            _specialAtlas.AddSprite(SpecialTileType.StairsDown, 2, 2);
            _specialAtlas.AddSprite(SpecialTileType.WonderTile, 1, 2);
        }

        public void Render(Tilemap tilemap)
        {
            Tilemap = tilemap;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Point tilePosition;

            for (int y = 0; y < Tilemap.Height; y++)
            {
                for (int x = 0; x < Tilemap.Width; x++)
                {
                    Tile tile = Tilemap.Tiles[x, y];
                    tilePosition = new Point(x, y) * new Point(_unitSize, _unitSize); //Offset taking unitSize into account

                    if (tile.IsSpecial)
                    {
                        SpecialTileType specialTileType = ((SpecialTile)tile).SpecialTileType;
                        _specialAtlas.SetCurrentSprite(specialTileType);

                        spriteBatch.Draw(
                            _specialAtlas.SourceTexture,
                            new Rectangle(tilePosition.X, tilePosition.Y, _specialAtlas.SourceRectangle.Width, _specialAtlas.SourceRectangle.Height),
                            _specialAtlas.SourceRectangle,
                            Color.White);
                    }
                    else
                    {
                        TileType tileType = Tilemap.Tiles[x, y].TileType;
                        _dungeonAtlas.SetCurrentSprite(tileType);

                        spriteBatch.Draw(
                            _dungeonAtlas.SourceTexture,                                                                                                //Sprite sheet
                            new Rectangle(tilePosition.X, tilePosition.Y, _dungeonAtlas.SourceRectangle.Width, _dungeonAtlas.SourceRectangle.Height),   //Adjusted position, width and height of tile
                            _dungeonAtlas.SourceRectangle,                                                                                              //Current position of chosen sprite in sprite sheet
                            Color.White);
                    }
                }
            }
        }
    }
}
