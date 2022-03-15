using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MysteryDungeon.Core.Characters;
using MysteryDungeon.Core.Interface;
using MysteryDungeon.Core.Map;
using System;

namespace MysteryDungeon.Core
{
    public class Level : IDisposable
    {
        public Player Player;

        public Tilemap Tilemap;
        private readonly TilemapRenderer _TilemapRenderer;
        private readonly TilemapGenerator _TilemapGenerator;

        private readonly ContentManager _content;

        public Level(ContentManager content) //TODO: clean deze dogshit class up + leer deftig programmeren
        {
            _content = content;

            _TilemapGenerator = new TilemapGenerator(DungeonType.Standard);
            Tilemap = _TilemapGenerator.Generate();

            _TilemapRenderer = new TilemapRenderer(content);
            _TilemapRenderer.Render(Tilemap);

            Player = new Player(content, this);
            Player.SetPosition(Tilemap.SpawnPoint);
            Player.OnMoveFinished += () => { Tilemap.ActivateTile(this, Player); };

            Player.CurrentHealth = 20;
            Player.MaxHealth = 30;

            GUI.Instance.Widgets.Add(new HealthBarWidget(Player));
        }

        public void StairsReached()
        {
            Tilemap = _TilemapGenerator.Generate();
            _TilemapRenderer.Render(Tilemap);

            Player.Stop();
            Player.SetPosition(Tilemap.SpawnPoint);
        }

        public void Update(GameTime gameTime)
        {
            _TilemapRenderer.Update(gameTime);
            Player.Update(gameTime);

            if (Utility.KeyPressedOnce(Keys.R))
            {
                StairsReached();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _TilemapRenderer.Draw(spriteBatch);
            Player.Draw(spriteBatch);
        }

        public void Dispose()
        {
            _content.Unload();
        }
    }
}
