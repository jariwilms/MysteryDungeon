using MysteryDungeon.Core.Characters;

namespace MysteryDungeon.Core.Tiles
{
    public enum TileType
    {
        None,

        Floor,
        Wall,
        Block,
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
        public bool IsSpecial;

        public Tile(TileType tileType, TileCollision tileCollision)
        {
            TileType = tileType;
            TileCollision = tileCollision;
            IsSpecial = false;
        }

        public virtual void Activate(Dungeon dungeon, Actor actor)
        {

        }
    }
}
