﻿using Microsoft.Xna.Framework;
using MysteryDungeon.Core.Behaviour;
using MysteryDungeon.Core.Entities;
using MysteryDungeon.Core.Extensions;
using System;

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
        public EnemyBehaviour EnemyBehaviour { get; set; }

        public IntelligenceComponent(GameObject parent) : base(parent)
        {

        }

        public override void Update(GameTime gameTime)
        {
            EnemyBehaviour.Update(gameTime);
        }
    }
}
