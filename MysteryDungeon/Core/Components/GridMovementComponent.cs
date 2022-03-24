using Microsoft.Xna.Framework;
using MysteryDungeon.Core.Extensions;
using MysteryDungeon.Core.Input;
using MysteryDungeon.Core.Tiles;
using System;

namespace MysteryDungeon.Core.Components
{
    public enum Direction //remove deze shite
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

    public class GridMovementComponent : Component
    {
        public Grid<Tile> Tilegrid { get; set; }

        public float LerpDuration
        {
            get { return _lerpDuration; }
            set { if (value > 0) _lerpDuration = value; }
        }
        private float _lerpDuration;

        protected float TimeElapsed { get; set; }

        public Vector2 Speed { get; protected set; }
        protected Vector2 CurrentPosition { get; set; }
        protected Vector2 LastPosition { get; set; }

        protected Vector2 StartPosition;
        protected Vector2 EndPosition;

        public bool IsMoving => Math.Abs(Speed.X) > 0.01f || Math.Abs(Speed.Y) > 0.01f;
        public bool CanMove = true;
        public bool IsLerping = false;

        public Action MoveUpAction { get; set; }
        public Action MoveRightAction { get; set; }
        public Action MoveDownAction { get; set; }
        public Action MoveLeftAction { get; set; }

        public GridMovementComponent(GameObject parent) : base(parent)
        {
            LerpDuration = 0.2f;
            TimeElapsed = 0.0f;

            CurrentPosition = new Vector2();
            LastPosition = new Vector2();

            MoveUpAction += () => { MoveToCell(Direction.North); };
            MoveRightAction += () => { MoveToCell(Direction.East); };
            MoveDownAction += () => { MoveToCell(Direction.South); };
            MoveLeftAction += () => { MoveToCell(Direction.West); };

            //move uit component, gaat elke actor laten bewegen on keypress
            InputEventHandler.Instance.AddEventListener(KeyAction.Up, MoveUpAction);
            InputEventHandler.Instance.AddEventListener(KeyAction.Right, MoveRightAction);
            InputEventHandler.Instance.AddEventListener(KeyAction.Down, MoveDownAction);
            InputEventHandler.Instance.AddEventListener(KeyAction.Left, MoveLeftAction);
        }

        /// <summary>
        /// Checks if it is possible to move in the given direction
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        private bool CanMoveToCell(Direction direction) //Verander direction naar point
        {
            Point directionPoint = direction switch
            {
                Direction.North => new Point(0, -1),
                Direction.East => new Point(1, 0),
                Direction.South => new Point(0, 1),
                Direction.West => new Point(-1, 0),
                _ => throw new ArgumentException("The given direction does not exist.")
            };

            Vector2 tilePosition = new Vector2((int)Transform.Position.X / UnitSize, (int)Transform.Position.Y / UnitSize) + new Vector2(directionPoint.X, directionPoint.Y);

            if (Tilegrid.GetElement((int)tilePosition.X, (int)tilePosition.Y).TileCollision == TileCollision.Passable)
                return true;

            return false;
        }

        private void MoveToCell(Direction movementDirection)
        {
            if (IsMoving || !CanMove || !CanMoveToCell(movementDirection))
                return;

            StartPosition = Parent.Transform.Position;
            EndPosition = StartPosition + movementDirection switch
            {
                Direction.North => new Vector2(0, -UnitSize), //gebruik grid CellToWorld hiervoor, makkelijker
                Direction.East => new Vector2(UnitSize, 0),
                Direction.South => new Vector2(0, UnitSize),
                Direction.West => new Vector2(-UnitSize, 0),
                _ => throw new Exception("The requested direction does not exist"),
            };

            CanMove = false;
            IsLerping = true;
        }

        private void LerpToDestination(GameTime gameTime) //parent position moet niet per se gebruikt worden => base transform heeft reference naar parent
        {
            if (TimeElapsed < LerpDuration)
            {
                Parent.Transform.Position = Vector2.Lerp(StartPosition, EndPosition, TimeElapsed / LerpDuration);
                TimeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                Stop();
            }
        }

        public void Stop()
        {
            Parent.Transform.Position = EndPosition;

            CanMove = true;
            IsLerping = false;

            TimeElapsed = 0;
        }

        public override void Update(GameTime gameTime)
        {
            if (IsLerping)
                LerpToDestination(gameTime);

            LastPosition = CurrentPosition;
            CurrentPosition = Parent.Transform.Position;
            Speed = Vector2.Divide((CurrentPosition - LastPosition), (float)gameTime.ElapsedGameTime.TotalSeconds);
        }
    }
}
