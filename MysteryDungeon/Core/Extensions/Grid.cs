using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryDungeon.Core.Extensions
{
    /// <summary>
    /// Stores object data in a 2-dimensional array 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Grid<T>
    {
        public T[,] Elements { get; protected set; }
        private T[,] _elements;

        public Vector2 Position { get; protected set; }
        public Vector2 CellSize { get; protected set; }
        public Vector2 CellGap { get; protected set; }

        public int Width { get { return _elements.GetLength(0); } }
        public int Height { get { return _elements.GetLength(1); } }

        public Grid(int width, int height, Vector2 cellSize)
        {
            Elements = new T[width, height];

            Position = Vector2.Zero;
            CellSize = cellSize;
            CellGap = Vector2.Zero;
        }

        public Grid(int width, int height, Vector2 cellSize, Vector2 cellGap) : this(width, height, cellSize)
        {
            CellGap = cellGap;
        }

        private bool IsOutOfBounds(int x, int y)
        {
            return x > 0 && x < Width && y > 0 && y < Height;
        }

        public T GetObject(int x, int y)
        {
            if (IsOutOfBounds(x, y))
                return default;

            return _elements[x, y];
        }

        public Vector2 GetObjectLocalPosition(int x, int y)
        {
            if (IsOutOfBounds(x, y))
                return default;

            return new Vector2(
                (x - 1) * CellGap.X + x * CellSize.X,
                (y - 1) * CellGap.Y + y * CellSize.Y);
        }

        public Vector2 GetObjectWorldPosition(int x, int y)
        {
            if (IsOutOfBounds(x, y))
                return default;

            return new Vector2(
                Position.X + (x - 1) * CellGap.X + x * CellSize.X,
                Position.Y + (y - 1) * CellGap.Y + y * CellSize.Y);
        }
    }
}
