using Microsoft.Xna.Framework;
using MysteryDungeon.Core.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MysteryDungeon.Core.Map
{
    public enum DungeonType
    {
        Standard,
        HorizontalCorridor,
        VerticalCorridor,
        CorridorInside,
        CorridorOutside,
    }

    /// <summary>
    /// Generates random Dungeons according to predefined generators
    /// </summary>
    internal class DungeonGenerator
    {
        #region variables
        private Dungeon _dungeon;
        private DungeonType _dungeonType;

        private delegate Dungeon Generator();
        private readonly Generator _generator;

        private int _dungeonWidth;                //Total width of the Dungeon
        private int _dungeonHeight;               //Total height of the Dungeon

        private int _borderSize;                //Size of border around the Dungeon
        private int _minSpaceBetweenRooms;      //Minimum amount of space between rooms

        private int _minRooms;                  //Minimum amount of rooms
        private int _horizontalRooms;           //Number of horizontal rooms
        private int _verticalRooms;             //Number of vertical rooms

        private int _minRoomHorizontalSize;     //Minimum horizontal size of room
        private int _maxRoomHorizontalSize;     //Maximum horizontal size of room
        private int _minRoomVerticalSize;       //Minimum vertical size of room
        private int _maxRoomVerticalSize;       //Maximum vertical size of room

        private int _horizontalRoomBoxSize;     //Horizontal size of Dungeon subdivision => DungeonWidth - (2 * _borderSize) / _horizontalRooms
        private int _verticalRoomBoxSize;       //Vertical size of Dungeon subdivision => DungeonHeight - (2 * _borderSize) / _verticalRooms

        private int _minimumConnections;        //Minimum amount of connections per room
        private List<int> _connectionBias;      //Decides what direction connections are likely to form in
        private int _roomSpawnChance;           //Decides the chance
        private int _connectorSpawnChance;

        private int _specialTileSpawnChance;    //What are the odds of a special tile spawning?
        private int _DungeonDifficulty;           //<= Change to distinct groups of spawnable tiles per Dungeon difficulty?

        private RuleTile _ruleTile;
        private readonly Random _random;
        #endregion

        public DungeonGenerator(DungeonType dungeonType = DungeonType.Standard) //Voeg Dungeondifficulty toe aan ctor => trap spawn chance
        {
            _dungeonType = dungeonType;
            _generator = _dungeonType switch
            {
                DungeonType.Standard => GenerateStandard,
                DungeonType.HorizontalCorridor => GenerateHorizontalCorridor,
                DungeonType.VerticalCorridor => GenerateVerticalCorridor,
                DungeonType.CorridorInside => GenerateCorridorsInside,
                DungeonType.CorridorOutside => GenerateCorridorsOutside,
                _ => throw new Exception("An invalid DungeonType has been given.")
            };

            _random = new Random();
        }

        public void SetDungeonType(DungeonType dungeonType)
        {
            _dungeonType = dungeonType;
        }

        public Dungeon Generate()
        {
            _dungeon = new Dungeon();
            return _generator();
        }

        private Dungeon GenerateStandard()
        {
            _dungeonWidth = 60;
            _dungeonHeight = 36;

            _borderSize = 4;
            _minSpaceBetweenRooms = 3;

            _minRooms = 2;
            _horizontalRooms = 4;
            _verticalRooms = 3;

            _minRoomHorizontalSize = 5;
            _minRoomVerticalSize = 4;

            _maxRoomHorizontalSize = 9;
            _maxRoomVerticalSize = 8;

            _horizontalRoomBoxSize = (_dungeonWidth - 2 * _borderSize - _minSpaceBetweenRooms * (_horizontalRooms - 1)) / _horizontalRooms;
            _verticalRoomBoxSize = (_dungeonHeight - 2 * _borderSize - _minSpaceBetweenRooms * (_verticalRooms - 1)) / _verticalRooms;

            if (_maxRoomHorizontalSize > _horizontalRoomBoxSize)
                _maxRoomHorizontalSize = _horizontalRoomBoxSize;

            if (_maxRoomVerticalSize > _verticalRoomBoxSize)
                _maxRoomVerticalSize = _verticalRoomBoxSize;

            _roomSpawnChance = 50;
            _connectorSpawnChance = 100 - _roomSpawnChance;

            //_minimumConnections = 1;

            FillCharMap();                  //Fill Char Map with '#'

            GenerateRooms();                //Generate rooms with random size and location
            GenerateConnectors();           //Generate room connectors
            ConnectRooms();                 //Connect rooms together
            RemoveUnconnectedJunctions();   //Remove junctions that are not connected to any room

            DrawRoomsOnCharMap();           //Draw every room and hallway on the charmap, this means changing every '#' to '.'
            GenerateTilesFromCharMap();     //Generate tile textures according to charmap data

            GenerateSpawnPoint();           //Generate a spawnpoint in a random room
            GenerateSpecialTiles();

            return _dungeon;
        }

        #region toBeImplemented
        public Dungeon GenerateHorizontalCorridor()
        {
            throw new NotImplementedException();
        }

        public Dungeon GenerateVerticalCorridor()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generates a random Tilemap with a circular pattern
        /// </summary>
        /// <returns></returns>
        public Dungeon GenerateCorridorsInside()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generates a random Tilemap with a plus pattern
        /// </summary>
        /// <returns></returns>
        public Dungeon GenerateCorridorsOutside()
        {
            throw new NotImplementedException();
        }
        #endregion

        private void FillCharMap()
        {
            _dungeon.Charmap = new char[_dungeonWidth, _dungeonHeight];

            for (int y = 0; y < _dungeonHeight; y++)
                for (int x = 0; x < _dungeonWidth; x++)
                    _dungeon.Charmap[x, y] = '#';
        }

        private void GenerateRooms()
        {
            int largeRooms = 0;
            List<int> roomIds = new List<int>(Enumerable.Range(0, _horizontalRooms * _verticalRooms)); //0 - 11
            roomIds = roomIds.OrderBy(id => Guid.NewGuid()).ToList();

            int xIndex = 0;
            int yIndex = 0;
            int roomId;

            foreach (int v in Enumerable.Range(0, _verticalRooms))
            {
                foreach (int h in Enumerable.Range(0, _horizontalRooms))
                {
                    roomId = roomIds.Last();
                    roomIds.RemoveAt(roomIds.Count - 1);

                    xIndex = roomId % _horizontalRooms;
                    yIndex = roomId / _horizontalRooms;

                    Room room = new Room(roomId);

                    if (_random.Next(0, 99) < _connectorSpawnChance && largeRooms > _minRooms - 1) //Create a junction room
                    {
                        room.Bounds.Width = 1;
                        room.Bounds.Height = 1;

                        room.isJunction = true;
                    }
                    else //Create a normal room
                    {
                        largeRooms++;
                        room.Bounds.Width = _random.Next(_minRoomHorizontalSize, _maxRoomHorizontalSize);
                        room.Bounds.Height = _random.Next(_minRoomVerticalSize, _maxRoomVerticalSize);
                    }

                    room.Bounds.X = _random.Next(0, _maxRoomHorizontalSize - room.Bounds.Width) + xIndex * _horizontalRoomBoxSize + _borderSize + xIndex * _minSpaceBetweenRooms;
                    room.Bounds.Y = _random.Next(0, _maxRoomVerticalSize - room.Bounds.Height) + yIndex * _verticalRoomBoxSize + _borderSize + yIndex * _minSpaceBetweenRooms;

                    _dungeon.Rooms.Add(room);
                }
            }

            _dungeon.Rooms = _dungeon.Rooms.OrderBy(room => room.Id).ToList();
            _dungeon.HorizontalRooms = _horizontalRooms;
            _dungeon.VerticalRooms = _verticalRooms;
        }

        private void DrawRoomsOnCharMap()
        {
            _dungeon.Rooms.ForEach(room =>
            {
                for (int y = 0; y < room.Bounds.Height; y++)
                    for (int x = 0; x < room.Bounds.Width; x++)
                        _dungeon.Charmap[x + room.Bounds.X, y + room.Bounds.Y] = '.';
            });
        }

        private void GenerateConnectors()
        {
            Direction direction = Direction.Up | Direction.Right | Direction.Down | Direction.Left;
            int roomNumber = 0;

            for (int v = 0; v < _verticalRooms; v++)
            {
                direction |= Direction.Up | Direction.Down;

                if (v == 0)
                    direction &= ~Direction.Up;

                if (v == _verticalRooms - 1)
                    direction &= ~Direction.Down;

                for (int h = 0; h < _horizontalRooms; h++)
                {
                    direction |= Direction.Right | Direction.Left;

                    if (h == 0)
                        direction &= ~Direction.Left;

                    if (h == _horizontalRooms - 1)
                        direction &= ~Direction.Right;

                    Room room = _dungeon.Rooms[roomNumber];
                    room.Id = roomNumber;

                    roomNumber++;
                }
            }

            roomNumber = 0;

            for (int v = 0; v < _verticalRooms; v++)
            {
                for (int h = 0; h < _horizontalRooms; h++)
                {
                    Room room = _dungeon.Rooms[roomNumber];
                    room.CreateConnectors(h, _horizontalRooms, v, _verticalRooms);
                    roomNumber++;
                }
            }
        }

        public void ConnectRooms()
        {
            if (_dungeon.HorizontalRooms == 0 || _dungeon.VerticalRooms == 0)
                throw new NullReferenceException("Roomsize has not been set");

            _dungeon.Rooms.ForEach(room =>
            {
                if (!room.isJunction)
                {
                    room.Connectors.ForEach(sourceConnector =>
                    {
                        Room destinationRoom = _dungeon.GetDestinationRoom(room, sourceConnector); //Room apart opvragen is required voor HashSet
                        Connector destinationConnector = _dungeon.GetDestinationConnector(destinationRoom, sourceConnector);

                        _dungeon.Connect(sourceConnector, destinationConnector);

                        room.AdjacencyList.Add(destinationRoom);
                        destinationRoom.AdjacencyList.Add(room);
                    });
                }
            });

            _dungeon.CheckGraph();
        }

        public void RemoveUnconnectedJunctions()
        {
            bool anyConnected;

            for (int i = _dungeon.Rooms.Count - 1; i > -1; i--)
            {
                anyConnected = false;

                if (!_dungeon.Rooms[i].isJunction)
                    continue;

                _dungeon.Rooms[i].Connectors.ForEach(connector =>
                {
                    if (connector.IsConnected)
                        anyConnected = true;
                });

                if (!anyConnected)
                {
                    _dungeon.Charmap[_dungeon.Rooms[i].Bounds.X, _dungeon.Rooms[i].Bounds.Y] = '#';
                    _dungeon.Rooms.RemoveAt(i);
                }
            }
        }

        public void GenerateSpawnPoint()
        {
            List<Room> bigRooms = _dungeon.Rooms.Where(room => !room.isJunction).ToList();
            Room room = bigRooms[_random.Next(0, bigRooms.Count - 1)];

            int x = _random.Next(0, room.Bounds.Width - 1) + room.Bounds.X;
            int y = _random.Next(0, room.Bounds.Height - 1) + room.Bounds.Y;

            _dungeon.SpawnPoint = new Point(x, y);
        }

        private void GenerateSpecialTiles()
        {
            List<Room> bigRooms = _dungeon.Rooms.Where(room => !room.isJunction).ToList();
            Room room = bigRooms[_random.Next(0, bigRooms.Count - 1)];

            int x = _random.Next(0, room.Bounds.Width - 1) + room.Bounds.X;
            int y = _random.Next(0, room.Bounds.Height - 1) + room.Bounds.Y;

            _dungeon.Tilemap.Tilegrid.SetElement(x, y, new WonderTile(TileType.Floor, new Vector2(x * 24, y * 24)));

            room = bigRooms[_random.Next(0, bigRooms.Count - 1)];

            x = _random.Next(0, room.Bounds.Width - 1) + room.Bounds.X;
            y = _random.Next(0, room.Bounds.Height - 1) + room.Bounds.Y;

            _dungeon.Tilemap.Tilegrid.SetElement(x, y, new StairsTile(TileType.Floor, new Vector2(x * 24, y * 24), StairsTile.StairDirection.Down));
            _dungeon.stairsTilePosition = new Point(x, y);
        }

        public void GenerateTilesFromCharMap()
        {
            _dungeon.Tilemap.Tilegrid.CreateGrid(_dungeonWidth, _dungeonHeight);

            foreach (int topBorderX in Enumerable.Range(0, _dungeonWidth))
                _dungeon.Tilemap.Tilegrid.SetElement(topBorderX, 0, new Tile(TileType.Walls1_5, new Vector2(topBorderX * 24, 0), TileCollision.Impassable));

            foreach (int bottomBorderX in Enumerable.Range(0, _dungeonWidth))
                _dungeon.Tilemap.Tilegrid.SetElement(bottomBorderX, _dungeonHeight - 1, new Tile(TileType.Walls1_5, new Vector2(bottomBorderX * 24, 0), TileCollision.Impassable));

            foreach (int leftBorderY in Enumerable.Range(0, _dungeonHeight))
                _dungeon.Tilemap.Tilegrid.SetElement(0, leftBorderY, new Tile(TileType.Walls1_5, new Vector2(0, leftBorderY * 24), TileCollision.Impassable));

            foreach (int rightBorderY in Enumerable.Range(0, _dungeonHeight))
                _dungeon.Tilemap.Tilegrid.SetElement(_dungeonWidth - 1, rightBorderY, new Tile(TileType.Walls1_5, new Vector2(0, rightBorderY * 24), TileCollision.Impassable));

            _ruleTile = new RuleTile(_dungeon.Charmap);
            CreateRules();

            for (int y = 1; y < _dungeonHeight - 1; y++) //Loop over every tile
            {
                for (int x = 1; x < _dungeonWidth - 1; x++)
                {
                    TileType type = _ruleTile.Match(x, y);
                    if (type == TileType.Floor)
                        _dungeon.Tilemap.Tilegrid.SetElement(x, y, new Tile(type, new Vector2(x * 24, y * 24), TileCollision.Passable));
                    else
                        _dungeon.Tilemap.Tilegrid.SetElement(x, y, new Tile(type, new Vector2(x * 24, y * 24), TileCollision.Impassable));
                }
            }
        }

        private void CreateRules() //Visueel zou dit een pak gemakkelijker zijn honestly
        {
            //order is important, rules take precedence in order of declaration
            _ruleTile.AddRule(new Rule(TileType.Floor, "....-....")); //+ means solid tile, - means walkable tile
            _ruleTile.AddRule(new Rule(TileType.Walls1_5, "+++++++++")); //block



            _ruleTile.AddRule(new Rule(TileType.Walls2_1, "--.-++.+-")); //group 2 corners
            _ruleTile.AddRule(new Rule(TileType.Walls2_3, ".--++--+."));
            _ruleTile.AddRule(new Rule(TileType.Walls2_6, ".+--++--."));
            _ruleTile.AddRule(new Rule(TileType.Walls2_7, "-+.++-.--"));

            _ruleTile.AddRule(new Rule(TileType.Walls2_2, ".-.+++.-.")); //group 2 walls
            _ruleTile.AddRule(new Rule(TileType.Walls2_4, ".+.-+-.+."));
            _ruleTile.AddRule(new Rule(TileType.Walls2_5, "----+----"));



            _ruleTile.AddRule(new Rule(TileType.Walls9_1, "-+-+++-++")); //group 9
            _ruleTile.AddRule(new Rule(TileType.Walls9_2, "-+-+++++-"));
            _ruleTile.AddRule(new Rule(TileType.Walls9_3, "-+++++-+-"));
            _ruleTile.AddRule(new Rule(TileType.Walls9_4, "++-+++-+-"));
            _ruleTile.AddRule(new Rule(TileType.Walls9_5, "-+++++++-"));
            _ruleTile.AddRule(new Rule(TileType.Walls9_6, "++-+++-++"));



            _ruleTile.AddRule(new Rule(TileType.Walls8_1, ".-.+++++-")); //group 8
            _ruleTile.AddRule(new Rule(TileType.Walls8_2, ".-.+++-++"));
            _ruleTile.AddRule(new Rule(TileType.Walls8_3, "++-+++.-."));
            _ruleTile.AddRule(new Rule(TileType.Walls8_4, "-+++++.-."));



            _ruleTile.AddRule(new Rule(TileType.Walls7_1, ".++-++.+-")); //group 7
            _ruleTile.AddRule(new Rule(TileType.Walls7_2, "++.++--+."));
            _ruleTile.AddRule(new Rule(TileType.Walls7_3, ".+--++.++"));
            _ruleTile.AddRule(new Rule(TileType.Walls7_4, "-+.++-++."));



            _ruleTile.AddRule(new Rule(TileType.Walls6_1, "++++++++-")); //group 6
            _ruleTile.AddRule(new Rule(TileType.Walls6_2, "++++++-++"));
            _ruleTile.AddRule(new Rule(TileType.Walls6_3, "++-++++++"));
            _ruleTile.AddRule(new Rule(TileType.Walls6_4, "-++++++++"));



            _ruleTile.AddRule(new Rule(TileType.Walls5_1, "++++++-+-")); //group 5
            _ruleTile.AddRule(new Rule(TileType.Walls5_2, "++-+++++-"));
            _ruleTile.AddRule(new Rule(TileType.Walls5_3, "-+++++-++"));
            _ruleTile.AddRule(new Rule(TileType.Walls5_4, "-+-++++++"));



            _ruleTile.AddRule(new Rule(TileType.Walls4_1, ".-.+++-++")); //group 4
            _ruleTile.AddRule(new Rule(TileType.Walls4_2, ".+--++.+-"));
            _ruleTile.AddRule(new Rule(TileType.Walls4_3, "-+.++--+."));
            _ruleTile.AddRule(new Rule(TileType.Walls4_4, "-+-+++.-."));



            _ruleTile.AddRule(new Rule(TileType.Walls3_1, "----+-.+.")); //group 3
            _ruleTile.AddRule(new Rule(TileType.Walls3_2, "--.-++--."));
            _ruleTile.AddRule(new Rule(TileType.Walls3_4, ".--++-.--"));
            _ruleTile.AddRule(new Rule(TileType.Walls3_5, ".+.-+----"));
            _ruleTile.AddRule(new Rule(TileType.Walls3_3, "-+-+++-+-")); //wall cap center



            _ruleTile.AddRule(new Rule(TileType.Walls1_1, "--.-++.++")); //corners
            _ruleTile.AddRule(new Rule(TileType.Walls1_3, ".--++-++."));
            _ruleTile.AddRule(new Rule(TileType.Walls1_7, ".++-++--."));
            _ruleTile.AddRule(new Rule(TileType.Walls1_9, "++.++-.--"));
            _ruleTile.AddRule(new Rule(TileType.Walls1_2, "...++++++")); //walls
            _ruleTile.AddRule(new Rule(TileType.Walls1_4, ".++.++.++"));
            _ruleTile.AddRule(new Rule(TileType.Walls1_6, "++.++.++."));
            _ruleTile.AddRule(new Rule(TileType.Walls1_8, "++++++..."));
        }
    }
}
