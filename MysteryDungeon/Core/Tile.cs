﻿namespace MysteryDungeon.Core
{
    public enum TileType
    {
        None,

        Floor,
        Wall,
        Ceiling,
        Pillar,

        LedgeTop,
        LedgeRight,
        LedgeBottom,
        LedgeLeft,

        CornerTopLeft,
        CornerTopRight,
        CornerBottomRight,
        CornerBottomLeft,

        RidgeTopLeft,
        RidgeTopRight,
        RidgeBottomRight,
        RidgeBottomLeft,

        ConnectorHorizontal,
        ConnectorVertical,


        TopCap,
        RightCap,
        BottomCap,
        LeftCap,

    }

    public enum TileCollision
    {
        Passable = 0,
        Impassable = 1
    }

    class Tile
    {
        public TileType TileType;
        public TileCollision TileCollision;

        public Tile(TileType tileType, TileCollision tileCollision)
        {
            TileType = tileType;
            TileCollision = tileCollision;
        }
    }
}
