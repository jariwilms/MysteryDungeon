using MysteryDungeon.Core.Entities;
using System;
using System.Collections.Generic;

namespace MysteryDungeon.Core.Behaviour
{
    public class Behaviour
    {
        public LivingEntity Parent { get; set; }
        public LivingEntity Target { get; set; }

        public Stack<Action> ActiveStateStack { get; set; }

        public Behaviour(LivingEntity parent, LivingEntity target)
        {
            Parent = parent;
            Target = target;

            ActiveStateStack = new Stack<Action>();
        }
    }
}
