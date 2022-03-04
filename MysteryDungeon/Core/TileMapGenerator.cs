using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MysteryDungeon.Core
{
    public enum LevelType
    {
        Standard,
        HorizontalCorridor,
        VerticalCorridor,
        CorridorInside,
        CorridorOutside,
    }

    /// <summary>
    /// Generates random tilemaps from different level types
    /// </summary>
    class TileMapGenerator
    {
        private TileMap _tileMap;
        private delegate TileMap Generator();
        private Generator _generator;
        private LevelType _levelType;

        public int LevelWidth;                  //Total width of the level
        public int LevelHeight;                 //Total height of the level

        private int _borderSize;            //Size of border around the level
        private int _minSpaceBetweenRooms;      //Minimum amount of space between rooms

        private int _minRooms;                  //Minimum amount of rooms
        private int _horizontalRooms;           //Number of horizontal rooms
        private int _verticalRooms;             //Number of vertical rooms

        private int _minRoomHorizontalSize;     //Minimum horizontal size of room
        private int _maxRoomHorizontalSize;     //Maximum horizontal size of room
        private int _minRoomVerticalSize;       //Minimum vertical size of room
        private int _maxRoomVerticalSize;       //Maximum vertical size of room

        private int _horizontalRoomBoxSize;     //Horizontal size of level subdivision => LevelWidth - (2 * _borderSize) / _horizontalRooms
        private int _verticalRoomBoxSize;       //Vertical size of level subdivision => LevelHeight - (2 * _borderSize) / _verticalRooms

        private int _minimumConnections;        //Minimum amount of connections per room
        private List<int> _connectionBias;      //Decides what direction connections are likely to form to
        private int _roomSpawnChance;           //Decides the chance
        private int _connectorSpawnChance;

        private readonly ContentManager _content;
        private Texture2D _texture;

        private readonly Random _random;

        public TileMapGenerator(ContentManager content, LevelType levelType = LevelType.Standard)
        {
            _content = content;
            _texture = _content.Load<Texture2D>("tiles/BlockA0");

            _random = new Random();

            _levelType = levelType;
            _generator = _levelType switch
            {
                LevelType.Standard => GenerateStandard,
                LevelType.HorizontalCorridor => GenerateHorizontalCorridor,
                LevelType.VerticalCorridor => GenerateVerticalCorridor,
                LevelType.CorridorInside => GenerateCorridorsInside,
                LevelType.CorridorOutside => GenerateCorridorsOutside,
                _ => throw new Exception("An invalid LevelType has been given.")
            };
        }

        public TileMap Generate()
        {
            _tileMap = new TileMap(); //TODO: moet tilemap zonder content weergeven, anderzijds gewoon in renderer proppen?
            return _generator();
        }

        private TileMap GenerateStandard() //Kan omgezet worden naar een functie die enkel settings aanpast en dan een enkele generator functie runt
        {
            LevelWidth = 56;
            LevelHeight = 32;

            _borderSize = 2;
            _minSpaceBetweenRooms = 3;

            _minRooms = 2;
            _horizontalRooms = 4;
            _verticalRooms = 3;

            _minRoomHorizontalSize = 5;
            _minRoomVerticalSize = 4;

            _maxRoomHorizontalSize = 9;
            _maxRoomVerticalSize = 8;

            _horizontalRoomBoxSize = (LevelWidth - 2 * _borderSize - _minSpaceBetweenRooms * (_horizontalRooms - 1)) / _horizontalRooms;
            _verticalRoomBoxSize = (LevelHeight - 2 * _borderSize - _minSpaceBetweenRooms * (_verticalRooms - 1)) / _verticalRooms;

            if (_maxRoomHorizontalSize > _horizontalRoomBoxSize)
                _maxRoomHorizontalSize = _horizontalRoomBoxSize;

            if (_maxRoomVerticalSize > _verticalRoomBoxSize)
                _maxRoomVerticalSize = _verticalRoomBoxSize;

            _roomSpawnChance = 50;
            _connectorSpawnChance = 100 - _roomSpawnChance;

            _minimumConnections = 1;

            FillCharMap();                  //Fill Char Map with '#'

            GenerateRooms();                //Generate rooms with random size and location
            GenerateConnectors();           //Generate room connectors
            ConnectRooms();                 //Connect rooms together
            RemoveUnconnectedJunctions();   //Remove junctions that are not connected to any room

            DrawRoomsOnCharMap();           //Draw every room and hallway on the charmap, this means changing every '#' to '.'
            GenerateTilesFromCharMap();     //Generate tile textures according to charmap data

            GenerateSpawnPoint();           //Generate a spawnpoint in a random room

            return _tileMap;
        }

        #region toBeImplemented
        public TileMap GenerateHorizontalCorridor()
        {
            throw new NotImplementedException();
        }

        public TileMap GenerateVerticalCorridor()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generates a random tilemap with a circular pattern
        /// </summary>
        /// <returns></returns>
        public TileMap GenerateCorridorsInside()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generates a random tilemap with a plus pattern
        /// </summary>
        /// <returns></returns>
        public TileMap GenerateCorridorsOutside()
        {
            throw new NotImplementedException();
        }
        #endregion

        private void FillCharMap()
        {
            _tileMap.CharMap = new char[LevelWidth, LevelHeight];

            for (int y = 0; y < LevelHeight; y++)
                for (int x = 0; x < LevelWidth; x++)
                    _tileMap.CharMap[x, y] = '#';
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

                    room.Bounds.X = _random.Next(0, _maxRoomHorizontalSize - room.Bounds.Width) + xIndex * _horizontalRoomBoxSize + _borderSize + xIndex * _minSpaceBetweenRooms - 1;
                    room.Bounds.Y = _random.Next(0, _maxRoomVerticalSize - room.Bounds.Height) + yIndex * _verticalRoomBoxSize + _borderSize + yIndex * _minSpaceBetweenRooms - 1;

                    _tileMap.Rooms.Add(room);
                }
            }

            _tileMap.Rooms = _tileMap.Rooms.OrderBy(room => room.Id).ToList();
            _tileMap.HorizontalRooms = _horizontalRooms;
            _tileMap.VerticalRooms = _verticalRooms;
        }

        private void DrawRoomsOnCharMap()
        {
            _tileMap.Rooms.ForEach(room =>
            {
                for (int y = 0; y < room.Bounds.Height; y++)
                    for (int x = 0; x < room.Bounds.Width; x++)
                        _tileMap.CharMap[x + room.Bounds.X, y + room.Bounds.Y] = '.';
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

                    Room room = _tileMap.Rooms[roomNumber];
                    room.Id = roomNumber;

                    roomNumber++;
                }
            }

            roomNumber = 0;

            for (int v = 0; v < _verticalRooms; v++)
            {
                for (int h = 0; h < _horizontalRooms; h++)
                {
                    Room room = _tileMap.Rooms[roomNumber];
                    room.CreateConnectors(h, _horizontalRooms, v, _verticalRooms);
                    roomNumber++;
                }
            }
        }

        public void ConnectRooms()
        {
            if (_tileMap.HorizontalRooms == 0 || _tileMap.VerticalRooms == 0)
                throw new NullReferenceException("Roomsize has not been set");

            _tileMap.Rooms.ForEach(room =>
            {
                if (!room.isJunction)
                {
                    room.Connectors.ForEach(sourceConnector =>
                    {
                        Room destinationRoom = _tileMap.GetDestinationRoom(room, sourceConnector); //Room apart opvragen is required voor HashSet
                        Connector destinationConnector = _tileMap.GetDestinationConnector(destinationRoom, sourceConnector);

                        _tileMap.Connect(sourceConnector, destinationConnector);

                        room.AdjacencyList.Add(destinationRoom);
                        destinationRoom.AdjacencyList.Add(room);
                    });
                }
            });

            _tileMap.CheckGraph();
        }

        public void RemoveUnconnectedJunctions()
        {
            bool anyConnected;

            for (int i = _tileMap.Rooms.Count - 1; i > -1; i--)
            {
                anyConnected = false;

                if (!_tileMap.Rooms[i].isJunction)
                    continue;

                _tileMap.Rooms[i].Connectors.ForEach(connector =>
                {
                    if (connector.IsConnected)
                        anyConnected = true;
                });

                if (!anyConnected)
                {
                    _tileMap.CharMap[_tileMap.Rooms[i].Bounds.X, _tileMap.Rooms[i].Bounds.Y] = '#';
                    _tileMap.Rooms.RemoveAt(i);
                }
            }
        }

        public void LoadMapFromFile(string levelPath)
        {
            using StreamReader reader = new StreamReader(levelPath); //Change to level name
            List<string> lines = new List<string>();
            string line = reader.ReadLine();
            int lineWidth = line.Length;

            while (line != null)
            {
                if (line.Length != lineWidth)
                    throw new Exception(String.Format("The length of line {0} is different from all preceeding lines.", lines.Count));

                lines.Add(line);
                line = reader.ReadLine();
            }

            _tileMap.CharMap = new char[lineWidth, lines.Count];

            for (int y = 0; y < lines.Count; y++)
                for (int x = 0; x < lineWidth; x++)
                    _tileMap.CharMap[x, y] = lines[y][x];

            GenerateTilesFromCharMap();
        }

        public void GenerateTilesFromCharMap()
        {
            _tileMap.Tiles = new Tile[LevelWidth, LevelHeight];

            for (int y = 0; y < LevelHeight; y++)
            {
                for (int x = 0; x < LevelWidth; x++)
                {
                    char tileType = _tileMap.CharMap[x, y];
                    _tileMap.Tiles[x, y] = LoadTile(tileType, x, y);
                }
            }
        }

        private Tile LoadTile(char tileType, int x, int y)
        {
            return tileType switch
            {
                '.' => new Tile(null, TileCollision.Passable),//"Air" tiles have no texture or hitbox
                '#' => new Tile(_texture, TileCollision.Impassable),
                '*' => new Tile(_texture, TileCollision.Impassable),
                _ => throw new InvalidDataException(String.Format("Unsupported tile type character '{0}' at position {1}, {2}.", tileType, x, y)),
            };
        }

        public void GenerateSpawnPoint()
        {
            List<Room> bigRooms = _tileMap.Rooms.Where(room => !room.isJunction).ToList();
            Room room = bigRooms[_random.Next(0, bigRooms.Count - 1)];

            int x = _random.Next(0, room.Bounds.Width - 1) + room.Bounds.X;
            int y = _random.Next(0, room.Bounds.Height - 1) + room.Bounds.Y;

            _tileMap.SpawnPoint = new Vector2(x, y);
        }
    }
}
