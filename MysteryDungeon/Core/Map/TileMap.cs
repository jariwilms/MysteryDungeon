using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MysteryDungeon.Core.Characters;
using MysteryDungeon.Core.Extensions;
using MysteryDungeon.Core.Tiles;

namespace MysteryDungeon.Core.Map
{
    /// <summary>
    /// Stores and handles tile assets for creating 2D levels
    /// </summary>
    public class Tilemap : GameObject
    {
        public Grid<Tile> TileGrid;
        public TilemapRenderer TilemapRenderer;

        public int Width { get { return TileGrid.Width; } }
        public int Height { get { return TileGrid.Height; } }

        public Tilemap()
        {
            TileGrid = new Grid<Tile>();
            TileGrid.CellSize = new Vector2(24);

            TilemapRenderer = new TilemapRenderer(Content);
            TilemapRenderer.Render(this);
        }

        public void ActivateTile(Level dungeon, Actor actor)
        {
            Point actorPosition = actor.Transform.Position.ToPoint();
            Point index = TileGrid.GlobalPositionToCellIndex(actorPosition.X, actorPosition.Y);

            TileGrid.GetElement(index).Activate(dungeon, actor);
        }

        public override void Update(GameTime gameTime)
        {
            TilemapRenderer.Update(gameTime); //doet niks atm
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            TilemapRenderer.Draw(spriteBatch);
        }
    }
}
