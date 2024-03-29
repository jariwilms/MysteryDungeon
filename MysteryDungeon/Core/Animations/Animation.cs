﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MysteryDungeon.Core.Animations
{
    public class Animation
    {
        public Texture2D SourceTexture { get; set; }

        public Rectangle SourceRectangle { get { return _sourceRectangle; } set { _sourceRectangle = value; } }
        private Rectangle _sourceRectangle;

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

        public bool IsLooping { get; set; }

        public Animation(
            Texture2D texture,
            int rows, int columns,
            float frameTime = 1.0f, bool isLooping = true)
        {
            SourceTexture = texture;

            _sourceRectangle.Width = SourceTexture.Width / columns;
            _sourceRectangle.Height = SourceTexture.Height / rows;

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
            Texture2D texture, Point textureOffset,
            int spriteWidth, int spriteHeight,
            int spaceBetweenSpritesX, int spaceBetweenSpritesY,
            int rows, int columns,
            float frameTime = 1.0f,
            bool isLooping = true,
            SpriteEffects spriteEffects = SpriteEffects.None)
            : this(texture, rows, columns, frameTime)
        {
            _textureOffset = textureOffset;
            SpriteEffects = spriteEffects;

            _sourceRectangle.Width = spriteWidth;
            _sourceRectangle.Height = spriteHeight;

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

        public void Reset()
        {
            _currentFrame = 0;
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

        public void UpdateSprite() //Updates without taking time into account
        {
            int column = _currentFrame % _columns;
            int row = _currentFrame / _columns;

            _sourceRectangle.X = _textureOffset.X + column * _sourceRectangle.Width + column * _spaceBetweenSpritesX;
            _sourceRectangle.Y = _textureOffset.Y + row * _sourceRectangle.Height + row * _spaceBetweenSpritesY;
        }
    }
}
