using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MysteryDungeon.Core.GUI
{
    class DialogueBox : Widget
    {
        public Texture2D DialogueBoxTexture;

        public DialogueBox(Texture2D dialogueBoxTexture, Rectangle boundingRectangle) : base()
        {
            DialogueBoxTexture = dialogueBoxTexture;
            BoundingRectangle = boundingRectangle;
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(
                DialogueBoxTexture,
                BoundingRectangle,
                Color.White);
        }
    }
}
