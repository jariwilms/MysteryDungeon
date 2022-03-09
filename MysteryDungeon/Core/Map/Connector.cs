using Microsoft.Xna.Framework;

namespace MysteryDungeon.Core.Map
{
    public class Connector
    {
        public Point Position;
        public Direction Direction;
        public bool IsConnected;

        public Connector(int x, int y, Direction direction)
        {
            Position = new Point(x, y);
            Direction = direction;
            IsConnected = false;
        }
    }
}
