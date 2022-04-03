using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryDungeon.Core.Contracts
{
    public interface IDrawable
    {
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
