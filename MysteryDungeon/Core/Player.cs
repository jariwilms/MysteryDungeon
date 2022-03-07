using System;
using System.Collections.Generic;
using System.Configuration;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace MysteryDungeon.Core
{
    class Player : Sprite
    {
        private Level _level;

        private float _lerpDuration = 0.12f;
        private float _timeElapsed = 0;

        private Vector2 _startPosition;
        private Vector2 _endPosition;

        private bool _canMove = true;
        private bool _isMoving = false;

        public Player(ContentManager content, Texture2D texture, Level level) : base(texture)
        {
            _level = level;

            _animationPlayer = new AnimationPlayer();
            _animationDictionary = new Dictionary<string, Animation>();

            _tempSprite = content.Load<Texture2D>("sprites/chikorita");

            CreateAnimations();

            _animationPlayer.PlayAnimation(_animationDictionary.GetValueOrDefault("Idle"));
        }

        private void CreateAnimations()
        {
            _animationDictionary.Add("Idle", new Animation(_tempSprite, new Vector2(102, 46), 3, 0, 13, 21, 1, 2, 1.0f));
        }

        public void Move(GameTime gameTime)
        {
            if (_canMove) //TODO: fix deze repeating shit code
            {
                if (Utility.KeyPressedOnce(Keys.W))
                {
                    if (CanMoveTo(Direction.Up))
                    {
                        _startPosition = Transform.Position;
                        _endPosition = Transform.Position + new Vector2(0, -_unitSize);
                        _isMoving = true;
                        _canMove = false;
                    }
                }
                else if (Utility.KeyPressedOnce(Keys.A))
                {
                    if (CanMoveTo(Direction.Left))
                    {
                        _startPosition = Transform.Position;
                        _endPosition = Transform.Position + new Vector2(-_unitSize, 0);
                        _isMoving = true;
                        _canMove = false;
                    }
                }
                else if (Utility.KeyPressedOnce(Keys.S))
                {
                    if (CanMoveTo(Direction.Down))
                    {
                        _startPosition = Transform.Position;
                        _endPosition = Transform.Position + new Vector2(0, _unitSize);
                        _isMoving = true;
                        _canMove = false;
                    }
                }
                else if (Utility.KeyPressedOnce(Keys.D))
                {
                    if (CanMoveTo(Direction.Right))
                    {
                        _startPosition = Transform.Position;
                        _endPosition = Transform.Position + new Vector2(_unitSize, 0);
                        _isMoving = true;
                        _canMove = false;
                    }
                }
            }

            if (_isMoving)
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
        }

        private bool CanMoveTo(Direction direction) //TODO: DIRECTION ENUM IDK
        {
            Vector2 directionVector = direction switch
            {
                Direction.Up => new Vector2(0, -1),
                Direction.Right => new Vector2(1, 0),
                Direction.Down => new Vector2(0, 1),
                Direction.Left => new Vector2(-1, 0),
                _ => throw new ArgumentException("The given direction does not exist")
            };

            Vector2 offsetPosition = new Vector2((int)Transform.Position.X / _unitSize, (int)Transform.Position.Y / _unitSize) + directionVector;

            if (_level.TileMap.Tiles[(int)offsetPosition.X, (int)offsetPosition.Y].TileCollision == TileCollision.Passable)
                return true;

            return false;
        }

        public override void Update(GameTime gameTime)
        {
            Move(gameTime);
            _animationPlayer.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _animationPlayer.Draw(spriteBatch, new Rectangle((int)Transform.Position.X, (int)Transform.Position.Y, _unitSize, _unitSize));
        }
    }
}
