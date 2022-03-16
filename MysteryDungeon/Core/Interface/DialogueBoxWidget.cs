﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MysteryDungeon.Core.Data;

namespace MysteryDungeon.Core.Interface
{
    class DialogueBoxWidget : Widget
    {
        public enum DialogueBoxColor
        {
            Red,
            Green,
            Blue
        }

        public Rectangle DialogueBoxForegroundSourceRectangle;
        public Rectangle DialogueBoxBackgroundSourceRectangle;

        public DialogueBoxWidget(Widget parent = null, DialogueBoxColor dialogueBoxColor = DialogueBoxColor.Blue) : base(parent)
        {
            SourceTexture = GuiTextures.DialogueTexture;

            DialogueBoxForegroundSourceRectangle = GuiTextures.DialogueBoxForegroundSourceBlue;
            DialogueBoxBackgroundSourceRectangle = GuiTextures.DialogueBoxBackgroundSource;

            DestinationRectangle = new Rectangle(
                WindowSettings.WindowWidth / 2 - DialogueBoxForegroundSourceRectangle.Width / 2,
                WindowSettings.WindowHeight - DialogueBoxForegroundSourceRectangle.Height - 10,
                DialogueBoxForegroundSourceRectangle.Width,
                DialogueBoxForegroundSourceRectangle.Height);
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(
                SourceTexture,
                DestinationRectangle,
                DialogueBoxBackgroundSourceRectangle,
                Color.White);

            spritebatch.Draw(
                SourceTexture,
                DestinationRectangle,
                DialogueBoxForegroundSourceRectangle,
                Color.White);
        }
    }
}
