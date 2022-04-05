using Microsoft.Xna.Framework;

namespace MysteryDungeon.Core.Map
{
    public class RoomConnector
    {
        public Point Position;
        public Direction Direction;
        public bool IsConnected;

        public RoomConnector(int x, int y, Direction direction)
        {
            Position = new Point(x, y);
            Direction = direction;
            IsConnected = false;
        }
    }
}
