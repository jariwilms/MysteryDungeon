using MysteryDungeon.Core.Entities;
using MysteryDungeon.Core.Map;

namespace MysteryDungeon.Core.Tiles
{
    internal class WonderTile : SpecialTile
    {
        public WonderTile() : base(TileType.Floor, SpecialTileType.WonderTile)
        {
            SpecialTileType = SpecialTileType.WonderTile;

            IsVisible = true;
        }

        public override void Activate(Level level, Entity actor)
        {

        }
    }
}
