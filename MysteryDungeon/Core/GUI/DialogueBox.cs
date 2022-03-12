using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MysteryDungeon.Core.Data;

namespace MysteryDungeon.Core.GUI
{
    class DialogueBox : Widget
    {
        public enum DialogueBoxColor
        {
            Red, 
            Green, 
            Blue
        }

        public Rectangle DialogueBoxForegroundSourceRectangle;
        public Rectangle DialogueBoxBackgroundSourceRectangle;

        public DialogueBox(DialogueBoxColor dialogueBoxColor, int screenWidth, int screenHeight) : base()
        {
            SourceTexture = GuiTextures.DialogueTexture;

            DialogueBoxForegroundSourceRectangle = GuiTextures.DialogueBoxForegroundSourceBlue;
            DialogueBoxBackgroundSourceRectangle = GuiTextures.DialogueBoxBackgroundSource;

            DestinationRectangle = new Rectangle(
                screenWidth / 2 - DialogueBoxForegroundSourceRectangle.Width / 2, 
                screenHeight - DialogueBoxForegroundSourceRectangle.Height - 10, 
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
