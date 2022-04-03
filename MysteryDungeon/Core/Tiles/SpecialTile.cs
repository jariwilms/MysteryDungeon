using Microsoft.Xna.Framework;

namespace MysteryDungeon.Core.Tiles
{
    public abstract class SpecialTile : Tile
    {
        public SpecialTileType SpecialTileType;
        public bool IsVisible;

        protected SpecialTile(TileType tileType, Vector2 position, TileCollision tileCollision = TileCollision.Passable) : base(tileType, position, tileCollision)
        {
            IsSpecial = true;
        }
    }
}
