using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MysteryDungeon.Core.Characters
{
    class Enemy : Actor //abstract prob
    {
        public enum EnemyState
        {
            Idle, 
            Sleeping, 
            
            Patrolling, 
            Chasing, 
            Attacking, 
        }

        public EnemyState State { get; protected set; }
        public Actor Target;

        public Point Destination;
        public Point WalkPoint;
        public bool WalkPointSet;

        public float SightRange;
        public float AttackRange;
        public bool PlayerInSightRange;

        private Enemy()
        {
            Destination = new Point();
            WalkPoint = new Point();
        }

        public Enemy(Level dungeon, EnemyState enemyState) : this()
        {
            Level = dungeon;

            State = EnemyState.Patrolling;
        }

        public void Tick() //AI actions, updated once per turn
        {
            //If we are sleeping and not attacked => Sleep
            //If we are sleeping and attacked => Attacking

            //If we are patrolling and spot target => Chasing
            //If we are patrolling and reach destination => Patrol to new point
            //If we are patrolling and can't reach destination => Patrol to new point

            //If we are chasing and target < 2 tiles away => Attacking
            //If we are chasing and target > 2 tiles away => Chasing

            //If we are attacking and kill the target => Idle
            //If we are attacking and die => Stop
        }

        protected void Patrol()
        {
            
        }

        public override void Update(GameTime gameTime)
        {


            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            base.Draw(spriteBatch);
        }
    }
}
