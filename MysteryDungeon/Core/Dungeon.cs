using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MysteryDungeon.Core.Characters;
using MysteryDungeon.Core.GUI;
using MysteryDungeon.Core.Map;
using System;
using System.Diagnostics;

namespace MysteryDungeon.Core
{
    public class Dungeon : IDisposable
    {
        public Player Player;

        public TileMap TileMap;
        private readonly TileMapRenderer _tileMapRenderer;
        private readonly TileMapGenerator _tileMapGenerator;

        private readonly ContentManager _content;

        public Dungeon(ContentManager content) //TODO: clean up deze dogshit class + leer programmeren
        {
            _content = content;

            _tileMapGenerator = new TileMapGenerator(this, DungeonType.Standard);
            TileMap = _tileMapGenerator.Generate();

            _tileMapRenderer = new TileMapRenderer(content);
            _tileMapRenderer.Render(TileMap);

            Player = new Player(content, this);
            Player.SetPosition(TileMap.SpawnPoint);
            Player.OnMoveFinished += () => { TileMap.TriggerTile(this, Player); };

            Player.CurrentHealth = 20;
            Player.MaxHealth = 30;

            GuiManager.Widgets.Add(new HealthBarWidget(Player));
        }

        public void StairsReached()
        {
            TileMap = _tileMapGenerator.Generate();

            Player.Stop();
            Player.SetPosition(TileMap.SpawnPoint);

            _tileMapRenderer.Render(TileMap);
        }

        public void Update(GameTime gameTime)
        {
            _tileMapRenderer.Update(gameTime);
            Player.Update(gameTime);

            if (Utility.KeyPressedOnce(Keys.R))
            {
                StairsReached();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _tileMapRenderer.Draw(spriteBatch);
            Player.Draw(spriteBatch);
        }

        public void Dispose()
        {
            _content.Unload();
        }
    }
}
