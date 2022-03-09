using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MysteryDungeon.Core.Animations;
using MysteryDungeon.Core.Input;
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

    public class Player : Sprite
    {
        private Level _level;

        private float _lerpDuration = 0.11f;
        private float _timeElapsed = 0.0f;

        private Vector2 _startPosition;
        private Vector2 _endPosition;

        private bool _canMove = true; //Voorlopig zijn deze vars altijd het tegenovergestelde van elkaar
        private bool _isMoving = false;

        private Action MoveUpAction; //Move naar component/sprite?
        private Action MoveRightAction;
        private Action MoveDownAction;
        private Action MoveLeftAction;

        public Player(ContentManager content, Texture2D texture, Level level) : base(texture)
        {
            _level = level;

            _spriteSheet = content.Load<Texture2D>("sprites/chikorita"); //Move naar andere locatie => animationplayer maybe, also maak ctor param
            _animationPlayer = new AnimationPlayer();

            CreateAnimations();
            CreateActions();

            _animationPlayer.PlayAnimation("Idle");
        }

        private void CreateAnimations() //Move dit naar data class met animation data => verschillende player/enemy models
        {
            _animationPlayer.AddAnimation("Idle", new Animation(_spriteSheet, new Point(98, 20), 1, 0, 16, 21, 1, 2, 0.8f));   //Move spritesheet naar animationplayer? texture verandert niet wrs
            _animationPlayer.AddAnimation("MoveUp", new Animation(_spriteSheet, new Point(260, 47), 3, 0, 13, 20, 1, 2, 0.4f));
            _animationPlayer.AddAnimation("MoveRight", new Animation(_spriteSheet, new Point(175, 49), 3, 0, 20, 18, 1, 2, 0.4f, true, SpriteEffects.FlipHorizontally));
            _animationPlayer.AddAnimation("MoveDown", new Animation(_spriteSheet, new Point(102, 46), 3, 0, 13, 20, 1, 2, 0.4f));
            _animationPlayer.AddAnimation("MoveLeft", new Animation(_spriteSheet, new Point(175, 49), 3, 0, 20, 18, 1, 2, 0.4f)); //TODO: mogelijkheid om animation frames te stitchen, extra animation class?
        }

        //Move to sprite class + override, also put in list of actions => foreach iteration
        private void CreateActions()
        {
            MoveUpAction = () => { MoveTo(MovementDirection.North); };
            MoveRightAction = () => { MoveTo(MovementDirection.East); };
            MoveDownAction = () => { MoveTo(MovementDirection.South); };
            MoveLeftAction = () => { MoveTo(MovementDirection.West); };

            InputEventInvoker.RegisterAction(ActionKeys.UpKey, MoveUpAction);
            InputEventInvoker.RegisterAction(ActionKeys.LeftKey, MoveLeftAction);
            InputEventInvoker.RegisterAction(ActionKeys.DownKey, MoveDownAction);
            InputEventInvoker.RegisterAction(ActionKeys.RightKey, MoveRightAction);
        }

        private void MoveTo(MovementDirection movementDirection)
        {
            if (_isMoving || !_canMove || !CanMoveInDirection(movementDirection))
                return;

            _isMoving = true;
            _canMove = false;

            _startPosition = Transform.Position;
            _endPosition = _startPosition + movementDirection switch
            {
                MovementDirection.North => new Vector2(0, -24), //magic numbers jaja idc atm, 24 is _unitSize btww
                MovementDirection.East => new Vector2(24, 0),
                MovementDirection.South => new Vector2(0, 24),
                MovementDirection.West => new Vector2(-24, 0),
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

            _animationPlayer.PlayAnimation(animationIdentifier);
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

        /// <summary>
        /// Checks if it is possible to move in the given direction
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        private bool CanMoveInDirection(MovementDirection direction) //TODO: DIRECTION ENUM IDK
        {
            Point directionVector = direction switch
            {
                MovementDirection.North => new Point(0, -1),
                MovementDirection.East => new Point(1, 0),
                MovementDirection.South => new Point(0, 1),
                MovementDirection.West => new Point(-1, 0),
                _ => throw new ArgumentException("The given direction does not exist")
            };
            //Maak onderscheid tussen tilePosition en drawingPosition?
            Vector2 offsetPosition = new Vector2((int)Transform.Position.X / _unitSize, (int)Transform.Position.Y / _unitSize) + new Vector2(directionVector.X, directionVector.Y);

            if (_level.TileMap.Tiles[(int)offsetPosition.X, (int)offsetPosition.Y].TileCollision == TileCollision.Passable)
                return true;

            return false;
        }

        public override void Update(GameTime gameTime)
        {
            if (_isMoving) //Gaat niet meer werken wanneer non grid-based movement geimplementeerd wordt
                LerpToDestination(gameTime);

            _animationPlayer.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _animationPlayer.Draw(spriteBatch, new Rectangle((int)Transform.Position.X, (int)Transform.Position.Y, _unitSize, _unitSize));
        }
    }
}
