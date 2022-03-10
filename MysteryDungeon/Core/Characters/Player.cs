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

    public class Player : Actor
    {
        private Level _level;

        private float _lerpDuration = 0.22f;
        private float _timeElapsed = 0.0f;

        private Vector2 _startPosition;
        private Vector2 _endPosition;

        private bool _canMove = true; //Voorlopig zijn deze vars altijd het tegenovergestelde van elkaar
        private bool _isMoving = false;

        private Action MoveUpAction; //Move naar component/sprite?
        private Action MoveRightAction;
        private Action MoveDownAction;
        private Action MoveLeftAction;

        private delegate void OnSpawn();
        private delegate void OnDefeat();
        private delegate void OnMoveStart();
        private delegate void OnMoveFinished();

        private OnSpawn _onSpawn;
        private OnDefeat _onDefeat;
        private OnMoveStart _onMoveStart;
        private OnMoveFinished _onMoveFinished;

        public Player(ContentManager content, Level level) : base()
        {
            _level = level;

            Sprite = new Sprite(content.Load<Texture2D>("sprites/chikorita"), UnitSize); //move naar constructor

            CreateAnimations();
            CreateActions();

            Sprite.AnimationPlayer.PlayAnimation("Idle");

            _onMoveStart += () => { _isMoving = true; };
            _onMoveFinished += () => { _isMoving = false; };
            _onMoveFinished += () => { };
        }

        private void CreateAnimations() //Move dit naar data class met animation data => verschillende player/enemy models
        {
            Sprite.AnimationPlayer.AddAnimation("Idle", new Animation(Sprite.Texture, new Point(98, 20), 1, 0, 16, 21, 1, 2, 0.8f));   //Move spritesheet naar animationplayer? texture verandert niet wrs
            Sprite.AnimationPlayer.AddAnimation("MoveUp", new Animation(Sprite.Texture, new Point(260, 47), 3, 0, 13, 20, 1, 2, 0.4f));
            Sprite.AnimationPlayer.AddAnimation("MoveRight", new Animation(Sprite.Texture, new Point(175, 49), 3, 0, 20, 18, 1, 2, 0.4f, true, SpriteEffects.FlipHorizontally));
            Sprite.AnimationPlayer.AddAnimation("MoveDown", new Animation(Sprite.Texture, new Point(102, 46), 3, 0, 13, 20, 1, 2, 0.4f));
            Sprite.AnimationPlayer.AddAnimation("MoveLeft", new Animation(Sprite.Texture, new Point(175, 49), 3, 0, 20, 18, 1, 2, 0.4f)); //TODO: mogelijkheid om animation frames te stitchen, extra animation class?
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

            _onMoveStart.Invoke();
            _canMove = false;

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
                _canMove = true;

                _onMoveFinished.Invoke();
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

            if (_level.TileMap.Tiles[(int)tilePosition.X, (int)tilePosition.Y].TileCollision == TileCollision.Passable)
                return true;

            return false;
        }

        public override void Update(GameTime gameTime)
        {
            if (_isMoving) //Gaat niet meer werken wanneer non grid-based movement geimplementeerd wordt
                LerpToDestination(gameTime);

            Sprite.Update(gameTime, Transform);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Sprite.Draw(spriteBatch, gameTime);
        }
    }
}
