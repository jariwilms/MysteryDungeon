using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MysteryDungeon.Core.Components;
using MysteryDungeon.Core.Data;
using MysteryDungeon.Core.Entities;
using MysteryDungeon.Core.Extensions;
using MysteryDungeon.Core.Input;
using System.Collections.Generic;

namespace MysteryDungeon.Core.Map
{
    public class Level
    {
        public Player Player;
        public Enemy Enemy;
        public List<Enemy> Enemies;

        public static Dungeon Dungeon { get; set; } //temporary om het makkelijker te makken, voorzie getgameobject functies etc
        private readonly DungeonGenerator _dungeonGenerator;

        private Pathfinder _pathFinder;
        private List<PathNode> _nodes;
        public bool _pathFound;

        private Texture2D _redpng;

        public TurnHandler TurnHandler { get; set; }

        public Level(ContentManager content) //TODO: clean deze dogshit class up + leer deftig programmeren
        {
            _dungeonGenerator = new DungeonGenerator(DungeonType.Standard);
            Dungeon = _dungeonGenerator.Generate();

            Player = new Player(Pokemon.Chikorita);
            Enemy = new Enemy(Pokemon.Chikorita);

            _pathFinder = new Pathfinder();
            _nodes = new List<PathNode>();
            _redpng = content.Load<Texture2D>("particles/red");

            StairsReached();

            Enemy.Nodes = _nodes;

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

            Player.Transform.Position = Dungeon.Tilemap.Tilegrid.CellIndexToGlobalPosition(Dungeon.SpawnPoint.X, Dungeon.SpawnPoint.Y);
            Enemy.Transform.Position = Dungeon.Tilemap.Tilegrid.CellIndexToGlobalPosition(Dungeon.SpawnPoint.X, Dungeon.SpawnPoint.Y);
        }

        private void FindPath()
        {
            _pathFinder.SetCharmap(Dungeon.Charmap);
            Point start = new Point(Dungeon.SpawnPoint.X, Dungeon.SpawnPoint.Y);
            Point destination = new Point(Dungeon.stairsTilePosition.X, Dungeon.stairsTilePosition.Y);

            _pathFound = _pathFinder.FindPath(start, destination, out List<PathNode> nodes);

            if (_pathFound) _nodes = nodes;
            else _nodes.Clear();
        }

        public void StairsReached() //zet om naar LoadNewDungeon voor als de stairs bereikt worden idk
        {
            GenerateNewDungeon();
            FindPath();
        }

        public void Update(GameTime gameTime)
        {
            Dungeon.Update(gameTime);
            Player.Update(gameTime);
            Enemy.Update(gameTime);

            if (InputEventHandler.Instance.IsKeyPressedOnce(Keys.R)) StairsReached();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Dungeon.Draw(spriteBatch);
            Player.Draw(spriteBatch);
            Enemy.Draw(spriteBatch);
        }
    }
}
