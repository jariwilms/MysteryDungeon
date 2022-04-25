using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using MysteryDungeon.Core.Extensions;

namespace MysteryDungeon.Core.Animations.Particles
{
    public enum ParticleType
    {
        White, 
        Red, 
        Green, 
        Blue,
    }

    public class Particle
    {
        public Vector2 Position { get; set; }                   //Local position
        public Vector2 Velocity { get; set; }                   //Change in position per second
        public Vector2 Acceleration { get; set; }               //Change in velocity per second

        public float LocalAngle { get; set; }                   //Local angle of rotation
        public float LocalAngularVelocity { get; set; }         //Local angular velocity
        public Vector2 LocalRotationOrigin { get; set; }        //Local rotation point

        public float GlobalAngle { get; set; }                  //Global angle of rotation
        public float GlobalAngularVelocity { get; set; }        //Global angular velocity
        public Vector2 GlobalRotationOrigin { get; set; }       //Global rotation point

        public float Scale { get; set; }                        //Particle size
        public Color Color { get; set; }                        //Particle color

        public float LifeSpan { get; set; }                       //Lifespan in milliseconds

        public Particle(Vector2 position, Vector2 velocity, float lifeSpan)
        {
            Position = position;
            Velocity = velocity;
            Acceleration = Vector2.Zero;

            Scale = 0.01f;
            Color = Color.White;

            LifeSpan = lifeSpan;
        }
        public Particle(Vector2 position, Vector2 velocity, Vector2 acceleration, float lifeSpan) : this(position, velocity, lifeSpan)
        {
            Acceleration = acceleration;
        }
        public Particle(Vector2 position, Vector2 velocity, Vector2 acceleration, int scale, Color color, float lifeSpan) : this(position, velocity, acceleration, lifeSpan)
        {
            Scale = scale;
            Color = color;
        }

        public void Update(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            LifeSpan -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            Position += Velocity * deltaTime;
            Velocity += Acceleration * deltaTime;
            LocalAngle += LocalAngularVelocity * deltaTime;
        }
    }
}
