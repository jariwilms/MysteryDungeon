using MysteryDungeon.Core.Characters;

namespace MysteryDungeon.Core.Tiles
{
    class FloorTile : Tile
    {
        public FloorTile(TileType tileType) : base(tileType)
        {
            TileCollision = TileCollision.Passable;
        }

        public override void Activate(Player player)
        {
            return;
        }
    }
}
