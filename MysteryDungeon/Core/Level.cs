using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MysteryDungeon.Core
{
    class Level : IDisposable
    {
        public Player Player;

        public TileMap TileMap;
        private readonly TileMapRenderer _tileMapRenderer;
        private readonly TileMapGenerator _tileMapGenerator;

        private readonly ContentManager _content;

        public Level(ContentManager content) //TODO: clean up deze dogshit class + leer programmeren
        {
            _content = content;

            Player = new Player(_content.Load<Texture2D>("sprites/player"), this);

            _tileMapGenerator = new TileMapGenerator(LevelType.Standard);
            TileMap = _tileMapGenerator.Generate();

            _tileMapRenderer = new TileMapRenderer(content);
            _tileMapRenderer.Render(TileMap);

            Player.SetPosition(TileMap.SpawnPoint);
        }

        public void Update(GameTime gameTime)
        {
            _tileMapRenderer.Update(gameTime);
            Player.Update(gameTime);

            if (Utility.KeyPressedOnce(Keys.R))
            {
                TileMap = _tileMapGenerator.Generate();
                Player.SetPosition(TileMap.SpawnPoint);
                _tileMapRenderer.Render(TileMap);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _tileMapRenderer.Draw(spriteBatch, gameTime);
            Player.Draw(spriteBatch, gameTime);
        }

        public void Dispose()
        {
            _content.Unload();
        }
    }
}
