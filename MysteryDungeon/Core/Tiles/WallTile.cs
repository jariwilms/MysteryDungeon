using MysteryDungeon.Core.Characters;

namespace MysteryDungeon.Core.Tiles
{
    class WallTile : Tile
    {
        public WallTile(TileType tileType) : base(tileType)
        {

        }

        public override void Activate(Level level, Player player)
        {
            return;
        }
    }
}
