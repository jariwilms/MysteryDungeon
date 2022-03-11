using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MysteryDungeon.Core;
using MysteryDungeon.Core.Input;

namespace MysteryDungeon
{
    public class MysteryDungeon : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public SpriteFont _spriteFont;

        private const int _virtualWindowWidth = 720;
        private const int _virtualWindowHeight = 720;
        public int _windowWidth; //Fix access modifier => tijdelijk gebruikt voor Camera class
        public int _windowHeight;
        private readonly float _windowWidthScale;
        private readonly float _windowHeightScale;
        private Matrix _windowScale;

        private double _deltaTime;

        public static KeyboardState KeyboardState;
        public static KeyboardState LastkeyboardState;
        public MouseState MouseState;
        public MouseState LastMouseState;

        private double[] _frameTimes;
        private double _averageFrameTime;
        private int _frameIndex;

        // ### THE CUM ZONE ###

        Camera camera;
        Dungeon dungeon;

        public MysteryDungeon()
        {
            _graphics = new GraphicsDeviceManager(this);

            _windowWidth = 800;
            _windowHeight = 600;
            _windowWidthScale = _windowWidth / _virtualWindowWidth;
            _windowHeightScale = _windowHeight / _virtualWindowHeight;
            _windowScale = Matrix.CreateScale(_windowWidthScale, _windowHeightScale, 1.0f);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _frameTimes = new double[16];
            _averageFrameTime = 0;
            _frameIndex = 0;
        }

        protected override void Initialize()
        {
            _graphics.GraphicsProfile = GraphicsProfile.HiDef;
            _graphics.PreferredBackBufferWidth = _windowWidth;
            _graphics.PreferredBackBufferHeight = _windowHeight;
            _graphics.PreferMultiSampling = false;

            _graphics.ApplyChanges();

            // #####

            dungeon = new Dungeon(Content);

            camera = new Camera(_windowWidth, _windowHeight);
            camera.Follow(dungeon.Player);

            // #####

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _spriteFont = Content.Load<SpriteFont>("font");

            // #####
        }

        protected override void Update(GameTime gameTime)
        {
            _deltaTime = gameTime.ElapsedGameTime.TotalSeconds;

            _frameTimes[_frameIndex] = _deltaTime;
            _frameIndex = (_frameIndex + 1) % _frameTimes.Length;
            _averageFrameTime = 0;

            for (int i = 0; i < _frameTimes.Length; i++)
                _averageFrameTime += _frameTimes[i];

            _averageFrameTime /= 16;
            _averageFrameTime = 1 / _averageFrameTime;

            KeyboardState = Keyboard.GetState();
            MouseState = Mouse.GetState();

            // #####

            InputHandler.Update();

            if (MouseState.ScrollWheelValue > LastMouseState.ScrollWheelValue)
                camera.ZoomIn();

            if (MouseState.ScrollWheelValue < LastMouseState.ScrollWheelValue)
                camera.ZoomOut();

            dungeon.Update(gameTime);
            camera.Update();

            // #####

            LastkeyboardState = KeyboardState;
            LastMouseState = MouseState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                transformMatrix: camera.TransformMatrix);

            // #####

            dungeon.Draw(_spriteBatch, gameTime);

            //_spriteBatch.DrawString(_spriteFont, Math.Round(_averageFrameTime).ToString(), new Vector2(100, 100), Color.White); //drawfps

            // #####

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
