using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace MysteryDungeon.Core
{
    class Hallway
    {
        public Room Room1;
        public Room Room2;
        public List<Vector2> Points;

        public Hallway()
        {
            Room1 = new Room();
            Room2 = new Room();
            Points = new List<Vector2>();
        }
    }
}
