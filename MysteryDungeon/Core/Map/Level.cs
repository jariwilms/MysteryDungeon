using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MysteryDungeon.Core.Animations;
using MysteryDungeon.Core.Characters;
using MysteryDungeon.Core.Extensions;
using MysteryDungeon.Core.Input;
using MysteryDungeon.Core.Interface;
using System.Collections.Generic;
using MysteryDungeon.Core.Data;
using MysteryDungeon.Core.Components;

namespace MysteryDungeon.Core.Map
{
    public class Level
    {
        public Player Player;
        public List<Enemy> Enemies;

        public Dungeon Dungeon;
        private DungeonGenerator _dungeonGenerator;

        private Pathfinder _pathFinder;
        private List<PathNode> _nodes;
        public bool _pathFound;

        private Texture2D _redpng;

        public Level(ContentManager content) //TODO: clean deze dogshit class up + leer deftig programmeren
        {
            _dungeonGenerator = new DungeonGenerator(DungeonType.Standard);
            Dungeon = _dungeonGenerator.Generate();

            _pathFinder = new Pathfinder();
            _nodes = new List<PathNode>();
            _redpng = content.Load<Texture2D>("particles/red");



            Player = new Player(Pokemon.Chikorita, Dungeon);
            Player.Components.Add(new SpriteRenderer(Player, Player.Sprite));
            Player.OnMoveFinished += () => { Dungeon.Tilemap.ActivateTile(this, Player); };
            Player.SetPosition(Dungeon.SpawnPoint);
        }

        public void GenerateNewDungeon()
        {
            Dungeon = _dungeonGenerator.Generate();

            Player.Stop(); //move naar nieuwe function, moet hier niet staan
            Player.SetPosition(Dungeon.SpawnPoint);
        }

        private void FindPath()
        {
            _pathFinder.SetCharmap(Dungeon.Charmap);
            Point start = new Point((int)Dungeon.SpawnPoint.X, (int)Dungeon.SpawnPoint.Y);
            Point destination = new Point(Dungeon.stairsTilePosition.X, Dungeon.stairsTilePosition.Y);

            if (_pathFinder.FindPath(start, destination, out List<PathNode> nodes))
            {
                _nodes = nodes;
                _pathFound = true;
            }
            else
            {
                _nodes.Clear();
                _pathFound = false;
            }
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

            if (InputEventHandler.Instance.IsKeyPressedOnce(Keys.R))
            {
                StairsReached();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Dungeon.Draw(spriteBatch);
            Player.Draw(spriteBatch);

            foreach (var node in _nodes)
            {
                spriteBatch.Draw(_redpng, new Rectangle(node.Position.X * 24, node.Position.Y * 24, 24, 24), Color.White);
            }
        }
    }
}
