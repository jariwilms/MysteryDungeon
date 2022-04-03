using System;

namespace MysteryDungeon.Core.Entities
{
    public abstract class Entity : GameObject
    {
        public event Action OnSpawn;
        public event Action OnDefeat;

        public Entity() : base() { }
    }
}
