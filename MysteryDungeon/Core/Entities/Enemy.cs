
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MysteryDungeon.Core.Data;

namespace MysteryDungeon.Core.Entities
{
    public class Enemy : LivingEntity
    {
        public Enemy(Pokemon pokemon)
        {
            //State = EnemyState.Patrolling;
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
