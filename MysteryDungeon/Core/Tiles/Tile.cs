﻿using Microsoft.Xna.Framework;
using MysteryDungeon.Core.Actors;
using MysteryDungeon.Core.Map;

namespace MysteryDungeon.Core.Tiles
{
    public enum TileType
    {
        None,
        Floor,

        Walls1_1,
        Walls1_2,
        Walls1_3,
        Walls1_4,
        Walls1_5,
        Walls1_6,
        Walls1_7,
        Walls1_8,
        Walls1_9,

        Walls2_1,
        Walls2_2,
        Walls2_3,
        Walls2_4,
        Walls2_5,
        Walls2_6,
        Walls2_7,
        Walls2_8,
        Walls2_9,

        Walls3_1,
        Walls3_2,
        Walls3_3,
        Walls3_4,
        Walls3_5,

        Walls4_1,
        Walls4_2,
        Walls4_3,
        Walls4_4,

        Walls5_1,
        Walls5_2,
        Walls5_3,
        Walls5_4,

        Walls6_1,
        Walls6_2,
        Walls6_3,
        Walls6_4,

        Walls7_1,
        Walls7_2,
        Walls7_3,
        Walls7_4,

        Walls8_1,
        Walls8_2,
        Walls8_3,
        Walls8_4,

        Walls9_1,
        Walls9_2,
        Walls9_3,
        Walls9_4,
        Walls9_5,
        Walls9_6,
    }
    public enum SpecialTileType //wordt wrs ge-removed
    {
        WonderTile,         //Resets all stats, regardless if they are positive or negative

        BlastTrap,          //Creates a small damaging explosion
        BigBlastTrap,       //Creates a large damaging explosion

        HungerTrap,         //Makes you more hungry
        PoisonTrap,         //Poisons the pokemon that steps on it

        SealTrap,           //Seals a random move. It can not be used this floor
        SlowTrap,           //Slows your speed for 10 turns
        SlumberTrap,        //Makes you sleep
        SpikeTrap,          //Inflicts a small amount of damage

        StairsUp,           //Stairs with upwards model
        StairsDown,         //Stairs with downwards model
    }
    public enum TileCollision
    {
        Passable = 0,
        Impassable = 1
    }

    public class Tile //Convert naar abstract class, derivatives => groundtile, specialtile, walltile
    {
        public TileType TileType;
        public TileCollision TileCollision;

        public Vector2 Position;

        public bool IsSpecial;

        public Tile(TileType tileType)
        {
            TileType = tileType;
            Position = Vector2.Zero;
            TileCollision = TileCollision.Passable;
            IsSpecial = false;
        }

        public Tile(TileType tileType, Vector2 position, TileCollision tileCollision = TileCollision.Impassable) : this(tileType)
        {
            Position = position;
            TileCollision = tileCollision;
        }

        public virtual void Activate(Level dungeon, Entity actor)
        {

        }
    }
}
