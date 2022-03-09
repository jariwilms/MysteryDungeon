using MysteryDungeon.Core.Characters;
using System;

namespace MysteryDungeon.Core.Tiles
{
    public class StairsTile : SpecialTile
    {
        public StairsTile(TileType tileType, SpecialType trapType) : base(tileType, trapType)
        {

        }

        public override void Activate(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
