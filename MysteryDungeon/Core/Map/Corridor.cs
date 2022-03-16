using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MysteryDungeon.Core.Map
{
    public class Corridor
    {
        public Room Room1;
        public Room Room2;
        public List<Point> Points;

        public Corridor()
        {
            Room1 = new Room();
            Room2 = new Room();
            Points = new List<Point>();
        }
    }
}
