using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MysteryDungeon.Core.Animations;
using MysteryDungeon.Core.Extensions;
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

    public abstract class Actor : GameObject
    {
        public Level Level { get; protected set; }
        public Sprite Sprite { get; protected set; }

        public event Action OnSpawn;
        public event Action OnDefeat;
        public event Action OnMoveStart;
        public event Action OnMoveFinished;

        public event Action OnTurnStart;
        public event Action OnTurnFinished;

        public float LerpDuration { get { return _lerpDuration; } set { if (value > 0) _lerpDuration = value; } }
        private float _lerpDuration;
        protected float TimeElapsed;

        protected Vector2 StartPosition;
        protected Vector2 EndPosition;

        public bool CanMove = true; //Voorlopig zijn deze vars altijd het tegenovergestelde van elkaar
        public bool IsMoving = false;
        public bool IsLerping = false;
        public bool SkipLerp = false;

        protected Action MoveUpAction; //change to Command?
        protected Action MoveRightAction;
        protected Action MoveDownAction;
        protected Action MoveLeftAction;

        public Actor() : base()
        {
            LerpDuration = 0.2f;
            TimeElapsed = 0.0f;
        }

        public void Stop()
        {
            Transform.Position = EndPosition;

            CanMove = true;
            IsMoving = false;
            IsLerping = false;

            TimeElapsed = 0;

            OnMoveFinished?.Invoke();
        }

        protected virtual void CreateAnimations() //laat buitenstaande class animations toevoegen
        {

        }

        //Move to sprite class + override, also put in list of actions => foreach iteration
        protected virtual void CreateActions()
        {

        } //insgelijks

        protected virtual void MoveTo(MovementDirection movementDirection)
        {
            if (IsMoving || !CanMove || !CanMoveInDirection(movementDirection))
                return;

            StartPosition = Transform.Position;
            EndPosition = StartPosition + movementDirection switch
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
                _ => throw new Exception(String.Format("The requested direction does not exist: {0}", movementDirection)),
            };

            IsMoving = true;
            CanMove = false;
            IsLerping = true;

            OnMoveStart?.Invoke();

            Sprite.AnimationPlayer.PlayAnimation(animationIdentifier);
        }

        protected void LerpToDestination(GameTime gameTime)
        {
            if (TimeElapsed < LerpDuration && !SkipLerp)
            {
                Transform.Position = Vector2.Lerp(StartPosition, EndPosition, TimeElapsed / LerpDuration);
                TimeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                Stop();

                CanMove = true;
                SkipLerp = false;
            }
        }

        /// <summary>
        /// Checks if it is possible to move in the given direction
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        private bool CanMoveInDirection(MovementDirection direction)
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

            if (Level.Dungeon.Tilemap.Tiles[(int)tilePosition.X, (int)tilePosition.Y].TileCollision == TileCollision.Passable)
                return true;

            return false;
        }

        public override void Update(GameTime gameTime)
        {
            if (IsLerping) //Gaat niet meer werken wanneer non grid-based movement geimplementeerd wordt
                LerpToDestination(gameTime);

            Sprite.Update(gameTime, Transform);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch);
        }
    }
}
