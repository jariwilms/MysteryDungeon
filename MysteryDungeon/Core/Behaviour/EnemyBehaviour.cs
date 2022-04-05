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

        public Action IdleAction { get; set; }

        public Action MoveUpAction { get; set; }
        public Action MoveRightAction;
        public Action MoveDownAction;
        public Action MoveLeftAction;

        public Action OnTargetReached;

        public Pathfinder PathFinder { get; set; }
        private List<PathNode> _pathNodes;
        private Random _random;

        public EnemyBehaviour(LivingEntity parent) : base(parent)
        {
            PathFinder = new Pathfinder();
            _random = new Random();

            ActiveStateStack.Push(Chasing);
        }

        public EnemyBehaviour(LivingEntity parent, Action defaultAction) : this(parent)
        {
            SetNewState(defaultAction);
        }

        public void Idling()
        {
            var targetIndex = Vector2.Divide(Target.Transform.Position, 24);
            var enemyIndex = Vector2.Divide(Parent.Transform.Position, 24);
            var diff = targetIndex - enemyIndex;
            diff = new Vector2(Math.Abs(diff.X), Math.Abs(diff.Y));

            if (diff.X + diff.Y > 3)
                SetNewState(Chasing, true);
            else
                IdleAction?.Invoke();
        }

        public void Sleeping()
        {
            IdleAction?.Invoke();
        }

        public void Wandering()
        {
            var targetIndex = Vector2.Divide(Target.Transform.Position, 24); //move naar eigen functie ipv dit te copy-paste'en als een retard
            var parentIndex = Vector2.Divide(Parent.Transform.Position, 24);
            var diff = targetIndex - parentIndex;
            diff = new Vector2(Math.Abs(diff.X), Math.Abs(diff.Y));

            var distance = diff.X + diff.Y;

            if (distance < 2)
                OnTargetReached?.Invoke();

            //SetNewState(Idling, true);
        }

        public void Chasing()
        {
            var targetIndex = Vector2.Divide(Target.Transform.Position, 24);
            var enemyIndex = Vector2.Divide(Parent.Transform.Position, 24);
            var diff = targetIndex - enemyIndex;
            diff = new Vector2(Math.Abs(diff.X), Math.Abs(diff.Y));

            if (diff.X + diff.Y < 3)
                SetNewState(Idling, true);

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
        }

        public void Attacking()
        {
            throw new NotImplementedException();
        }
    }
}
