using MysteryDungeon.Core.Characters;

namespace MysteryDungeon.Core.Tiles
{
    class WonderTile : SpecialTile
    {
        public WonderTile() : base(TileType.Floor, SpecialTileType.WonderTile)
        {
            SpecialTileType = SpecialTileType.WonderTile;

            IsVisible = true;
        }

        public override void Activate(Dungeon dungeon, Actor actor)
        {

        }
    }
}
