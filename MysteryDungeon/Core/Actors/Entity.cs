using System;

namespace MysteryDungeon.Core.Actors
{
    public abstract class Entity : GameObject
    {
        public event Action OnSpawn;
        public event Action OnDefeat;

        public Entity() : base() { }
    }
}
