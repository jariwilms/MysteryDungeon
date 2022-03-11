using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MysteryDungeon.Core.Animations;
using MysteryDungeon.Core.Tiles;
using System;

namespace MysteryDungeon.Core.Characters
{
    public enum MovementDirection
    {
        None,

        North,
        East,
        South,
        West,

        NorthEast,
        SouthEast,
        SouthWest,
        NorthWest,
    }

    public enum AnimationStates //not used for now
    {
        Idle,

        MoveUp,
        MoveRight,
        MoveDown,
        MoveLeft,

        AttackUp,
        AttackRight,
        AttackDown,
        AttackLeft,
    }

    public class Actor : Component
    {
        public Dungeon Dungeon;
        public Sprite Sprite;

        public delegate void OnSpawnEvent();
        public delegate void OnDefeatEvent();
        public delegate void OnMoveStartEvent();
        public delegate void OnMoveFinishedEvent();

        public event OnSpawnEvent OnSpawn;
        public event OnDefeatEvent OnDefeat;
        public event OnMoveStartEvent OnMoveStart;
        public event OnMoveFinishedEvent OnMoveFinished;

        protected float _lerpDuration = 0.22f;
        protected float _timeElapsed = 0.0f;

        protected Vector2 _startPosition;
        protected Vector2 _endPosition;

        public bool CanMove = true; //Voorlopig zijn deze vars altijd het tegenovergestelde van elkaar
        public bool IsMoving = false;

        protected Action MoveUpAction; //change to Command?
        protected Action MoveRightAction;
        protected Action MoveDownAction;
        protected Action MoveLeftAction;

        public Actor() : base()
        {

        }

        protected virtual void CreateAnimations()
        {

        }

        //Move to sprite class + override, also put in list of actions => foreach iteration
        protected virtual void CreateActions()
        {

        }

        protected virtual void MoveTo(MovementDirection movementDirection)
        {
            if (IsMoving || !CanMove || !CanMoveInDirection(movementDirection))
                return;

            OnMoveStart?.Invoke();
            CanMove = false;

            _startPosition = Transform.Position;
            _endPosition = _startPosition + movementDirection switch
            {
                MovementDirection.North => new Vector2(0, -UnitSize),
                MovementDirection.East => new Vector2(UnitSize, 0),
                MovementDirection.South => new Vector2(0, UnitSize),
                MovementDirection.West => new Vector2(-UnitSize, 0),
                _ => throw new Exception("The requested direction does not exist"),
            };

            string animationIdentifier = movementDirection switch
            {
                MovementDirection.North => "MoveUp",
                MovementDirection.East => "MoveRight",
                MovementDirection.South => "MoveDown",
                MovementDirection.West => "MoveLeft",
                _ => throw new Exception(string.Format("The requested direction does not exist: {0}", movementDirection)),
            };

            Sprite.AnimationPlayer.PlayAnimation(animationIdentifier);
        }

        protected void LerpToDestination(GameTime gameTime)
        {
            if (_timeElapsed < _lerpDuration)
            {
                Transform.Position = Vector2.Lerp(_startPosition, _endPosition, _timeElapsed / _lerpDuration);
                _timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                OnMoveFinished?.Invoke();

                Transform.Position = _endPosition;
                _timeElapsed = 0;

                CanMove = true;
            }
        }

        /// <summary>
        /// Checks if it is possible to move in the given direction
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        private bool CanMoveInDirection(MovementDirection direction) //TODO: DIRECTION ENUM IDK
        {
            Point directionPoint = direction switch
            {
                MovementDirection.North => new Point(0, -1),
                MovementDirection.East => new Point(1, 0),
                MovementDirection.South => new Point(0, 1),
                MovementDirection.West => new Point(-1, 0),
                _ => throw new ArgumentException("The given direction does not exist.")
            };

            Vector2 tilePosition = new Vector2((int)Transform.Position.X / UnitSize, (int)Transform.Position.Y / UnitSize) + new Vector2(directionPoint.X, directionPoint.Y);

            if (Dungeon.TileMap.Tiles[(int)tilePosition.X, (int)tilePosition.Y].TileCollision == TileCollision.Passable)
                return true;

            return false;
        }

        public override void Update(GameTime gameTime)
        {
            if (IsMoving) //Gaat niet meer werken wanneer non grid-based movement geimplementeerd wordt
                LerpToDestination(gameTime);

            Sprite.Update(gameTime, Transform);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Sprite.Draw(spriteBatch, gameTime);
        }
    }
}
