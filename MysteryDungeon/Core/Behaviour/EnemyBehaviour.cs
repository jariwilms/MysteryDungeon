using Microsoft.Xna.Framework;
using MysteryDungeon.Core.Actors;
using MysteryDungeon.Core.Extensions;
using System;
using System.Collections.Generic;

namespace MysteryDungeon.Core.AI
{
    public class EnemyBehaviour : Behaviour
    {
        public Point Origin;
        public Point Destination;

        public float SightRange { get; protected set; }
        public float AttackRange { get; protected set; }

        public Action MoveUpAction { get; set; }
        public Action MoveRightAction;
        public Action MoveDownAction;
        public Action MoveLeftAction;

        public Pathfinder PathFinder { get; set; }
        private List<PathNode> _pathNodes;
        private Random _random;

        public EnemyBehaviour(LivingEntity parent) : base(parent)
        {
            PathFinder = new Pathfinder();
            _random = new Random();

            ActiveStateStack.Push(Chasing);
        }

        public void Idling()
        {
            //skip turn here
        }

        public void Sleeping()
        {
            //keep sleeping until random tick or taking damage

            //if (_lastHealth > Parent.CurrentHealth) //If damage is taken in any form
            //{
            //    ActiveStateStack.Pop();
            //    ActiveStateStack.Push(Chasing); //maak startchasing transition om target te zoeken, chasing checkt dan gewoon voor distance etc
            //}
            //else if (_random.Next(1, 100) > 99)
            //{
            //    ActiveStateStack.Pop();
            //    ActiveStateStack.Push(Wandering);
            //}
        }

        public void Wandering()
        {
            //if cansee target
            //  stack.push chasing
        }

        public void Chasing()
        {
            bool found = PathFinder.FindPath(Vector2.Divide(Parent.Transform.Position, 24).ToPoint(), Vector2.Divide(Target.Transform.Position, 24).ToPoint(), out _pathNodes);

            if (!found || _pathNodes.Count < 2)
                return;

            var moveDirection = _pathNodes[1].Position - _pathNodes[0].Position;

            Action moveAction = moveDirection switch
            {
                Point(0, -1) => MoveUpAction,
                Point(1, 0) => MoveRightAction,
                Point(0, 1) => MoveDownAction,
                Point(-1, 0) => MoveLeftAction,
                _ => throw new Exception()
            };

            moveAction?.Invoke();

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
    }
}
