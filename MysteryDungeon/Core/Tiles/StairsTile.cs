using Microsoft.Xna.Framework;
using MysteryDungeon.Core.Actors;
using MysteryDungeon.Core.Map;
using System;

namespace MysteryDungeon.Core.Tiles
{
    internal class StairsTile : SpecialTile
    {
        public enum StairsDirection
        {
            Up,
            Down,
        }

        public StairsTile(TileType tileType, Vector2 position, StairsDirection stairsDirection) : base(tileType, position, TileCollision.Passable)
        {
            SpecialTileType = stairsDirection switch
            {
                StairsDirection.Up => SpecialTileType.StairsUp,
                StairsDirection.Down => SpecialTileType.StairsDown,
                _ => throw new Exception("Invalid stair type.")
            };

            IsVisible = true;
        }

        public override void Activate(Level level, Entity actor)
        {
            level.GenerateNewDungeon();
        }
    }
}
