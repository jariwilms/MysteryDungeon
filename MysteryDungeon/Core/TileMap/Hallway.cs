using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MysteryDungeon.Core
{
    class Hallway
    {
        public Room Room1;
        public Room Room2;
        public List<Point> Points;

        public Hallway()
        {
            Room1 = new Room();
            Room2 = new Room();
            Points = new List<Point>();
        }
    }
}
