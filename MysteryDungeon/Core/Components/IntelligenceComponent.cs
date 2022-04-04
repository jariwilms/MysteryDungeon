using Microsoft.Xna.Framework;
using MysteryDungeon.Core.AI;

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
        public Behaviour Behaviour { get; set; }
        //public EnemyBehaviour EnemyBehaviour { get; set; }

        public IntelligenceComponent(GameObject parent) : base(parent)
        {

        }

        public void Act()
        {
            Behaviour.Act();
        }
    }
}
