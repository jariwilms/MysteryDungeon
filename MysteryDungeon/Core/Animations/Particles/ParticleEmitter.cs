using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MysteryDungeon.Core.Extensions;
using MysteryDungeon.Core.UI;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel;

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
        public Texture2D Texture { get; private set; }                      //Texture of new particles
        public List<Particle> Particles { get; private set; }               //List of particles

        #region main
        public int MinLifetime { get; set; }                                //Minimum particle lifetime
        public int MaxLifetime { get; set; }                                //Maximum particle lifetime

        public float EmissionDuration { get; set; }                         //Duration of new particle emission in seconds

        public bool IsEmitting { get; set; }                                //Emit new particles?
        public bool IsLooping { get; set; }                                 //Loop emission duration?
        #endregion

        #region emission
        public EmissionShape EmissionShape                                  //Resulting shape of emitted particles
        {
            get => _emissionShape;
            set 
            { 
                Particles.Clear(); 
                _emissionShape = value; 
            } 
        }                           
        private EmissionShape _emissionShape;

        public int EmissionRate { get; set; }                               //Rate of particle emission
        public float DeltaTimePerEmission => 1.0f / EmissionRate;           //Seconds per particle emission
        private int _particlesToGenerate;
        #endregion

        #region shape
        public double MinConeAngle { get; set; }                            //Minimum angle of cone emission
        public double MaxConeAngle { get; set; }                            //Maximum angle of cone emission
        #endregion

        #region velocity
        public Vector2 Velocity { get; set; }                               //Velocity of the emitter
        public Vector2 Acceleration { get; set; }                           //Acceleration of the emitter

        public float SpeedMultiplier { get; set; }                          //Particle speed multiplier
        #endregion

        private float _deltaTime;
        private float _totalTime;

        private readonly Random _random;

        public ParticleEmitter(ParticleType particleType, Vector2 position, float emissionDuration, int emissionRate = 1)
        {
            Texture = ParticleEngine.Instance.LoadTexture(particleType.GetFilename());

            Particles = new List<Particle>();
            EmissionShape = EmissionShape.Sphere;

            EmissionDuration = emissionDuration;

            Transform.Position = position;
            Velocity = new Vector2(10.0f, 2.0f);
            Acceleration = Vector2.Zero;

            SpeedMultiplier = 100.0f;

            MinConeAngle = Math.PI / 4.0f;
            MaxConeAngle = MinConeAngle * 3;

            MinLifetime = 1000;
            MaxLifetime = 3000;

            EmissionRate = emissionRate;

            _deltaTime = 0.0f;
            _totalTime = 0.0f;

            IsEmitting = true;
            IsLooping = true;

            _random = new Random();
        }
        ~ParticleEmitter()
        {
            ParticleEngine.Instance.Emitters.Remove(this);
        }

        private void GenerateParticles(int amount)
        {
            Action generator = EmissionShape switch
            {
                EmissionShape.Sphere => SphereGenerator,
                EmissionShape.Hemisphere => HemisphereGenerator,
                EmissionShape.Cone => ConeGenerator, 
                EmissionShape.Box => throw new NotImplementedException(), 
                EmissionShape.Circle => throw new NotImplementedException(), 
                EmissionShape.Edge => throw new NotImplementedException(),
                _ => throw new InvalidEnumArgumentException()
            };

            for (int i = 0; i < amount; i++)
                generator?.Invoke(); //i know, shut up. Ben niet zeker of for loop in elke generator for some reason
        }
        private void SphereGenerator()
        {
            var angle = 2 * Math.PI * _random.Next(0, 10000) / 10000 + Transform.Rotation.X;
            var velocity = new Vector2((float)Math.Cos(angle), -(float)Math.Sin(angle)) * SpeedMultiplier;
            var particle = new Particle(Transform.Position, Velocity + velocity, Acceleration, _random.Next(MinLifetime, MaxLifetime));
            Particles.Add(particle);
        }
        private void HemisphereGenerator()
        {
            var angle = 2 * Math.PI * _random.Next(-5000, 0) / 10000 + Transform.Rotation.X;
            var velocity = new Vector2((float)Math.Cos(angle), -(float)Math.Sin(angle)) * SpeedMultiplier;
            var particle = new Particle(Transform.Position, velocity, _random.Next(MinLifetime, MaxLifetime));
            Particles.Add(particle);
        }
        private void ConeGenerator()
        {
            var angle = (MaxConeAngle - MinConeAngle) * _random.Next(0, 10000) / 10000 + MinConeAngle + Transform.Rotation.X;
            var velocity = new Vector2((float)Math.Cos(angle), -(float)Math.Sin(angle)) * SpeedMultiplier;
            var particle = new Particle(Transform.Position, velocity, _random.Next(MinLifetime, MaxLifetime));
            Particles.Add(particle);
        }

        public override void Update(GameTime gameTime)
        {
            _deltaTime += (float)gameTime.ElapsedGameTime.TotalSeconds; //16.67ms on locked
            _totalTime += _deltaTime;

            if (!IsLooping && _totalTime > EmissionDuration)
                IsEmitting = false;

            if (IsEmitting)
            {
                _particlesToGenerate = (int)(_deltaTime / DeltaTimePerEmission);
                if (_particlesToGenerate > 0) _deltaTime -= _particlesToGenerate * DeltaTimePerEmission;
                GenerateParticles(_particlesToGenerate);
            }

            for (int index = Particles.Count - 1; index > 0; index--)
            {
                Particles[index].Update(gameTime);

                if (!(Particles[index].LifeSpan > 0.0f))
                    Particles.RemoveAt(index);
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            //var visibleParticles = 0;
            //Particles.ForEach(particle =>
            //{
            //    if (!Window.IsOutOfBounds(particle.Position))
            //    {
            //        visibleParticles++;
            //        spriteBatch.Draw(Texture, particle.Position, null, particle.Color, particle.LocalAngle, particle.GlobalRotationOrigin, particle.Scale, SpriteEffects.None, 0.0f);
            //    }
            //});
            //GUI.Instance.QueueStringDraw("DRAWN PARTICLES " + visibleParticles, new Vector2(10, 10));

            //Particles.ForEach(particle =>
            //{
            //    if (!Window.IsOutOfBounds(particle.Position))
            //        spriteBatch.Draw(Texture, particle.Position, _sourceRectangle, particle.Color, particle.LocalAngle, particle.GlobalRotationOrigin, particle.Size, SpriteEffects.None, 0.0f);
            //});
        }
    }
}
