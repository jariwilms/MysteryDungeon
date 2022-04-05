using System.Collections.Generic;

namespace MysteryDungeon.Core.Animations.Particles
{
    public sealed class ParticleManager
    {
        public static readonly ParticleManager Instance = new ParticleManager();

        public List<Particle> Particles { get; set; }

        static ParticleManager() { }
        private ParticleManager() { }
    }
}
