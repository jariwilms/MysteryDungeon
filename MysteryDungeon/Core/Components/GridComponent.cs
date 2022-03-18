using MysteryDungeon.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace MysteryDungeon.Core.Components
{
    /// <summary>
    /// Stores dimensional data of the grid layout and provides helper functions to retrieve information about the grid
    /// </summary>
    public class GridComponent : Component
    {
        public Vector2 CellGap { get; }
        public Vector2 CellSize { get; }

        public GridComponent(int cellSize, int cellGap = 0)
        {
            CellSize = new Vector2(cellSize);
            CellGap = new Vector2(cellGap);
        }
        public GridComponent(Vector2 cellSize)
        {
            CellSize = cellSize;
            CellGap = Vector2.Zero;
        }
        public GridComponent(Vector2 cellSize, Vector2 cellGap) : this(cellSize)
        {
            CellGap = cellGap;
        }

        /// <summary>
        ///Returns the cell that is closest to the given position in local space
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Vector2 GetLocalCellPosition(Vector2 position)
            => new Vector2((float)(Math.Round(position.X / CellSize.X) * CellSize.X), (float)(Math.Round(position.Y / CellSize.Y) * CellSize.Y));

        /// <summary>
        /// Returns the cell that is closest to the given position in global space
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Vector2 GetGlobalCellPosition(Vector2 position)
            => GetLocalCellPosition(position) + Transform.Position;

        /// <summary>
        /// Returns the center of the cell with the given position in local space
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Vector2 GetLocalCellCenter(Vector2 position)
            => GetLocalCellPosition(position) + CellSize / 2;

        /// <summary>
        /// Returns the center of the cell with the given position in global space
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Vector2 GetGlobalCellCenter(Vector2 position) 
            => GetGlobalCellPosition(position) + CellSize / 2;
    }
}
