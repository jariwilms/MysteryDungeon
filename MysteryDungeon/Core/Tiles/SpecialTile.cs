namespace MysteryDungeon.Core.Tiles
{
    public abstract class SpecialTile : Tile
    {
        public SpecialTileType SpecialTileType;
        public bool IsVisible;

        protected SpecialTile(TileType tileType, SpecialTileType specialTileType) : base(tileType, TileCollision.Passable)
        {
            SpecialTileType = specialTileType;
            IsSpecial = true;
        }
    }
}
