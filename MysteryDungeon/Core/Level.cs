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
    public class Level
    {
        public Player Player;

        public Dungeon Dungeon;
        private DungeonGenerator _dungeonGenerator;

        public Level(ContentManager content) //TODO: clean deze dogshit class up + leer deftig programmeren
        {
            _dungeonGenerator = new DungeonGenerator(DungeonType.Standard);
            Dungeon = _dungeonGenerator.Generate();

            Player = new Player(content, this);
            Player.SetPosition(Dungeon.SpawnPoint);
            //Player.OnMoveFinished += () => { Tilemap.ActivateTile(this, Player); };

            Player.CurrentHealth = 20;
            Player.MaxHealth = 30;

            GUI.Instance.Widgets.Add(new HealthBarWidget(Player));
        }

        public void GenerateNewDungeon()
        {
            Dungeon = _dungeonGenerator.Generate();

            Player.Stop();
            Player.SetPosition(Dungeon.SpawnPoint);
        }

        public void StairsReached()
        {
            GenerateNewDungeon();
        }

        public void Update(GameTime gameTime)
        {
            Dungeon.Update(gameTime);
            Player.Update(gameTime);

            if (Utility.KeyPressedOnce(Keys.R))
            {
                StairsReached();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Dungeon.Draw(spriteBatch);
            Player.Draw(spriteBatch);
        }
    }
}
