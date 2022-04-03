using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MysteryDungeon.Core.Components;
using MysteryDungeon.Core.Data;
using MysteryDungeon.Core.Input;
using MysteryDungeon.Core.UI;
using MysteryDungeon.Core.Map;
using System;
using MysteryDungeon.Core.Contracts;

namespace MysteryDungeon.Core.Entities
{
    public class Player : LivingEntity, ITurnActor
    {
        private readonly GridMovementComponent _gridMovementComponent;

        public event Action OnTurnStart;
        public event Action OnTurnFinished;

        public bool IsPerforming { get => _isPerforming; set => _isPerforming = value; }
        private bool _isPerforming;

        private int _turnsLeft;

        public Player(Pokemon pokemon) : base(pokemon) //move pokemon name naar ctor
        {
            MaxHealth = 30;
            CurrentHealth = 20;

            _gridMovementComponent = new GridMovementComponent(this);
            _gridMovementComponent.Tilegrid = Level.Dungeon.Tilemap.Tilegrid;
            _gridMovementComponent.OnMoveFinished += () => _turnsLeft--;
            _gridMovementComponent.OnMoveFinished += () => { if (_turnsLeft == 0) _gridMovementComponent.MovementLocked = true; };

            Components.Insert(0, _gridMovementComponent);

            InputEventHandler.Instance.AddEventListener(KeyAction.Up, _gridMovementComponent.MoveUpAction);
            InputEventHandler.Instance.AddEventListener(KeyAction.Right, _gridMovementComponent.MoveRightAction);
            InputEventHandler.Instance.AddEventListener(KeyAction.Down, _gridMovementComponent.MoveDownAction);
            InputEventHandler.Instance.AddEventListener(KeyAction.Left, _gridMovementComponent.MoveLeftAction);
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

        public override void Draw(SpriteBatch spriteBatch)
        {
            var Speed = _gridMovementComponent.Velocity;
            GUI.Instance.QueueStringDraw($"Position: [{Math.Round(Transform.Position.X, 2)}, {Math.Round(Transform.Position.Y, 2)}]", new Vector2(20, 20));
            //GUI.Instance.QueueStringDraw($"Speed: [{Math.Round(Speed.X, 2)}, {Math.Round(Speed.Y, 2)}]", new Vector2(20, 50));
            GUI.Instance.QueueStringDraw($"MovementLocked?: {_gridMovementComponent.MovementLocked}", new Vector2(20, 80));

            base.Draw(spriteBatch);
        }

        public void StartTurn()
        {
            OnTurnStart?.Invoke();
            IsPerforming = true;

            _turnsLeft = 1;
            _gridMovementComponent.MovementLocked = false;
        }

        public void EndTurn()
        {
            _gridMovementComponent.MovementLocked = true;

            IsPerforming = false;
            OnTurnFinished?.Invoke();
        }
    }
}
