using Microsoft.Xna.Framework;
using MysteryDungeon.Core.Entities;
using MysteryDungeon.Core.Map;
using System;

namespace MysteryDungeon.Core.Tiles
{
    internal class StairsTile : SpecialTile
    {
        public enum StairDirection
        {
            Up,
            Down,
        }

        public StairsTile(TileType tileType, Vector2 position, StairDirection stairsDirection) : base(tileType, position, TileCollision.Passable)
        {
            SpecialTileType = stairsDirection switch
            {
                StairDirection.Up => SpecialTileType.StairsUp,
                StairDirection.Down => SpecialTileType.StairsDown,
                _ => throw new Exception("Invalid stair type.")
            };

            IsVisible = true;
        }

        public override void Activate(Level level, Entity actor)
        {
            level.StairsReached();
        }
    }
}
