using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MysteryDungeon.Core;
using MysteryDungeon.Core.Data;
using MysteryDungeon.Core.Interface;
using MysteryDungeon.Core.Input;
using System;
using MysteryDungeon.Core.Extensions;
using MysteryDungeon.Core.Map;

namespace MysteryDungeon
{
    public class MysteryDungeon : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _spriteFont;

        public WindowSettings WindowSettings;

        private double _deltaTime;

        public static KeyboardState KeyboardState;
        public static KeyboardState LastkeyboardState;
        public MouseState MouseState;
        public MouseState LastMouseState;

        private double[] _frameTimes;
        private double _averageFrameTime;
        private int _frameIndex;

        // ### THE CUM ZONE ###

        private Camera _camera;
        private Level _level;

        private Texture2D _blackRectangle;
        FrameCounter frameCounter;
        
        // ###

        public MysteryDungeon()
        {
            _graphics = new GraphicsDeviceManager(this);
            WindowSettings = new WindowSettings(800, 600);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.GraphicsProfile = GraphicsProfile.HiDef;
            _graphics.PreferredBackBufferWidth = WindowSettings.WindowWidth;
            _graphics.PreferredBackBufferHeight = WindowSettings.WindowHeight;
            _graphics.PreferMultiSampling = false;

            _graphics.ApplyChanges();

            // #####

            GameObject.Content = Content;   //jaja I know dat dit een regel of 28 breekt ma so what
            GuiTextures.Load(Content);      //das hun eigen fout om geen global content load shit te maken aleja
                                            //Like actually wie in zijn right mind gaat de ContentManager instance
                                            //passen door heel de f'in class hierarchy, doe ff normaal

            GUI.Instance.Initialize(Content);
            Widget.WindowSettings = WindowSettings; //idem

            _level = new Level(Content);

            _camera = new Camera(WindowSettings.WindowWidth, WindowSettings.WindowHeight);
            _camera.Follow(_level.Player);

            frameCounter = new FrameCounter();

            // #####

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _spriteFont = Content.Load<SpriteFont>("font");
            _blackRectangle = Content.Load<Texture2D>("effects/black_rectangle");
            // #####
        }

        protected override void Update(GameTime gameTime)
        {
            _deltaTime = gameTime.ElapsedGameTime.TotalSeconds;

            KeyboardState = Keyboard.GetState();
            MouseState = Mouse.GetState();

            // #####

            InputEventHandler.Instance.Update();

            if (MouseState.ScrollWheelValue > LastMouseState.ScrollWheelValue)
                _camera.ZoomIn();

            if (MouseState.ScrollWheelValue < LastMouseState.ScrollWheelValue)
                _camera.ZoomOut();

            _level.Update(gameTime);
            _camera.Update();

            GUI.Instance.Update(gameTime);
            frameCounter.Update(gameTime);
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
                transformMatrix: _camera.TransformMatrix);

            _level.Draw(_spriteBatch);

            _spriteBatch.End();



            _spriteBatch.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointClamp);

            if (true) //draw gui debug shit hier
            {
                _spriteBatch.DrawString(_spriteFont, "Camera zoom: " + _camera.ZoomValue.ToString(), new Vector2(10, 10), Color.White, 0.0f, Vector2.Zero, 0.1f, SpriteEffects.None, 0.0f);
            }

            GUI.Instance.DrawWidgets(_spriteBatch);
            //_spriteBatch.DrawString(_spriteFont, frameCounter.AverageFramesPerSecond.ToString(), new Vector2(10, 10), Color.White); //drawfps

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
