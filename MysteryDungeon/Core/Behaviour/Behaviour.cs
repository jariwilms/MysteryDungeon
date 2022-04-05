using MysteryDungeon.Core.Actors;
using System;
using System.Collections.Generic;

namespace MysteryDungeon.Core.AI
{
    public abstract class Behaviour
    {
        public LivingEntity Parent { get; set; }
        public LivingEntity Target { get; set; }

        public Stack<Action> ActiveStateStack { get; set; }

        public Behaviour(LivingEntity parent)
        {
            Parent = parent;
            ActiveStateStack = new Stack<Action>();
        }

        protected void SetNewState(Action newState, bool pop = false)
        {
            if (pop)
                ActiveStateStack.Pop();

            ActiveStateStack.Push(newState);
            ActiveStateStack.Peek()?.Invoke();
        }

        public virtual void Act()
        {
            ActiveStateStack.Peek()?.Invoke();
        }
    }
}
