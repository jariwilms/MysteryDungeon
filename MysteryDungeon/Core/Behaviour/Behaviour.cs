using MysteryDungeon.Core.Actors;
using System;
using System.Collections.Generic;

namespace MysteryDungeon.Core.AI
{
    public class Behaviour
    {
        public LivingEntity Parent { get; set; }
        public LivingEntity Target { get; set; }

        public Stack<Action> ActiveStateStack { get; set; }

        public Behaviour(LivingEntity parent)
        {
            Parent = parent;
            ActiveStateStack = new Stack<Action>();
        }

        public void Act()
        {
            ActiveStateStack.Peek()?.Invoke();
        }
    }
}
