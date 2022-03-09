using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MysteryDungeon.Core
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

    class Player : Sprite
    {
        private Level _level;

        private float _lerpDuration = 0.22f;
        private float _timeElapsed = 0.0f;

        private Vector2 _startPosition;
        private Vector2 _endPosition;

        private bool _canMove = true;
        private bool _isMoving = false;

        private Action MoveUpAction;
        private Action MoveRightAction;
        private Action MoveDownAction;
        private Action MoveLeftAction;

        public Player(ContentManager content, Texture2D texture, Level level) : base(texture)
        {
            _level = level;

            _spriteSheet = content.Load<Texture2D>("sprites/chikorita"); //Move naar andere locatie => animationplayer maybe, also maak ctor param
            _animationPlayer = new AnimationPlayer();

            CreateAnimations();
            _animationPlayer.PlayAnimation("Idle");

            MoveUpAction = () => { MoveTo(MovementDirection.North); _animationPlayer.PlayAnimation("MoveUp"); };
            MoveRightAction = () => { MoveTo(MovementDirection.East); _animationPlayer.PlayAnimation("MoveRight"); };
            MoveDownAction = () => { MoveTo(MovementDirection.South); _animationPlayer.PlayAnimation("MoveDown"); };
            MoveLeftAction = () => { MoveTo(MovementDirection.West); _animationPlayer.PlayAnimation("MoveLeft"); };

            InputEventInvoker.RegisterAction(Keys.W, MoveUpAction);
            InputEventInvoker.RegisterAction(Keys.A, MoveLeftAction);
            InputEventInvoker.RegisterAction(Keys.S, MoveDownAction);
            InputEventInvoker.RegisterAction(Keys.D, MoveRightAction);
        }

        private void CreateAnimations() //Move dit naar data class met animation data
        {
            _animationPlayer.AddAnimation("Idle", new Animation(_spriteSheet, new Point(98, 20), 1, 0, 16, 21, 1, 2, 0.8f));   //Move spritesheet naar animationplayer? texture verandert niet wrs
            _animationPlayer.AddAnimation("MoveUp", new Animation(_spriteSheet, new Point(260, 47), 3, 0, 13, 20, 1, 2, 0.4f));
            _animationPlayer.AddAnimation("MoveRight", new Animation(_spriteSheet, new Point(175, 49), 3, 0, 20, 18, 1, 2, 0.4f, true, SpriteEffects.FlipHorizontally));
            _animationPlayer.AddAnimation("MoveDown", new Animation(_spriteSheet, new Point(102, 46), 3, 0, 13, 20, 1, 2, 0.4f));
            _animationPlayer.AddAnimation("MoveLeft", new Animation(_spriteSheet, new Point(175, 49), 3, 0, 20, 18, 1, 2, 0.4f));
        }

        public void ReadInput(GameTime gameTime) //Placeholder functie tijdelijk
        {
            //if (_canMove) //TODO: fix deze repeating shit code
            //{
            //    Point movementPoint = Point.Zero;
            //    KeyboardState keyboardState = Keyboard.GetState();

            //    if (keyboardState.IsKeyDown(Keys.W))
            //    {
            //        if (CanMoveTo(MovementDirection.North))
            //        {
            //            _startPosition = Transform.Position;
            //            _endPosition = Transform.Position + new Vector2(0, -_unitSize);
            //            _isMoving = true;
            //            _canMove = false;
            //            _animationPlayer.PlayAnimation("MoveUp");
            //        }
            //    }
            //    else if (keyboardState.IsKeyDown(Keys.A))
            //    {
            //        if (CanMoveTo(MovementDirection.West))
            //        {
            //            _startPosition = Transform.Position;
            //            _endPosition = Transform.Position + new Vector2(-_unitSize, 0);
            //            _isMoving = true;
            //            _canMove = false;
            //            _animationPlayer.PlayAnimation("MoveLeft");
            //        }
            //    }
            //    else if (keyboardState.IsKeyDown(Keys.S))
            //    {
            //        if (CanMoveTo(MovementDirection.South))
            //        {
            //            _startPosition = Transform.Position;
            //            _endPosition = Transform.Position + new Vector2(0, _unitSize);
            //            _isMoving = true;
            //            _canMove = false;
            //            _animationPlayer.PlayAnimation("MoveDown");
            //        }
            //    }
            //    else if (keyboardState.IsKeyDown(Keys.D))
            //    {
            //        if (CanMoveTo(MovementDirection.East))
            //        {
            //            _startPosition = Transform.Position;
            //            _endPosition = Transform.Position + new Vector2(_unitSize, 0);
            //            _isMoving = true;
            //            _canMove = false;
            //            _animationPlayer.PlayAnimation("MoveRight");
            //        }
            //    }
            //}
        }

        private void MoveTo(MovementDirection movementDirection)
        {
            if (_isMoving || !CanMoveTo(movementDirection))
                return;

            _isMoving = true;
            _canMove = false;

            _startPosition = Transform.Position;
            _endPosition = _startPosition + movementDirection switch
            {
                MovementDirection.North => new Vector2(0, -24), //magic numbers jaja idc atm
                MovementDirection.East => new Vector2(24, 0),
                MovementDirection.South => new Vector2(0, 24),
                MovementDirection.West => new Vector2(-24, 0),
                _ => throw new Exception("The requested direction does not exist"),
            };
        }

        public void LerpToDestination(GameTime gameTime)
        {
            if (_timeElapsed < _lerpDuration)
            {
                Transform.Position = Vector2.Lerp(_startPosition, _endPosition, _timeElapsed / _lerpDuration);
                _timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                Transform.Position = _endPosition;

                _timeElapsed = 0;
                _isMoving = false;
                _canMove = true;
            }
        }

        private bool CanMoveTo(MovementDirection direction) //TODO: DIRECTION ENUM IDK
        {
            if (!_canMove)
                return false;

            Point directionVector = direction switch
            {
                MovementDirection.North => new Point(0, -1),
                MovementDirection.East => new Point(1, 0),
                MovementDirection.South => new Point(0, 1),
                MovementDirection.West => new Point(-1, 0),
                _ => throw new ArgumentException("The given direction does not exist")
            };

            Vector2 offsetPosition = new Vector2((int)Transform.Position.X / _unitSize, (int)Transform.Position.Y / _unitSize) + new Vector2(directionVector.X, directionVector.Y);

            if (_level.TileMap.Tiles[(int)offsetPosition.X, (int)offsetPosition.Y].TileCollision == TileCollision.Passable)
                return true;

            return false;
        }

        public override void Update(GameTime gameTime)
        {
            //ReadInput(gameTime);

            if (_isMoving)
                LerpToDestination(gameTime);

            _animationPlayer.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _animationPlayer.Draw(spriteBatch, new Rectangle((int)Transform.Position.X, (int)Transform.Position.Y, _unitSize, _unitSize));
        }
    }
}
