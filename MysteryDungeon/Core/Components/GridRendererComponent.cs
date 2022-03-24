using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MysteryDungeon.Core.Components
{
    /// <summary>
    /// Stores dimensional data of the grid layout and provides helper functions to retrieve information about the grid
    /// </summary>
    public class GridRendererComponent : Component //not used for now
    {
        public Vector2 CellSize { get; }
        public Vector2 CellGap { get; }

        public GridRendererComponent(GameObject parent, int cellSize, int cellGap = 0) : base(parent)
        {
            CellSize = new Vector2(cellSize);
            CellGap = new Vector2(cellGap);
        }
        public GridRendererComponent(GameObject parent, Vector2 cellSize) : base(parent)
        {
            CellSize = cellSize;
            CellGap = Vector2.Zero;
        }
        public GridRendererComponent(GameObject parent, Vector2 cellSize, Vector2 cellGap) : this(parent, cellSize)
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

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
    }
}
