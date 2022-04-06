using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MysteryDungeon.Core.Extensions;

namespace MysteryDungeon.Core.Animations.Particles
{
    public enum EmissionShape
    {
        Sphere, 
        Hemisphere, 
        Cone, 
        Box, 
        Circle, 
        Edge
    }

    public class ParticleEmitter : GameObject
    {
        public Texture2D Texture { get; private set; }
        public List<Particle> Particles { get; private set; }
        public EmissionShape EmissionShape 
        {
            get => _emissionShape;
            set 
            { 
                Particles.Clear(); 
                _emissionShape = value; 
            } 
        }
        private EmissionShape _emissionShape;

        public Vector2 Position { get; set; }

        public int MinTimeToLive { get; set; }
        public int MaxTimeToLive { get; set; }

        public Vector2 MinVelocity { get; set; }
        public Vector2 MaxVelocity { get; set; }

        public int EmissionRate { get; set; }
        public double DeltaTimePerEmission => (double)1 / EmissionRate;
        private double _deltaTime;

        private Rectangle _sourceRectangle;

        private readonly Random _random;

        public ParticleEmitter(ParticleType particleType, Vector2 position, int emissionRate = 1)
        {
            Texture = ParticleEngine.Instance.LoadTexture(particleType.GetFilename());
            Particles = new List<Particle>();
            EmissionShape = EmissionShape.Sphere;

            Position = position;

            MinTimeToLive = 1000;
            MaxTimeToLive = 10000;

            MinVelocity = new Vector2(-10.0f);
            MaxVelocity = new Vector2(10.0f);

            EmissionRate = emissionRate;
            _deltaTime = 0.0f;

            _sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);

            _random = new Random();
        }

        private void GenerateParticles(int amount = 1)
        {
            for (int i = 0; i < amount; i++)
            {
                double angle = _random.Next(-18000, 18000) / 100 + 180;
                var velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 100;

                var particle = new Particle(Texture, Position, velocity, _random.Next(MinTimeToLive, MaxTimeToLive));
                Particles.Add(particle);
            }
        }

        public override void Update(GameTime gameTime)
        {
            _deltaTime += gameTime.ElapsedGameTime.TotalSeconds;

            if (_deltaTime > DeltaTimePerEmission)
            {
                _deltaTime = 0.0f;
                GenerateParticles(EmissionRate);
            }

            for (int i = 0; i < Particles.Count; i++)
            {
                var particle = Particles[i];
                particle.Update(gameTime);

                if (particle.TimeToLive <= 0)
                {
                    Particles.RemoveAt(i);
                    i--;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Particles.ForEach(particle => spriteBatch.Draw(Texture, particle.Position, _sourceRectangle, particle.Color, particle.LocalAngle, particle.GlobalRotationOrigin, particle.Size, SpriteEffects.None, 0.0f));
        }
    }
}
