using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MysteryDungeon.Core.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MysteryDungeon.Core.Animations.Particles
{
    public sealed class ParticleEngine : IDisposable
    {
        public static readonly ParticleEngine Instance = new ParticleEngine();

        public List<Texture2D> Textures { get; set; }
        public List<ParticleEmitter> Emitters { get; set; }
        public int TotalParticles
        {
            get
            {
                int totalParticles = 0;
                Emitters.ForEach(emitter => totalParticles += emitter.Particles.Count);
                return totalParticles;
            }
        }

        private SpriteBatch _spriteBatch;
        private readonly ContentManager _content;

        static ParticleEngine() { }
        private ParticleEngine()
        {
            Textures = new List<Texture2D>();
            Emitters = new List<ParticleEmitter>();

            _content = new ContentManager(MysteryDungeon.Services, "Content/particles");
        }

        public void Initialise(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
        }

        public Texture2D LoadTexture(string filename)
        {
            return _content.Load<Texture2D>(filename);
        }

        public void Resume()
        {
            Emitters.ForEach(emitter =>
            {
                emitter.IsEmitting = true;
            });
        }
        public void Pause()
        {
            Emitters.ForEach(emitter =>
            {
                emitter.IsEmitting = false;
            });
        }
        public void Stop()
        {
            Emitters.ForEach(emitter =>
            {
                emitter.IsEmitting = false;
                emitter.Particles.Clear();
            });
        }

        public void Update(GameTime gameTime)
        {
            Emitters.ForEach(emitter => emitter.Update(gameTime));
        }
        public void Draw()
        {
            GUI.Instance.QueueDebugStringDraw("TOTAL PARTICLES: " + TotalParticles.ToString());

            _spriteBatch.Begin(SpriteSortMode.Texture, BlendState.Additive);
            Emitters.ForEach(emitter =>  emitter.Draw(_spriteBatch));
            _spriteBatch.End();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            _content.Unload();
        }
    }
}
