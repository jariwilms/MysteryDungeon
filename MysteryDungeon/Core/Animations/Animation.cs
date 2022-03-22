using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MysteryDungeon.Core.Animations
{
    public class Animation
    {
        public string Identifier { get; set; }

        public Texture2D SourceTexture { get; set; }
        public Rectangle SourceRectangle;

        public SpriteEffects SpriteEffects;
        private Point _textureOffset;

        private int _spaceBetweenSpritesX;
        private int _spaceBetweenSpritesY;

        private int _rows;
        private int _columns;

        private int _currentFrame; //Huidige frame: 0 <= _currentFrame < _totalFrames
        private int _totalFrames; //Hoeveelheid frames -> Rows * Columns

        private double _frameTime; //Tijd tussen frames
        private double _deltaTime;

        public bool IsLooping;

        public Animation(
            string identifier, 
            Texture2D texture, 
            int rows, int columns, 
            float frameTime = 1.0f, bool isLooping = true)
        {
            Identifier = identifier;

            SourceTexture = texture;

            SourceRectangle.Width = SourceTexture.Width / columns;
            SourceRectangle.Height = SourceTexture.Height / rows;

            _textureOffset = new Point(0, 0);
            _spaceBetweenSpritesX = 0;
            _spaceBetweenSpritesY = 0;

            _rows = rows;
            _columns = columns;

            _currentFrame = 0;
            _totalFrames = rows * columns;

            _frameTime = frameTime;

            IsLooping = isLooping;
        }

        public Animation(
            string identifier, 
            Texture2D texture, Point textureOffset,
            int spriteWidth, int spriteHeight,
            int spaceBetweenSpritesX, int spaceBetweenSpritesY,
            int rows, int columns,
            float frameTime = 1.0f,
            bool isLooping = true,
            SpriteEffects spriteEffects = SpriteEffects.None)
            : this(identifier, texture, rows, columns, frameTime)
        {
            _textureOffset = textureOffset;
            SpriteEffects = spriteEffects;

            SourceRectangle.Width = spriteWidth;
            SourceRectangle.Height = spriteHeight;

            _spaceBetweenSpritesX = spaceBetweenSpritesX;
            _spaceBetweenSpritesY = spaceBetweenSpritesY;

            IsLooping = isLooping;
        }

        public void NextFrame()
        {
            _currentFrame = (_currentFrame + 1) % _totalFrames;
            UpdateSprite();
        }

        public void PreviousFrame()
        {
            _currentFrame = (_currentFrame + _totalFrames - 1) % _totalFrames;
            UpdateSprite();
        }

        public void Update(GameTime gameTime)
        {
            if (_frameTime <= 0)
                return;

            if (_currentFrame == _totalFrames - 1 && !IsLooping)
                return;

            _deltaTime += gameTime.ElapsedGameTime.TotalSeconds;

            while (_deltaTime >= _frameTime)
            {
                _deltaTime -= _frameTime;
                _currentFrame = (_currentFrame + 1) % _totalFrames;
            }

            UpdateSprite();
        }

        private void UpdateSprite() //Updates without taking time into account
        {
            int column = _currentFrame % _columns;
            int row = _currentFrame / _columns;

            SourceRectangle.X = _textureOffset.X + column * SourceRectangle.Width + column * _spaceBetweenSpritesX;
            SourceRectangle.Y = _textureOffset.Y + row * SourceRectangle.Height + row * _spaceBetweenSpritesY;
        }
    }
}
