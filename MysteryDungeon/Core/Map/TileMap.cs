using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MysteryDungeon.Core.Characters;
using MysteryDungeon.Core.Components;
using MysteryDungeon.Core.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MysteryDungeon.Core.Map
{
    /// <summary>
    /// Stores and handles tile assets for creating 2D levels
    /// </summary>
    public class Tilemap : GameObject
    {
        public Tile[,] Tiles;                       //Complete array of all tiles
        public GridComponent GridComponent;

        public TilemapRenderer TilemapRenderer;

        public int Width { get { return Tiles.GetLength(0); } }
        public int Height { get { return Tiles.GetLength(1); } }

        public Tilemap()
        {
            Tiles = new Tile[0, 0];
            GridComponent = new GridComponent(24); //declare unitsize ergens
            TilemapRenderer = new TilemapRenderer(Content, UnitSize);
            TilemapRenderer.Render(this);
        }

        public void ActivateTile(Level dungeon, Actor actor)
        {
            Point tilePosition = new Point((int)(actor.Transform.Position.X / 24), (int)(actor.Transform.Position.Y / 24));
            Tiles[tilePosition.X, tilePosition.Y].Activate(dungeon, actor);
        }

        public override void Update(GameTime gameTime)
        {
            TilemapRenderer.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            TilemapRenderer.Draw(spriteBatch);
        }
    }
}
