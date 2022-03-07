using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MysteryDungeon.Core;
using System;

namespace MysteryDungeon
{
    public class MysteryDungeon : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public static SpriteFont _spriteFont;

        private const int _virtualWindowWidth = 800;
        private const int _virtualWindowHeight = 600;
        public static int _windowWidth; //Fix access modifier => tijdelijk gebruikt voor Camera class
        public static int _windowHeight;
        private readonly float _windowWidthScale;
        private readonly float _windowHeightScale;
        private Matrix _windowScale;

        private double _deltaTime;

        public static KeyboardState KeyboardState;
        public static KeyboardState LastkeyboardState;

        private double[] _frameTimes;
        private double _averageFrameTime;
        private int _frameIndex;

        // ### THE CUM ZONE ###

        Camera camera;
        Level level;

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

            _graphics.SynchronizeWithVerticalRetrace = false;
            this.IsFixedTimeStep = false;

            _graphics.ApplyChanges();

            // #####

            level = new Level(Content);

            camera = new Camera();
            camera.Follow(level.Player);

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

            // #####

            if (Utility.KeyPressedOnce(Keys.Q))
                camera.ZoomOut();

            if (Utility.KeyPressedOnce(Keys.E))
                camera.ZoomIn();

            level.Update(gameTime);
            camera.Update();

            // #####

            LastkeyboardState = KeyboardState;
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

            level.Draw(_spriteBatch, gameTime);
            //_spriteBatch.DrawString(_spriteFont, Math.Round(_averageFrameTime).ToString(), new Vector2(100, 100), Color.White); //drawfps

            // #####

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
