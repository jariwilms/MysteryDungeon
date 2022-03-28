using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MysteryDungeon.Core.Entities;
using MysteryDungeon.Core.Extensions;
using MysteryDungeon.Core.Tiles;

namespace MysteryDungeon.Core.Map
{
    /// <summary>
    /// Stores and handles tile assets for creating 2D levels
    /// </summary>
    public class Tilemap : GameObject
    {
        public Grid<Tile> Tilegrid;
        public TilemapRenderer TilemapRenderer;

        public int Width { get { return Tilegrid.Width; } }
        public int Height { get { return Tilegrid.Height; } }

        public Tilemap()
        {
            Tilegrid = new Grid<Tile>();
            Tilegrid.CellSize = new Vector2(24);

            TilemapRenderer = new TilemapRenderer(this);
            TilemapRenderer.Render(this);
        }

        public void ActivateTile(Level dungeon, Entity actor)
        {
            Point actorPosition = actor.Transform.Position.ToPoint();
            Point index = Tilegrid.GlobalPositionToCellIndex(actorPosition.X, actorPosition.Y);

            Tilegrid.GetElement(index).Activate(dungeon, actor);
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
