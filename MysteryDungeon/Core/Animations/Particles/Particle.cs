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
        public Texture2D Texture { get; set; }                  //Particle texture

        public Vector2 Position { get; set; }                   //Local position
        public Vector2 Velocity { get; set; }                   //Change in position per second

        public float LocalAngle { get; set; }                   //Local angle of rotation
        public float LocalAngularVelocity { get; set; }         //Local angular velocity
        public Vector2 LocalRotationOrigin { get; set; }        //Local rotation point

        public float GlobalAngle { get; set; }                  //Global angle of rotation
        public float GlobalAngularVelocity { get; set; }        //Global angular velocity
        public Vector2 GlobalRotationOrigin { get; set; }       //Global rotation point

        public Color Color { get; set; }                        //Particle color
        public float Size { get; set; }                         //Particle size

        public int TimeToLive { get; set; }                     //Lifetime in milliseconds

        public Particle(Texture2D texture, Vector2 position, Vector2 velocity, int timeToLive)
        {
            Texture = texture;

            Position = position;
            Velocity = velocity;

            LocalAngle = 0.0f;
            LocalAngularVelocity = 0.0f;

            Color = Color.White;
            Size = 0.01f;

            TimeToLive = timeToLive;
        }
        public Particle(Texture2D texture, Vector2 position, Vector2 velocity, float angle, float angularVelocity, Vector2 rotationOrigin, int timeToLive) : this(texture, position, velocity, timeToLive)
        {
            LocalAngle = angle;
            LocalAngularVelocity = angularVelocity;
            GlobalRotationOrigin = rotationOrigin;
        }
        public Particle(Texture2D texture, Vector2 position, Vector2 velocity, float angle, float angularVelocity, Vector2 rotationOrigin, Color color, float size, int timeToLive) : this(texture, position, velocity, timeToLive)
        {
            LocalAngle = angle;
            LocalAngularVelocity = angularVelocity;
            GlobalRotationOrigin = rotationOrigin;

            Color = color;
            Size = size;
        }

        public void Update(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            TimeToLive -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            Position += Velocity * deltaTime;
            LocalAngle += LocalAngularVelocity * deltaTime;
        }
    }
}
