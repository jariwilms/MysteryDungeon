using Microsoft.Xna.Framework;
using MysteryDungeon.Core.Entities;

namespace MysteryDungeon.Core.Components
{
    public enum EnemyState
    {
        Idle,
        Sleeping,

        Wandering,
        Chasing,
        Attacking,
    }

    public class IntelligenceComponent : Component
    {
        public LivingEntity Target { get; set; }

        public Point Destination;
        public Point WalkPoint;

        public float SightRange;
        public float AttackRange;
        public bool PlayerInSightRange;

        public EnemyState State { get; protected set; }

        public IntelligenceComponent(GameObject parent) : base(parent) { }
    }
}
