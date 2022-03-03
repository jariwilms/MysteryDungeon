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

        private int _borderSize = 2;            //Size of border around the level
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

            _random = new Random(Guid.NewGuid().GetHashCode());

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
            _minSpaceBetweenRooms = 0;

            _minRooms = 2;
            _horizontalRooms = 4;
            _verticalRooms = 3;

            _horizontalRoomBoxSize = (LevelWidth - 2 * _borderSize - _minSpaceBetweenRooms * (_horizontalRooms - 1)) / _horizontalRooms;
            _verticalRoomBoxSize = (LevelHeight - 2 * _borderSize - _minSpaceBetweenRooms * (_verticalRooms - 1)) / _verticalRooms;

            _minRoomHorizontalSize = 5;
            _minRoomVerticalSize = 4;

            _maxRoomHorizontalSize = 9;
            _maxRoomVerticalSize = 8;

            _roomSpawnChance = 50;
            _connectorSpawnChance = 50;

            _minimumConnections = 1;

            FillCharMap();
            GenerateRooms();
            GenerateTilesFromCharMap();
            GenerateSpawnPoint();

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

        private void GenerateRooms() //Move naar charmap class?
        {
            Direction direction = Direction.Up | Direction.Right | Direction.Down | Direction.Left;
            int roomNumber = 0;
            int bigRooms = 0;

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

                    Room room = new Room(roomNumber);
                   
                    if (_random.Next(0, 99) < _connectorSpawnChance && bigRooms > _minRooms - 1) //Create a connector room
                    {
                        room.Bounds.Width = 1;
                        room.Bounds.Height = 1;

                        room.IsConnector = true;
                    }
                    else //Create a normal room
                    {
                        bigRooms++;
                        room.Bounds.Width = _random.Next(_minRoomHorizontalSize, _maxRoomHorizontalSize);
                        room.Bounds.Height = _random.Next(_minRoomVerticalSize, _maxRoomVerticalSize);
                    }

                    room.Bounds.X = _random.Next(0, _maxRoomHorizontalSize - room.Bounds.Width) + h * _horizontalRoomBoxSize + _borderSize + h * _minSpaceBetweenRooms;
                    room.Bounds.Y = _random.Next(0, _maxRoomVerticalSize - room.Bounds.Height) + v * _verticalRoomBoxSize + _borderSize + v * _minSpaceBetweenRooms;

                    room.CreateConnectors(h, _horizontalRooms, v, _verticalRooms);
                    _tileMap.Rooms.Add(room);

                    roomNumber++;
                }
            }

            //_tileMap.Rooms = _tileMap.Rooms.OrderBy(room => Guid.NewGuid()).ToList();

            _tileMap.Rooms.ForEach(room =>
            {
                for (int y = 0; y < room.Bounds.Height; y++)
                    for (int x = 0; x < room.Bounds.Width; x++)
                        _tileMap.CharMap[x + room.Bounds.X, y + room.Bounds.Y] = '.';
            });



            //Slechste programming in known history wrs
            _tileMap.HorizontalRooms = _horizontalRooms;
            _tileMap.VerticalRooms = _verticalRooms;
            _tileMap.ConnectRooms();
        }

        private void GenerateHallways()
        {

        }

        private void CreateRoomWallsOnCharMap()
        {
            //_tileMap.Rooms.ForEach(room => //Idem
            //{
            //    for (int x = -1; x < room.Bounds.Width + 1; x++)
            //    {
            //        _tileMap.CharMap[x + room.Bounds.X, room.Bounds.Y - 1] = '*';
            //        _tileMap.CharMap[x + room.Bounds.X, room.Bounds.Y + room.Bounds.Height] = '*';
            //    }

            //    for (int y = -1; y < room.Bounds.Height + 1; y++)
            //    {
            //        _tileMap.CharMap[room.Bounds.X - 1, y + room.Bounds.Y] = '*';
            //        _tileMap.CharMap[room.Bounds.X + room.Bounds.Width, y + room.Bounds.Y] = '*';
            //    }
            //});
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
            Room room = _tileMap.Rooms[_random.Next(0, _tileMap.Rooms.Count - 1)];

            int x = _random.Next(0, room.Bounds.Width - 1) + room.Bounds.X;
            int y = _random.Next(0, room.Bounds.Height - 1) + room.Bounds.Y;

            _tileMap.SpawnPoint = new Vector2(x, y);
        }
    }
}
