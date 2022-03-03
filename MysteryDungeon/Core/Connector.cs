using System;
using System.Collections.Generic;
using System.Text;

namespace MysteryDungeon.Core
{
    class Connector
    {
        public Tuple<int, int> Position;
        public Direction Direction;
        public bool IsConnected;

        public Connector(int x, int y, Direction direction)
        {
            Position = new Tuple<int, int>(x, y);
            Direction = direction;
            IsConnected = false;
        }
    }
}
