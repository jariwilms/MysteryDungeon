using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MysteryDungeon.Core.Data
{
    public static class GuiTextures
    {
        private static readonly ContentManager _content = new ContentManager(new GameServiceContainer(), "Content");

        public static readonly Texture2D DialogueTexture = _content.Load<Texture2D>("gui/dialogue");
        public static readonly Texture2D FontTexture = _content.Load<Texture2D>("gui/font");

        public static readonly Rectangle DialogueBoxBackgroundSource = new Rectangle(4, 301, 224, 40);
        public static readonly Rectangle DialogueBoxForegroundSourceRed = new Rectangle(4, 255, 224, 40);
        public static readonly Rectangle DialogueBoxForegroundSourceGreen = new Rectangle(4, 255, 224, 40);
        public static readonly Rectangle DialogueBoxForegroundSourceBlue = new Rectangle(4, 255, 224, 40);

        public static readonly Rectangle HealthBarHealthSource = new Rectangle(152, 1, 30, 6);
        public static readonly Rectangle HealthBarDamageSource = new Rectangle(152, 10, 30, 6);
    }
}
