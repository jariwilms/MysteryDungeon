using Microsoft.Xna.Framework;
using System;

namespace MysteryDungeon.Core.Extensions
{
    /// <summary>
    /// Stores object data in a 2-dimensional array 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Grid<T>
    {
        public T[,] Cells { get; protected set; }

        public Vector2 Position { get; protected set; }

        public Vector2 CellSize { get { return _cellSize; } set { _cellSize = value; CellDistance = CellSize + CellGap; } }
        private Vector2 _cellSize;

        public Vector2 CellGap { get { return _cellGap; } set { _cellGap = value; CellDistance = CellSize + CellGap; } }
        private Vector2 _cellGap;

        public Vector2 CellDistance { get; protected set; }

        public int Width { get { return Cells.GetLength(0); } }
        public int Height { get { return Cells.GetLength(1); } }

        public Grid()
        {
            Cells = new T[0, 0];

            Position = Vector2.Zero;

            CellSize = Vector2.One;
            CellGap = Vector2.Zero;
        }

        public Grid(int width, int height, Vector2 cellSize) : this()
        {
            Cells = new T[width, height];

            if (cellSize.X != 0 && cellSize.Y != 0)
                CellSize = cellSize;
            else
                CellSize = Vector2.One;
        }

        public Grid(int width, int height, Vector2 cellSize, Vector2 cellGap) : this(width, height, cellSize)
        {
            CellGap = cellGap;
        }

        /// <summary>
        /// Create a new grid with a given width and height
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void CreateGrid(int width, int height)
        {
            Cells = new T[width, height];
        }

        /// <summary>
        /// Resize the grid to the new width and height
        /// </summary>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        public void ResizeGrid(int newWidth, int newHeight)
        {
            T[,] newCells = new T[newWidth, newHeight];

            int columns = Math.Min(Width, newWidth);
            int rows = Math.Min(Height, newHeight);

            for (int y = 0; y < rows; y++)
                for (int x = 0; x < columns; x++)
                    newCells[x, y] = Cells[x, y];

            Cells = newCells;
        }

        private bool IsOutOfBounds(int x, int y)
        {
            return !(x > -1 && x < Width && y > -1 && y < Height);
        }

        public T GetElement(int x, int y) //fix nullability voor ref en value types
        {
            if (IsOutOfBounds(x, y))
                return default;

            return Cells[x, y];
        }

        public T GetElement(Point gridPosition)
        {
            int x = gridPosition.X;
            int y = gridPosition.Y;

            if (IsOutOfBounds(x, y))
                return default;

            return Cells[x, y];
        }

        public void SetElement(int x, int y, T value)
        {
            if (IsOutOfBounds(x, y))
                return;

            Cells[x, y] = value;
        }

        /// <summary>
        /// Returns the local position of the cell at the given index
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Vector2 CellIndexToLocalPosition(int x, int y)
        {
            if (IsOutOfBounds(x, y))
                return default;

            return new Vector2(x * CellDistance.X, y * CellDistance.Y);
        }

        /// <summary>
        /// Returns the global position of the cell at the given index
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Vector2 CellIndexToGlobalPosition(int x, int y)
        {
            if (IsOutOfBounds(x, y))
                return default;

            return CellIndexToLocalPosition(x, y) + Position;
        }

        /// <summary>
        /// Returns the index of the cell at the given position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Point LocalPositionToCellIndex(int x, int y)
        {
            x = (int)Math.Ceiling(x / CellDistance.X);
            y = (int)Math.Ceiling(y / CellDistance.Y);

            if (IsOutOfBounds(x, y))
                return default;

            return new Point(x, y);
        }

        /// <summary>
        /// Returns the index of the cell at the given global position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Point GlobalPositionToCellIndex(int x, int y)
        {
            return LocalPositionToCellIndex(x, y);
        }
    }
}
