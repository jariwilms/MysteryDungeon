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

        public Vector2 CellSize { get { return _cellSize; } set { _cellSize = value; TotalCellDistance = CellSize + CellGap; } }
        private Vector2 _cellSize;

        public Vector2 CellGap { get { return _cellGap; } set { _cellGap = value; TotalCellDistance = CellSize + CellGap; } }
        private Vector2 _cellGap;

        public Vector2 TotalCellDistance { get; protected set; }

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

        /// <summary>
        /// Get the element at the given index
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public T GetElement(int x, int y)
        {
            if (IsOutOfBounds(x, y))
                return default;

            return Cells[x, y];
        }
        public T GetElement(Point index)
        {
            int x = index.X;
            int y = index.Y;

            if (IsOutOfBounds(x, y))
                return default;

            return Cells[x, y];
        }

        /// <summary>
        /// Set the element at the given index to a specified value
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="value"></param>
        public void SetElement(int x, int y, T value)
        {
            if (IsOutOfBounds(x, y))
                return;

            Cells[x, y] = value;
        }
        public void SetElement(Point index, T value)
        {
            SetElement(index.X, index.Y, value);
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

            return new Vector2(x * TotalCellDistance.X, y * TotalCellDistance.Y);
        }
        public Vector2 CellIndexToLocalPosition(Point index)
        {
            return CellIndexToLocalPosition(index.X, index.Y);
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
        public Vector2 CellIndexToGlobalPosition(Point index)
        {
            return CellIndexToGlobalPosition(index.X, index.Y);
        }

        /// <summary>
        /// Returns the index of the cell at the given position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Point LocalPositionToCellIndex(int x, int y)
        {
            x = (int)Math.Ceiling(x / TotalCellDistance.X);
            y = (int)Math.Ceiling(y / TotalCellDistance.Y);

            if (IsOutOfBounds(x, y))
                return default;

            return new Point(x, y);
        }
        public Point LocalPositionToCellIndex(Vector2 position)
        {
            return LocalPositionToCellIndex((int)position.X, (int)position.Y);
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
        public Point GlobalPositionToCellIndex(Vector2 position)
        {
            return LocalPositionToCellIndex((int)position.X, (int)position.Y);
        }

        private bool IsOutOfBounds(int x, int y)
            => !(x > -1 && x < Width && y > -1 && y < Height);
    }
}
