using Microsoft.Xna.Framework;
using MysteryDungeon.Core.AI;
using MysteryDungeon.Core.Components;
using MysteryDungeon.Core.Contracts;
using MysteryDungeon.Core.Data;
using MysteryDungeon.Core.Map;
using System;

namespace MysteryDungeon.Core.Actors
{
    public class Enemy : LivingEntity, ITurnActor
    {
        private readonly GridMovementComponent _gridMovementComponent;
        private readonly IntelligenceComponent _intelligenceComponent;

        public event Action OnTurnStart;
        public event Action OnTurnFinished;

        public EnemyBehaviour Behaviour { get; set; }

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

            Behaviour = new EnemyBehaviour(this);

            Behaviour.IdleAction += () => _turnsLeft--;

            Behaviour.MoveUpAction = _gridMovementComponent.MoveUpAction;
            Behaviour.MoveRightAction = _gridMovementComponent.MoveRightAction;
            Behaviour.MoveDownAction = _gridMovementComponent.MoveDownAction;
            Behaviour.MoveLeftAction = _gridMovementComponent.MoveLeftAction;

            _intelligenceComponent = AddComponent<IntelligenceComponent>();
            _intelligenceComponent.Behaviour = Behaviour;
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

        public void StartTurn()
        {
            OnTurnStart?.Invoke();
            IsPerforming = true;

            _turnsLeft = 1;
            _gridMovementComponent.MovementLocked = false;
            _intelligenceComponent.Act();
        }

        public void EndTurn()
        {
            _gridMovementComponent.MovementLocked = true;

            IsPerforming = false;
            OnTurnFinished?.Invoke();
        }
    }
}
