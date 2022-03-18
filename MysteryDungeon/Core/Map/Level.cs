using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MysteryDungeon.Core.Characters;
using MysteryDungeon.Core.Extensions;
using MysteryDungeon.Core.Interface;
using MysteryDungeon.Core.Map;
using System;
using System.Collections.Generic;

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
            Player = new Player(content, this);
            Player.OnMoveFinished += () => { Dungeon.Tilemap.ActivateTile(this, Player); };

            _pathFinder = new Pathfinder();
            _nodes = new List<PathNode>();

            _dungeonGenerator = new DungeonGenerator(DungeonType.Standard);
            StairsReached();

            GUI.Instance.Widgets.Add(new HealthBarWidget(Player));
            _redpng = content.Load<Texture2D>("particles/red");
        }

        public void GenerateNewDungeon()
        {
            Dungeon = _dungeonGenerator.Generate();

            Player.Stop();
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

            if (Utility.KeyPressedOnce(Keys.R))
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
