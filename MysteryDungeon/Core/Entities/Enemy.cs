using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MysteryDungeon.Core.Components;
using MysteryDungeon.Core.Data;
using MysteryDungeon.Core.Input;
using MysteryDungeon.Core.UI;
using MysteryDungeon.Core.Map;
using System;
using MysteryDungeon.Core.Contracts;
using System.Collections.Generic;
using MysteryDungeon.Core.Extensions;

namespace MysteryDungeon.Core.Entities
{
    public class Enemy : LivingEntity, ITurnActor
    {
        private readonly GridMovementComponent _gridMovementComponent;

        public event Action OnTurnStart;
        public event Action OnTurnFinished;

        public bool IsPerforming { get => _isPerforming; set => _isPerforming = value; }
        private bool _isPerforming;

        private int _turnsLeft;

        public Enemy(Pokemon pokemon) : base(pokemon)
        {
            MaxHealth = 30;
            CurrentHealth = 20;

            _gridMovementComponent = new GridMovementComponent(this);
            _gridMovementComponent.Tilegrid = Level.Dungeon.Tilemap.Tilegrid;
            _gridMovementComponent.OnMoveFinished += () => _turnsLeft--;

            Components.Insert(0, _gridMovementComponent); //maak components list niet visible
        }

        public override void Update(GameTime gameTime)
        {
            AnimatorController.SetParameter("VelocityX", _gridMovementComponent.Velocity.X);
            AnimatorController.SetParameter("VelocityY", _gridMovementComponent.Velocity.Y);
            AnimatorController.SetParameter("ViewDirectionX", _gridMovementComponent.ViewDirection.X);
            AnimatorController.SetParameter("ViewDirectionY", _gridMovementComponent.ViewDirection.Y);

            if (IsPerforming)
                if (_turnsLeft == 0)
                    EndTurn();

            base.Update(gameTime);
        }

        private int _nodeIndex;
        public List<PathNode> Nodes;
        public void StartTurn()
        {
            OnTurnStart?.Invoke();
            IsPerforming = true;

            _turnsLeft = 1;
            _gridMovementComponent.MovementLocked = false;

            if (_nodeIndex < Nodes.Count - 1) //temp btw, ff testen
            {
                _nodeIndex++;

                var moveDirection = Nodes[_nodeIndex].Position - Nodes[_nodeIndex - 1].Position;
                var grid = _gridMovementComponent;

                Action a = moveDirection switch
                {
                    Point(0, -1) => grid.MoveUpAction,
                    Point(1, 0) => grid.MoveRightAction,
                    Point(0, 1) => grid.MoveDownAction,
                    Point(-1, 0) => grid.MoveLeftAction,
                    _ => throw new Exception()
                };

                a?.Invoke();
            }
            else
                EndTurn();
        }

        public void EndTurn()
        {
            _gridMovementComponent.MovementLocked = true;

            IsPerforming = false;
            OnTurnFinished?.Invoke();
        }
    }
}
