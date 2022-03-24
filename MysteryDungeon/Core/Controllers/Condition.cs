using System;

namespace MysteryDungeon.Core.Controllers
{
    internal class Condition<T>
    {
        private Predicate<T> _predicate;

        public Condition(Predicate<T> predicate)
        {
            SetPredicate(predicate);
        }

        public void SetPredicate(Predicate<T> predicate)
            => _predicate = predicate;

        public bool IsTrue(T value)
            => _predicate.Invoke(value);
    }
}
