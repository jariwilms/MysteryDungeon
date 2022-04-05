using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MysteryDungeon.Core.Actors;
using MysteryDungeon.Core.Components;
using MysteryDungeon.Core.Data;
using MysteryDungeon.Core.Input;
using System.Collections.Generic;

namespace MysteryDungeon.Core.Map
{
    public class Level
    {
        public Player Player;
        public Enemy Enemy;
        public List<Enemy> Enemies; //enemy wordt later ge-removed => array 

        public static Dungeon Dungeon { get; set; }
        private readonly DungeonGenerator _dungeonGenerator;

        public TurnHandler TurnHandler { get; set; }

        public Level()
        {
            _dungeonGenerator = new DungeonGenerator(DungeonType.Standard);
            Dungeon = _dungeonGenerator.Generate();

            Player = new Player(Pokemon.Chikorita);
            Enemy = new Enemy(Pokemon.Chikorita);
            Enemy.Behaviour.Target = Player;
            Enemy.Behaviour.OnTargetReached = () => { };

            GenerateNewDungeon();

            TurnHandler = new TurnHandler();
            TurnHandler.AddActor(Player);
            TurnHandler.AddActor(Enemy);
            TurnHandler.Start();
        }

        public void GenerateNewDungeon()
        {
            Dungeon = _dungeonGenerator.Generate();

            var gridComponent = Player.GetComponent<GridMovementComponent>(); //ook temporary als ik niet lui ben
            gridComponent.Tilegrid = Dungeon.Tilemap.Tilegrid;
            gridComponent.Stop();

            var enemygrid = Enemy.GetComponent<GridMovementComponent>();
            enemygrid.Tilegrid = Dungeon.Tilemap.Tilegrid;
            enemygrid.Stop();

            var b = Enemy.Behaviour;
            b.PathFinder.SetCharmap(Dungeon.Charmap);

            Player.Transform.Position = Dungeon.Tilemap.Tilegrid.CellIndexToGlobalPosition(Dungeon.GenerateRandomSpawnPoint());
            Enemy.Transform.Position = Dungeon.Tilemap.Tilegrid.CellIndexToGlobalPosition(Dungeon.GenerateRandomSpawnPoint());
        }

        public void Update(GameTime gameTime)
        {
            Dungeon.Update(gameTime);
            Player.Update(gameTime);
            Enemy.Update(gameTime);

            if (InputEventHandler.Instance.IsKeyPressedOnce(Keys.R)) GenerateNewDungeon();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Dungeon.Draw(spriteBatch);
            Player.Draw(spriteBatch);
            Enemy.Draw(spriteBatch);
        }
    }
}
