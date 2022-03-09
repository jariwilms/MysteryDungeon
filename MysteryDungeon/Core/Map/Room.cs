using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MysteryDungeon.Core.Map
{
    public enum Direction
    {
        Up = 0b0001,
        Right = 0b0010,
        Down = 0b0100,
        Left = 0b1000,
        All = 0b1111
    }

    public class Room
    {
        public int Id;
        public Tuple<int, int> Index;

        public Rectangle Bounds;

        public HashSet<Room> AdjacencyList;
        public List<Connector> Connectors;
        public bool isJunction;

        private readonly Random _random; //Random item generation to be implemented

        public Room()
        {
            Bounds = new Rectangle();

            AdjacencyList = new HashSet<Room>();
            Connectors = new List<Connector>();

            _random = new Random();
        }

        public Room(int id) : this()
        {
            Id = id;
        }

        public Room(int x, int y, int width, int height) : this()
        {
            Bounds = new Rectangle(x, y, width, height);
        }

        public Room(Rectangle bounds, List<Connector> connectors) : this()
        {
            Bounds = bounds;
            Connectors = connectors;
        }

        public void CreateConnectors(int x, int xMax, int y, int yMax)
        {
            Index = new Tuple<int, int>(x, y);

            if (x > 0) //Create left connector
                Connectors.Add(new Connector(Bounds.X, _random.Next(0, Bounds.Height - 1) + Bounds.Y, Direction.Left));

            if (x < xMax - 1) //Create right connector
                Connectors.Add(new Connector(Bounds.X + Bounds.Width - 1, _random.Next(0, Bounds.Height - 1) + Bounds.Y, Direction.Right));

            if (y > 0) //Create top connector
                Connectors.Add(new Connector(_random.Next(0, Bounds.Width - 1) + Bounds.X, Bounds.Y, Direction.Up));

            if (y < yMax - 1) //Create bottom connector
                Connectors.Add(new Connector(_random.Next(0, Bounds.Width - 1) + Bounds.X, Bounds.Y + Bounds.Height - 1, Direction.Down));
        }

        public Connector GetConnector(Direction direction)
        {
            foreach (Connector c in Connectors)
                if (c.Direction.HasFlag(direction))
                    return c;

            return null;
        }
    }
}
