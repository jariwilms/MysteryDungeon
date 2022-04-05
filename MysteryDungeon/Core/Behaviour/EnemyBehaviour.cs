using Microsoft.Xna.Framework;
using MysteryDungeon.Core.Actors;
using MysteryDungeon.Core.Extensions;
using MysteryDungeon.Core.Map;
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

        public EnemyBehaviour(LivingEntity parent) : base(parent)
        {
            PathFinder = new Pathfinder();

            ActiveStateStack.Push(Wandering);
        }

        public EnemyBehaviour(LivingEntity parent, Action defaultAction) : this(parent)
        {
            SetNewState(defaultAction);
        }

        private static int CalculateDistanceInTiles(Vector2 start, Vector2 end)
        {
            var startIndex = Vector2.Divide(start, 24);
            var endIndex = Vector2.Divide(end, 24);
            var difference = (int)Math.Abs(endIndex.X - startIndex.X) + (int)Math.Abs(endIndex.Y - startIndex.Y);

            return difference;
        }

        private bool MoveToDestination(Vector2 origin, Vector2 destination)
        {
            //Otherwise, find a path to the destination
            bool found = PathFinder.FindPath(origin.ToPoint(), destination.ToPoint(), out _pathNodes);

            if (!found || _pathNodes.Count < 2)
                return false;

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
            return true;
        }

        public void Idling()
        {
            var distance = CalculateDistanceInTiles(Parent.Transform.Position, Target.Transform.Position);

            if (distance > 3)
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
            var distance = CalculateDistanceInTiles(Parent.Transform.Position, TargetPosition);

            if (distance < 2) //If we are within 2 tiles of our destination, fire destination reached and idle one turn
                TargetPosition = Vector2.Multiply(Level.Dungeon.GenerateRandomSpawnPoint().ToVector2(), 24);

            if (!MoveToDestination(Vector2.Divide(Parent.Transform.Position, 24), Vector2.Divide(TargetPosition, 24)))
            {
                TargetPosition = Vector2.Multiply(Level.Dungeon.GenerateRandomSpawnPoint().ToVector2(), 24);
                IdleAction?.Invoke();
            }
        }

        public void Chasing()
        {
            var distance = CalculateDistanceInTiles(Parent.Transform.Position, Target.Transform.Position);

            if (distance < 3)
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
