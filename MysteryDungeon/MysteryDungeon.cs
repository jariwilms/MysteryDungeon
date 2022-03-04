using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MysteryDungeon.Core;

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

            Rectangle a = new Rectangle(0, 0, 10, 10);
            Rectangle b = new Rectangle(1, 1, 2, 2);
            bool c = a.Intersects(b);
            Console.WriteLine("");
        }

        protected override void Initialize()
        {
            _graphics.GraphicsProfile = GraphicsProfile.HiDef;
            _graphics.PreferredBackBufferWidth = _windowWidth;
            _graphics.PreferredBackBufferHeight = _windowHeight;
            _graphics.PreferMultiSampling = true;
            _graphics.GraphicsDevice.PresentationParameters.MultiSampleCount = 4;

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
            KeyboardState = Keyboard.GetState();

            // #####

            if (Utility.KeyPressedOnce(Keys.Q))
                camera.zoomOut();

            if (Utility.KeyPressedOnce(Keys.E))
                camera.zoomIn();

            level.Update(gameTime);
            camera.Update();

            // #####

            LastkeyboardState = KeyboardState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(transformMatrix: camera.TransformMatrix);

            // #####

            level.Draw(_spriteBatch, gameTime);

            // #####

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
