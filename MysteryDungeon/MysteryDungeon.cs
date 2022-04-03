using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MysteryDungeon.Core;
using MysteryDungeon.Core.Data;
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

        // ### THE CUM ZONE ###

        private Camera _camera;
        private Level _level;
        private FrameCounter _frameCounter;

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

            _graphics.ApplyChanges();

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _spriteBatchGUI = new SpriteBatch(GraphicsDevice);

            // #####

            //GameObject.Content = new ContentManager(new GameServiceContainer(), "Content");
            Component.Content = Content;

            PokemonSpriteData.Content = Content;
            PokemonSpriteData.CreateDictionary();

            GUI.Instance.Initialize(Content, _spriteBatchGUI);

            Widget.WindowSettings = WindowSettings;

            _level = new Level(Content);

            _camera = new Camera(WindowSettings.WindowWidth, WindowSettings.WindowHeight);
            _camera.Follow(_level.Player);

            _frameCounter = new FrameCounter();

            // #####

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            InputEventHandler.Instance.Update();

            _level.Update(gameTime);
            _camera.Update();

            GUI.Instance.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, transformMatrix: _camera.TransformMatrix);

            _level.Draw(_spriteBatch);

            _spriteBatch.End();



            GUI.Instance.Draw();



            base.Draw(gameTime);
        }
    }
}
