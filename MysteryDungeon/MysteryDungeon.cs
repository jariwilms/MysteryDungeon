using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MysteryDungeon.Core;
using MysteryDungeon.Core.Extensions;
using MysteryDungeon.Core.Input;
using MysteryDungeon.Core.Map;
using MysteryDungeon.Core.UI;

namespace MysteryDungeon
{
    public class MysteryDungeon : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteBatch _spriteBatchGUI;

        public WindowSettings WindowSettings;

        public static new GameServiceContainer Services { get; private set; }

        // ### THE CUM ZONE ###

        private Camera _camera;
        private Level _level;
        private FrameCounter _frameCounter;

        // ###

        public MysteryDungeon()
        {
            _graphics = new GraphicsDeviceManager(this);
            WindowSettings = new WindowSettings(800, 600);

            Services = base.Services;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            _graphics.GraphicsProfile = GraphicsProfile.HiDef;
            _graphics.PreferredBackBufferWidth = WindowSettings.WindowWidth;
            _graphics.PreferredBackBufferHeight = WindowSettings.WindowHeight;
            _graphics.ApplyChanges();

            Widget.WindowSettings = WindowSettings;

            _level = new Level();
            _camera = new Camera(_level.Player, WindowSettings.WindowWidth, WindowSettings.WindowHeight);
            _frameCounter = new FrameCounter();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _spriteBatchGUI = new SpriteBatch(GraphicsDevice);
            GUI.Instance.Initialize(_spriteBatchGUI);

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            InputEventHandler.Instance.Update();

            _level.Update(gameTime);
            _camera.Update();

            _frameCounter.Update(gameTime);
            GUI.Instance.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, transformMatrix: _camera.TransformMatrix);
            _level.Draw(_spriteBatch);
            _spriteBatch.End();

            GUI.Instance.QueueStringDraw(_frameCounter.AverageFramesPerSecond.ToString(), new Vector2(20, 110));
            GUI.Instance.Draw();

            base.Draw(gameTime);
        }
    }
}
