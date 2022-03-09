namespace MysteryDungeon.Core.Tiles
{
    public abstract class SpecialTile : Tile
    {
        public SpecialType SpecialType;

        public SpecialTile(TileType tileType, SpecialType trapType) : base(tileType)
        {
            TileCollision = TileCollision.Passable;
            SpecialType = trapType;
        }
    }
}
