using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MysteryDungeon.Core.Animations;
using MysteryDungeon.Core.Tiles;

namespace MysteryDungeon.Core.Map
{
    public class TilemapRenderer : Component
    {
        public Tilemap Tilemap { get; private set; }

        private SpriteAtlas<TileType> _dungeonAtlas;
        private SpriteAtlas<SpecialTileType> _specialAtlas;

        public TilemapRenderer(GameObject parent) : base(parent)
        {
            Texture2D dungeonTexture = Content.Load<Texture2D>("tiles/tiny_woods");
            Texture2D specialTexture = Content.Load<Texture2D>("tiles/special_tiles");

            _dungeonAtlas = new SpriteAtlas<TileType>(dungeonTexture, new Point(9, 163), 1, 1, 24);
            _specialAtlas = new SpriteAtlas<SpecialTileType>(specialTexture, new Point(0, 0), 1, 1, 24);

            SetupAtlas();
        }

        private void SetupAtlas() //Zoek iets meer tedious dan dit
        {
            _dungeonAtlas.AddSprite(TileType.Floor, 13, 1);

            _dungeonAtlas.AddSprite(TileType.Walls1_1, 3, 0);
            _dungeonAtlas.AddSprite(TileType.Walls1_2, 4, 0);
            _dungeonAtlas.AddSprite(TileType.Walls1_3, 5, 0);
            _dungeonAtlas.AddSprite(TileType.Walls1_4, 3, 1);
            _dungeonAtlas.AddSprite(TileType.Walls1_5, 4, 1);
            _dungeonAtlas.AddSprite(TileType.Walls1_6, 5, 1);
            _dungeonAtlas.AddSprite(TileType.Walls1_7, 3, 2);
            _dungeonAtlas.AddSprite(TileType.Walls1_8, 4, 2);
            _dungeonAtlas.AddSprite(TileType.Walls1_9, 5, 2);

            _dungeonAtlas.AddSprite(TileType.Walls2_1, 3, 3);
            _dungeonAtlas.AddSprite(TileType.Walls2_2, 4, 3);
            _dungeonAtlas.AddSprite(TileType.Walls2_3, 5, 3);
            _dungeonAtlas.AddSprite(TileType.Walls2_4, 3, 4);
            _dungeonAtlas.AddSprite(TileType.Walls2_5, 4, 4);
            _dungeonAtlas.AddSprite(TileType.Walls2_6, 3, 4); //exception
            _dungeonAtlas.AddSprite(TileType.Walls2_7, 3, 5);
            _dungeonAtlas.AddSprite(TileType.Walls2_8, 4, 3); //exception
            _dungeonAtlas.AddSprite(TileType.Walls2_9, 5, 5);

            _dungeonAtlas.AddSprite(TileType.Walls3_1, 4, 6);
            _dungeonAtlas.AddSprite(TileType.Walls3_2, 3, 7);
            _dungeonAtlas.AddSprite(TileType.Walls3_3, 4, 7);
            _dungeonAtlas.AddSprite(TileType.Walls3_4, 5, 7);
            _dungeonAtlas.AddSprite(TileType.Walls3_5, 4, 8);

            _dungeonAtlas.AddSprite(TileType.Walls4_1, 4, 9);
            _dungeonAtlas.AddSprite(TileType.Walls4_2, 3, 10);
            _dungeonAtlas.AddSprite(TileType.Walls4_3, 5, 10);
            _dungeonAtlas.AddSprite(TileType.Walls4_4, 4, 11);

            _dungeonAtlas.AddSprite(TileType.Walls5_1, 4, 12);
            _dungeonAtlas.AddSprite(TileType.Walls5_2, 3, 13);
            _dungeonAtlas.AddSprite(TileType.Walls5_3, 5, 13);
            _dungeonAtlas.AddSprite(TileType.Walls5_4, 4, 14);

            _dungeonAtlas.AddSprite(TileType.Walls6_1, 3, 15);
            _dungeonAtlas.AddSprite(TileType.Walls6_2, 4, 15);
            _dungeonAtlas.AddSprite(TileType.Walls6_3, 3, 16);
            _dungeonAtlas.AddSprite(TileType.Walls6_4, 4, 16);

            _dungeonAtlas.AddSprite(TileType.Walls7_1, 3, 17);
            _dungeonAtlas.AddSprite(TileType.Walls7_2, 4, 17);
            _dungeonAtlas.AddSprite(TileType.Walls7_3, 3, 18);
            _dungeonAtlas.AddSprite(TileType.Walls7_4, 4, 18);

            _dungeonAtlas.AddSprite(TileType.Walls8_1, 3, 19);
            _dungeonAtlas.AddSprite(TileType.Walls8_2, 4, 19);
            _dungeonAtlas.AddSprite(TileType.Walls8_3, 3, 20);
            _dungeonAtlas.AddSprite(TileType.Walls8_4, 4, 20);

            _dungeonAtlas.AddSprite(TileType.Walls9_1, 3, 21);
            _dungeonAtlas.AddSprite(TileType.Walls9_2, 4, 21);
            _dungeonAtlas.AddSprite(TileType.Walls9_3, 3, 22);
            _dungeonAtlas.AddSprite(TileType.Walls9_4, 4, 22);
            _dungeonAtlas.AddSprite(TileType.Walls9_5, 3, 23);
            _dungeonAtlas.AddSprite(TileType.Walls9_6, 4, 23);

            _specialAtlas.AddSprite(SpecialTileType.StairsDown, 2, 2);
            _specialAtlas.AddSprite(SpecialTileType.WonderTile, 1, 2);
        }

        public void Render(Tilemap tilemap)
        {
            Tilemap = tilemap;
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Point position;

            for (int y = 0; y < Tilemap.Height; y++)
            {
                for (int x = 0; x < Tilemap.Width; x++)
                {
                    Tile tile = Tilemap.Tilegrid.GetElement(x, y);
                    position = tile.Position.ToPoint();

                    if (tile.IsSpecial)
                    {
                        SpecialTileType specialTileType = ((SpecialTile)tile).SpecialTileType;
                        _specialAtlas.SetCurrentSprite(specialTileType);

                        spriteBatch.Draw(
                            _specialAtlas.SourceTexture,
                            new Rectangle(position.X, position.Y, _specialAtlas.SourceRectangle.Width, _specialAtlas.SourceRectangle.Height),
                            _specialAtlas.SourceRectangle,
                            Color.White);
                    }
                    else
                    {
                        TileType tileType = Tilemap.Tilegrid.GetElement(x, y).TileType;

                        if (tileType == TileType.None)
                            continue;

                        _dungeonAtlas.SetCurrentSprite(tileType);

                        spriteBatch.Draw(
                            _dungeonAtlas.SourceTexture,                                                                                                //Sprite sheet
                            new Rectangle(position.X, position.Y, _dungeonAtlas.SourceRectangle.Width, _dungeonAtlas.SourceRectangle.Height),           //Adjusted position, width and height of tile
                            _dungeonAtlas.SourceRectangle,                                                                                              //Current position of chosen sprite in sprite sheet
                            Color.White);
                    }
                }
            }
        }
    }
}
