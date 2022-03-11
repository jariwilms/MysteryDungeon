using MysteryDungeon.Core.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MysteryDungeon.Core.GUI
{
    class HealthBarWidget
    {
        public Actor Actor { get; private set; }

        public HealthBarWidget(Actor actor)
        {

        }

        public void SetActor(Actor actor)
        {
            Actor = actor;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
