using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MysteryDungeon.Core.Data;
using MysteryDungeon.Core.Interface;
using System;

namespace MysteryDungeon.Core.Entities
{
    public class Player : LivingEntity
    {
        public Player(Pokemon pokemon) : base() //move pokemon name naar ctor
        {
            Initialise(pokemon);

            MaxHealth = 30;
            CurrentHealth = 20;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var Speed = _gridMovementComponent.Speed;

            GUI.Instance.QueueStringDraw($"Position: [{Math.Round(Transform.Position.X, 2)}, {Math.Round(Transform.Position.Y, 2)}]", new Vector2(20, 20));
            GUI.Instance.QueueStringDraw($"Speed: [{Math.Round(Speed.X, 2)}, {Math.Round(Speed.Y, 2)}]", new Vector2(20, 50));

            base.Draw(spriteBatch);
        }
    }
}
