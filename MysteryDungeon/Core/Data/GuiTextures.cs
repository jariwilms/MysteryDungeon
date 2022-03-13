﻿
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MysteryDungeon.Core.Data
{
    public static class GuiTextures
    {
        public static Texture2D DialogueTexture;
        public static Texture2D FontTexture;

        public static Rectangle DialogueBoxBackgroundSource;
        public static Rectangle DialogueBoxForegroundSourceRed;
        public static Rectangle DialogueBoxForegroundSourceGreen;
        public static Rectangle DialogueBoxForegroundSourceBlue;

        public static Rectangle HealthBarHealthSource;
        public static Rectangle HealthBarDamageSource;

        public static void Load(ContentManager content)
        {
            DialogueTexture = content.Load<Texture2D>("gui/dialogue");
            FontTexture = content.Load<Texture2D>("gui/font");

            DialogueBoxBackgroundSource = new Rectangle(4, 301, 224, 40);
            DialogueBoxForegroundSourceRed = new Rectangle(4, 255, 224, 40);
            DialogueBoxForegroundSourceGreen = new Rectangle(4, 255, 224, 40);
            DialogueBoxForegroundSourceBlue = new Rectangle(4, 255, 224, 40);

            HealthBarHealthSource = new Rectangle(152, 1, 30, 6);
            HealthBarDamageSource = new Rectangle(152, 10, 30, 6);
        }
    }
}
