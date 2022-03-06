using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Configuration;

namespace MysteryDungeon.Core
{
    class Player : Sprite
    {
        private Level _level;

        private float _lerpDuration = 0.08f;
        private float _timeElapsed = 0;
        private Vector2 _startPosition;
        private Vector2 _endPosition;

        private bool _canMove = true;
        private bool _isMoving = false;

        public Player(Texture2D texture, Level level) : base(texture)
        {
            _level = level;
            _unitSize = Int32.Parse(ConfigurationManager.AppSettings.Get("UnitSize"));
        }

        public void Move(GameTime gameTime)
        {
            if (_canMove) //TODO: fix deze repeating shit code
            {
                //Welke retard heeft dit geschreven?
                if (Utility.KeyPressedOnce(Keys.W))
                {
                    if (CanMoveTo("up"))
                    {
                        _startPosition = Transform.Position;
                        _endPosition = Transform.Position + new Vector2(0, -_unitSize);
                        _isMoving = true;
                        _canMove = false;
                    }
                }

                if (Utility.KeyPressedOnce(Keys.A))
                {
                    if (CanMoveTo("left"))
                    {
                        _startPosition = Transform.Position;
                        _endPosition = Transform.Position + new Vector2(-_unitSize, 0);
                        _isMoving = true;
                        _canMove = false;
                    }
                }

                if (Utility.KeyPressedOnce(Keys.S))
                {
                    if (CanMoveTo("down"))
                    {
                        _startPosition = Transform.Position;
                        _endPosition = Transform.Position + new Vector2(0, _unitSize);
                        _isMoving = true;
                        _canMove = false;
                    }
                }

                if (Utility.KeyPressedOnce(Keys.D))
                {
                    if (CanMoveTo("right"))
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

        private bool CanMoveTo(string direction) //TODO: DIRECTION ENUM IDK
        {
            int leftTile = (int)Math.Floor((float)BoundingRectangle.Left / _unitSize);
            int rightTile = (int)Math.Ceiling((float)BoundingRectangle.Right / _unitSize) - 1;
            int topTile = (int)Math.Floor((float)BoundingRectangle.Top / _unitSize);
            int bottomTile = (int)Math.Ceiling((float)BoundingRectangle.Bottom / _unitSize) - 1;

            int offsetx = 0;
            int offsety = 0;

            switch (direction)
            {
                case "up":
                    offsety = -1;
                    break;
                case "left":
                    offsetx = -1;
                    break;
                case "down":
                    offsety = 1;
                    break;
                case "right":
                    offsetx = 1;
                    break;
                default:
                    break;
            }

            offsetx = leftTile + offsetx < 0 ? 0 : offsetx;
            offsety = topTile + offsety < 0 ? 0 : offsety;

            if (_level.TileMap.Tiles[leftTile + offsetx, topTile + offsety].TileCollision == TileCollision.Passable)
                return true;

            return false;
        }

        public override void Update(GameTime gameTime)
        {
            Move(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)Transform.Position.X, (int)Transform.Position.Y, _unitSize, _unitSize), Color.White);

            //spriteBatch.DrawString(GameTest._spriteFont, topTile.ToString(), new Vector2(300, 10), Color.White);
            //spriteBatch.DrawString(GameTest._spriteFont, rightTile.ToString(), new Vector2(300, 40), Color.White);
            //spriteBatch.DrawString(GameTest._spriteFont, bottomTile.ToString(), new Vector2(300, 70), Color.White);
            //spriteBatch.DrawString(GameTest._spriteFont, leftTile.ToString(), new Vector2(300, 100), Color.White);
        }
    }
}
