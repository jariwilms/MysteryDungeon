using Microsoft.Xna.Framework;
using MysteryDungeon.Core.Entities;
using MysteryDungeon.Core.Map;

namespace MysteryDungeon.Core.Tiles
{
    internal class WonderTile : SpecialTile
    {
        public WonderTile(TileType tileType, Vector2 position) : base(TileType.Floor, position)
        {
            SpecialTileType = SpecialTileType.WonderTile;
            IsVisible = true;
        }

        public override void Activate(Level level, Entity actor)
        {
            //insert logic here, also signature is niet goed
        }
    }
}
