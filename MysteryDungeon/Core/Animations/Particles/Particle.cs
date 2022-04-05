using Microsoft.Xna.Framework;

namespace MysteryDungeon.Core.Animations.Particles
{
    public class Particle
    {
        public Transform Transform { get; set; }
        public Vector2 Velocity { get; set; }

        public float LifeTime { get; set; }

        public Particle()
        {
            ParticleManager.Instance.Particles.Add(this);
            Transform = new Transform();
        }

        public void Update()
        {
            Transform.Position += Velocity;
        }
    }
}
