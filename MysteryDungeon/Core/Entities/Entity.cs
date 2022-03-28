using System;

namespace MysteryDungeon.Core.Entities
{
    public abstract class Entity : GameObject
    {
        public event Action OnSpawn;
        public event Action OnDefeat;

        public event Action OnMoveStart;
        public event Action OnMoveFinished;

        public event Action OnTurnStart;
        public event Action OnTurnFinished;

        public Entity() : base() { }
    }
}
