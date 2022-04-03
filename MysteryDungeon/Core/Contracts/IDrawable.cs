using Microsoft.Xna.Framework.Graphics;

namespace MysteryDungeon.Core.Contracts
{
    public interface IDrawable
    {
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
