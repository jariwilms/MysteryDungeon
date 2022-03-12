using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MysteryDungeon.Core.Data
{
    public static class GuiTextures
    {
        public static Texture2D DialogueTexture;

        public static Rectangle DialogueBoxBackgroundSource;
        public static Rectangle DialogueBoxForegroundSourceRed;
        public static Rectangle DialogueBoxForegroundSourceGreen;
        public static Rectangle DialogueBoxForegroundSourceBlue;

        public static void Load(ContentManager content)
        {
            DialogueTexture = content.Load<Texture2D>("gui/dialogue");

            DialogueBoxBackgroundSource = new Rectangle(4, 301, 224, 40);
            //DialogueBoxForegroundSourceRed = new Rectangle(4, 255, 224, 40);
            //DialogueBoxForegroundSourceGreen = new Rectangle(4, 255, 224, 40);
            DialogueBoxForegroundSourceBlue = new Rectangle(4, 255, 224, 40);
        }
    }
}
