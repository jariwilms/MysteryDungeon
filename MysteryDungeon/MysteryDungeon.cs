using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MysteryDungeon.Core;
using MysteryDungeon.Core.Animations.Particles;
using MysteryDungeon.Core.Extensions;
using MysteryDungeon.Core.Input;
using MysteryDungeon.Core.Map;
using MysteryDungeon.Core.UI;
using System;

namespace MysteryDungeon
{
    public class MysteryDungeon : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteBatch _spriteBatchGUI;
        private SpriteBatch _spriteBatchParticle;

        public static new GameServiceContainer Services { get; private set; }

        private Camera _camera;
        private Level _level;
        private FrameCounter _frameCounter;

        public MysteryDungeon()
        {
            _graphics = new GraphicsDeviceManager(this);

            Services = base.Services;
            Content.RootDirectory = "Content";

            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = Core.UI.Window.WindowWidth;
            _graphics.PreferredBackBufferHeight = Core.UI.Window.WindowHeight;
            _graphics.SynchronizeWithVerticalRetrace = false;
            _graphics.PreferMultiSampling = false;
            _graphics.ApplyChanges();
            
            _level = new Level();
            _camera = new Camera(_level.Player, Core.UI.Window.WindowWidth, Core.UI.Window.WindowHeight);
            _frameCounter = new FrameCounter();

            base.Initialize();
        }

        ParticleEmitter emitter;
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _spriteBatchGUI = new SpriteBatch(GraphicsDevice);
            _spriteBatchParticle = new SpriteBatch(GraphicsDevice);

            ParticleEngine.Instance.Initialise(_spriteBatchParticle);
            GUI.Instance.Initialize(_spriteBatchGUI);

            emitter = new ParticleEmitter(ParticleType.White, new Vector2(400, 300), 10, 20000);
            emitter.IsLooping = false;

            ParticleEngine.Instance.Emitters.Add(emitter);

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            InputEventHandler.Instance.Update();

            _level.Update(gameTime);
            _camera.Update();

            //if (InputEventHandler.Instance.IsKeyPressedOnce(Keys.D))
            //    ParticleEngine.Instance.Pause();

            //if (InputEventHandler.Instance.IsKeyPressedOnce(Keys.A))
            //    ParticleEngine.Instance.Resume();

            //if (InputEventHandler.Instance.IsKeyPressedOnce(Keys.S))
            //    ParticleEngine.Instance.Stop();

            //emitter.IsEmitting = !emitter.IsEmitting;

            //ParticleEngine.Instance.Update(gameTime);
            GUI.Instance.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, transformMatrix: _camera.TransformMatrix);
            _level.Draw(_spriteBatch);
            _spriteBatch.End();

            _frameCounter.Update(gameTime);

            //ParticleEngine.Instance.Draw();
            //GUI.Instance.QueueDebugStringDraw("FPS " + Math.Round(_frameCounter.AverageFramesPerSecond).ToString());
            //GUI.Instance.QueueDebugStringDraw("MS  " + Math.Round(_frameCounter.ElapsedMilliseconds, 2).ToString());
            //GUI.Instance.QueueDebugStringDraw("MOUSE [" + Mouse.GetState().Position.X + ", " + Mouse.GetState().Position.Y + "]");
            //GUI.Instance.Draw();

            base.Draw(gameTime);
        }
    }
}
