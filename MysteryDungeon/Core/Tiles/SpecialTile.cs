using Microsoft.Xna.Framework;

namespace MysteryDungeon.Core.Tiles
{
    public abstract class SpecialTile : Tile
    {
        public Point Position;
        public SpecialTileType SpecialTileType;
        public bool IsVisible;

        public SpecialTile(Point position, SpecialTileType specialTileType, bool isVisible = false) : base(TileType.Floor)
        {
            Position = position;
            TileCollision = TileCollision.Passable;
            SpecialTileType = specialTileType;

            IsVisible = isVisible;
        }
    }
}
