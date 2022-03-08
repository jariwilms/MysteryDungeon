using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MysteryDungeon.Core
{
    /// <summary>
    /// Contains a representation of a map in a 2-dimensional tile array
    /// </summary>
    class TileMap
    {
        public Tile[,] Tiles;           //Complete array of all tiles
        public char[,] CharMap;         //Text representation of the map

        public List<Room> Rooms;        //Array of rooms
        public List<Hallway> Hallways;  //List of hallways

        public int HorizontalRooms;
        public int VerticalRooms;

        public bool isComplete;
        public Vector2 SpawnPoint;

        public int Width { get { return CharMap.GetLength(0); } }
        public int Height { get { return CharMap.GetLength(1); } }

        private Random _random;

        private delegate void ConnectFunction(Connector source, Connector destination);

        public TileMap()
        {
            Tiles = new Tile[0, 0];
            CharMap = new char[0, 0];

            Rooms = new List<Room>();
            Hallways = new List<Hallway>();

            isComplete = true;

            _random = new Random();
        }

        public Room GetDestinationRoom(Room room, Connector sourceConnector)
        {
            int roomIndex = room.Id;

            roomIndex = sourceConnector.Direction switch
            {
                Direction.Up => roomIndex - HorizontalRooms,
                Direction.Right => roomIndex + 1,
                Direction.Down => roomIndex + HorizontalRooms,
                Direction.Left => roomIndex - 1,
                _ => throw new Exception()
            };

            return Rooms[roomIndex];
        }

        public Connector GetDestinationConnector(Room adjacentRoom, Connector sourceConnector)
        {
            Direction inverse = sourceConnector.Direction switch
            {
                Direction.Up => Direction.Down,
                Direction.Right => Direction.Left,
                Direction.Down => Direction.Up,
                Direction.Left => Direction.Right,
                _ => throw new Exception()
            };

            return adjacentRoom.Connectors.First(connector => connector.Direction.HasFlag(inverse));

            throw new Exception();
        }

        public void Connect(Connector source, Connector destination)
        {
            if (source.IsConnected || destination.IsConnected)
                return;

            Point currentPosition = source.Position;
            Point positionIncrement;

            int totalDistanceA;
            int totalDistanceB;

            if (source.Direction.HasFlag(Direction.Up) || source.Direction.HasFlag(Direction.Down))
            {
                totalDistanceA = destination.Position.Y - source.Position.Y;
                totalDistanceB = destination.Position.X - source.Position.X;
            }
            else if (source.Direction.HasFlag(Direction.Left) || source.Direction.HasFlag(Direction.Right))
            {
                totalDistanceA = destination.Position.X - source.Position.X;
                totalDistanceB = destination.Position.Y - source.Position.Y;
            }
            else
            {
                throw new Exception();
            }

            int minBranchingDistance = 2; //TODO: check level generation for minimum room distance + juiste offsets => minBranchingDistance is soms groter dan totalDistanceA
            int stepsBeforeBranching = _random.Next(minBranchingDistance, Math.Abs(totalDistanceA) - minBranchingDistance);

            int stepsA = stepsBeforeBranching;
            int stepsB = Math.Abs(totalDistanceB);
            int stepsC = Math.Abs(totalDistanceA) - stepsBeforeBranching;

            positionIncrement = source.Direction switch
            {
                Direction.Up => new Point(0, -1),
                Direction.Right => new Point(1, 0),
                Direction.Down => new Point(0, 1),
                Direction.Left => new Point(-1, 0),
                _ => throw new Exception()
            };

            for (int a = 0; a < stepsA; a++)
            {
                currentPosition.X += positionIncrement.X;
                currentPosition.Y += positionIncrement.Y;
                CharMap[currentPosition.X, currentPosition.Y] = '.';
            }

            positionIncrement = source.Direction switch
            {
                Direction.Up or Direction.Down => totalDistanceB > 0 ? new Point(1, 0) : new Point(-1, 0),
                Direction.Left or Direction.Right => totalDistanceB > 0 ? new Point(0, 1) : new Point(0, -1),
                _ => throw new Exception()
            };

            for (int b = 0; b < stepsB; b++)
            {
                currentPosition.X += positionIncrement.X;
                currentPosition.Y += positionIncrement.Y;
                CharMap[currentPosition.X, currentPosition.Y] = '.';
            }

            positionIncrement = source.Direction switch
            {
                Direction.Up => new Point(0, -1),
                Direction.Right => new Point(1, 0),
                Direction.Down => new Point(0, 1),
                Direction.Left => new Point(-1, 0),
                _ => throw new Exception()
            };

            for (int c = 0; c < stepsC; c++)
            {
                currentPosition.X += positionIncrement.X;
                currentPosition.Y += positionIncrement.Y;
                CharMap[currentPosition.X, currentPosition.Y] = '.';
            }

            source.IsConnected = true;
            destination.IsConnected = true;
        }

        public void CheckGraph()
        {
            Dictionary<int, bool> connectedRoomsAlpha = new Dictionary<int, bool>();
            Rooms.ForEach(room => { connectedRoomsAlpha.Add(room.Id, false); });

            Stack<Room> roomStack = new Stack<Room>(Rooms.Count);
            roomStack.Push(Rooms.First());
            bool isVisited;

            while (roomStack.Count > 0)
            {
                Room removedRoom = roomStack.Pop();

                foreach (Room r in removedRoom.AdjacencyList)
                {
                    isVisited = false;

                    if (connectedRoomsAlpha.TryGetValue(r.Id, out isVisited))
                    {
                        if (!isVisited)
                        {
                            connectedRoomsAlpha[r.Id] = true;
                            roomStack.Push(r);
                        }
                    }
                }
            }

            foreach (bool value in connectedRoomsAlpha.Values)
            {
                if (!value)
                {
                    isComplete = false;
                    break;
                }
            }
        }
    }
}
