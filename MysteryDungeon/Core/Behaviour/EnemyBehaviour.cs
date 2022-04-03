using Microsoft.Xna.Framework;
using MysteryDungeon.Core.Components;
using MysteryDungeon.Core.Entities;
using MysteryDungeon.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryDungeon.Core.Behaviour
{
    public class EnemyBehaviour : Behaviour
    {
        public Point Origin;
        public Point Destination;

        public float SightRange { get; protected set; }
        public float AttackRange { get; protected set; }

        private Pathfinder _pathFinder;
        private Random _random;

        int _lastHealth;
        int _turnsPassedSinceTargetSeen;

        public EnemyBehaviour(LivingEntity parent, LivingEntity target) : base(parent, target)
        {
            _pathFinder = new Pathfinder();
            _random = new Random();

            ActiveStateStack.Push(Wandering);
        }

        public void Idling()
        {
            //skip turn here
        }

        public void Sleeping()
        {
            //keep sleeping until random tick or taking damage

            if (_lastHealth > Parent.CurrentHealth) //If damage is taken in any form
            {
                ActiveStateStack.Pop();
                ActiveStateStack.Push(Chasing); //maak startchasing transition om target te zoeken, chasing checkt dan gewoon voor distance etc
            }

            if (_random.Next(1, 100) > 99)
            {
                ActiveStateStack.Pop();
                ActiveStateStack.Push(Wandering);
            }
        }

        public void Wandering()
        {
            //if cansee target
            //  stack.push chasing
        }

        public void Chasing()
        {
            //if cansee target
            //  move to target
            //if in range with attack
            //  setstate attacking
            //if cant see target
            //  popstate
            //  set state wandering
        }

        public void Attacking()
        {
            //if in range of target
            //  if any attack in range
            //      do attack
            //if not in range with any move
            //  chasing
        }

        public void Update(GameTime gameTime)
        {
            ActiveStateStack.Peek()?.Invoke();
        }
    }
}
