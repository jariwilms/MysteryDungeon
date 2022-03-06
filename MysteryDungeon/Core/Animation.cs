
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MysteryDungeon.Core
{
    class Animation
    {
        public Texture2D Texture { get; set; } //De texture image zelf
        private Vector2 _textureSource; //Offset in de image

        public int TextureWidth { get { return _textureWidth; } }
        public int TextureHeight { get { return _textureHeight; } }
        private readonly int _textureWidth; //Voorlopig readonly, tenzij er een use-case komt voor texture resizing?
        private readonly int _textureHeight;

        public int Rows { get; set; } //Hoeveel rijen en kolommen de texture bevat
        public int Columns { get; set; }

        private int _currentFrame; //Huidige frame: 0 <= _currentFrame < _totalFrames
        private int _totalFrames; //Hoeveelheid frames -> Rows * Columns

        private double _frameTime; //Tijd tussen frames
        private double _deltaTime;

        public Rectangle SourceRectangle { get { return _sourceRectangle; } }
        private Rectangle _sourceRectangle;

        public Animation(Texture2D texture, int rows, int columns, float frameTime = 1.0f)
        {
            Texture = texture;
            _textureSource = Vector2.Zero;

            Rows = rows;
            Columns = columns;

            _currentFrame = 0;
            _totalFrames = Rows * Columns;

            _textureWidth = Texture.Width / Columns;
            _textureHeight = Texture.Height / Rows;

            _frameTime = frameTime;
        }

        public Animation(Texture2D texture, Vector2 textureOffset, int textureWidth, int textureHeight, int rows, int columns, float frameTime = 1.0f)
            : this(texture, rows, columns, frameTime)
        {
            _textureSource = textureOffset;
            _textureWidth = textureWidth;
            _textureHeight = textureHeight;
        }

        public void NextFrame()
        {
            _currentFrame = (_currentFrame + 1) % _totalFrames;
            UpdateFrame();
        }

        public void PreviousFrame()
        {
            _currentFrame = (_currentFrame + _totalFrames - 1) % _totalFrames;
            UpdateFrame();
        }

        public void Update(GameTime gameTime)
        {
            if (_frameTime <= 0)
                return;

            _deltaTime += gameTime.ElapsedGameTime.TotalSeconds;

            while (_deltaTime >= _frameTime)
            {
                _deltaTime -= _frameTime;
                _currentFrame = (_currentFrame + 1) % _totalFrames;
            }

            int row = _currentFrame / Columns; //Y index in the texture atlas
            int column = _currentFrame % Columns; //X index in the texture atlas

            _sourceRectangle = new Rectangle(
                (int)_textureSource.X + _textureWidth * column, //Column index + declared texture offset in spritesheet
                (int)_textureSource.Y + _textureHeight * row,  //Row index
                _textureWidth, _textureHeight);
        }

        public void UpdateFrame()
        {
            int row = _currentFrame / Columns; //Y index in the texture atlas
            int column = _currentFrame % Columns; //X index in the texture atlas

            _sourceRectangle = new Rectangle(
                (int)_textureSource.X + _textureWidth * column, //Column index + declared texture offset in spritesheet
                (int)_textureSource.Y + _textureHeight * row,  //Row index
                _textureWidth, _textureHeight);
        }
    }
}
