using Microsoft.Xna.Framework;
using MysteryDungeon.Core.Extensions;
using MysteryDungeon.Core.Input;
using MysteryDungeon.Core.Tiles;
using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using MysteryDungeon.Core.UI;

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

        public float LerpDuration { get; set; }
        protected float DeltaTime { get; set; }

        protected Vector2 Origin;                                           //Starting position of movement
        protected Vector2 Destination;                                      //Destination position of movement

        public Vector2 Velocity { get; protected set; }                     //Velocity of the parent
        protected Vector2 CurrentPosition { get; set; }                     //Current position of parent
        protected Vector2 PreviousPosition { get; set; }                    //Previous position of parent

        public Point ViewDirection { get; protected set; }                  

        public bool AllowMovement { get; set; }
        public bool MovementLocked { get; set; }
        public bool MovementInProgress { get; set; }

        public Action QueuedAction { get; set; }                            //Every frame, if a MoveAction input is done => set QueuedAction to last input
        public Action MoveUpAction { get; set; }
        public Action MoveRightAction { get; set; }
        public Action MoveDownAction { get; set; }
        public Action MoveLeftAction { get; set; }

        public event Action OnMoveStart;
        public event Action OnMoveFinished;

        public GridMovementComponent(GameObject parent) : base(parent)
        {
            LerpDuration = 0.2f;
            DeltaTime = 0.0f;

            ViewDirection = new Point();

            CurrentPosition = new Vector2();
            PreviousPosition = new Vector2();
            Velocity = new Vector2();

            AllowMovement = true;
            MovementLocked = false;
            MovementInProgress = false;

            MoveUpAction    += () => { QueuedAction = () => { SetDestinationCell(Direction.North); }; };
            MoveRightAction += () => { QueuedAction = () => { SetDestinationCell(Direction.East); }; };
            MoveDownAction  += () => { QueuedAction = () => { SetDestinationCell(Direction.South); }; };
            MoveLeftAction  += () => { QueuedAction = () => { SetDestinationCell(Direction.West); }; };
        }

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

            ViewDirection = directionPoint;
            Vector2 tilePosition = new Vector2((int)Transform.Position.X / UnitSize, (int)Transform.Position.Y / UnitSize) + new Vector2(directionPoint.X, directionPoint.Y);

            if (Tilegrid.GetElement((int)tilePosition.X, (int)tilePosition.Y)?.TileCollision == TileCollision.Passable)
                return true;

            return false;
        }

        private void SetDestinationCell(Direction movementDirection)
        {
            if (!CanMoveToCell(movementDirection)) //check apart zodat viewDirection niet constant geset wordt
                return;

            Point offsetIndex = movementDirection switch
            {
                Direction.North => new Point(0, -1), 
                Direction.East => new Point(1, 0), 
                Direction.South => new Point(0, 1), 
                Direction.West => new Point(-1, 0),
                _ => throw new Exception("The requested direction does not exist"), 
            };

            Origin = Parent.Transform.Position;
            var startIndex = Tilegrid.GlobalPositionToCellIndex(Parent.Transform.Position);
            var endIndex = startIndex + offsetIndex;
            Destination = Tilegrid.CellIndexToGlobalPosition(endIndex.X, endIndex.Y);

            AllowMovement = false;
            MovementInProgress = true;

            OnMoveStart?.Invoke();
        }
        
        private void MoveToDestinationCell(GameTime gameTime)
        {
            if (DeltaTime < LerpDuration)
            {
                DeltaTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                Parent.Transform.Position = Vector2.Lerp(Origin, Destination, DeltaTime / LerpDuration);
            }
            else
            {
                Stop();
                OnMoveFinished?.Invoke();

                if (!MovementLocked) QueuedAction?.Invoke();
                if (MovementInProgress) MoveToDestinationCell(gameTime);
            }
        }

        public void CalculateSpeed(GameTime gameTime)
        {
            PreviousPosition = CurrentPosition;
            CurrentPosition = Parent.Transform.Position;
            Velocity = Vector2.Divide((CurrentPosition - PreviousPosition), (float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public void Stop()
        {
            Origin = Destination;
            Parent.Transform.Position = Destination;

            Velocity = Vector2.Zero;
            DeltaTime = 0.0f;
            
            AllowMovement = true;
            MovementInProgress = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (AllowMovement && !MovementInProgress && !MovementLocked) QueuedAction?.Invoke();
            if (MovementInProgress) MoveToDestinationCell(gameTime);

            CalculateSpeed(gameTime);
            QueuedAction = null;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
