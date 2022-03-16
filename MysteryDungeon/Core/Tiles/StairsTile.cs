using MysteryDungeon.Core.Characters;
using MysteryDungeon.Core.Map;
using System;

namespace MysteryDungeon.Core.Tiles
{
    class StairsTile : SpecialTile
    {
        public enum StairDirection
        {
            Up,
            Down,
        }

        public StairsTile(StairDirection stairDirection) : base(TileType.Floor, SpecialTileType.StairsDown)
        {
            SpecialTileType = stairDirection switch
            {
                StairDirection.Up => SpecialTileType.StairsUp,
                StairDirection.Down => SpecialTileType.StairsDown,
                _ => throw new Exception("Invalid stair type.")
            };

            IsVisible = true;
        }

        public override void Activate(Level level, Actor actor)
        {
            level.StairsReached();
        }
    }
}
