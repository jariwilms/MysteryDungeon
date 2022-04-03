using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryDungeon.Core.Contracts
{
    public interface IUpdatable
    {
        public abstract void Update(GameTime gameTime);
    }
}
