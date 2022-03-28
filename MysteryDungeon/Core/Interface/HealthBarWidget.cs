using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MysteryDungeon.Core.Data;
using MysteryDungeon.Core.Entities;
using MysteryDungeon.Core.Extensions;

namespace MysteryDungeon.Core.Interface
{
    internal class HealthBarWidget : Widget
    {
        protected Player Player;

        protected Rectangle HealthBarHealthSource;
        protected Rectangle HealthBarDamageSource;
        protected Rectangle HealthBarHealthDestination;
        protected Rectangle HealthBarDamageDestination;

        public HealthBarWidget(Player player) : base() //maak class tussen player en actor => livingentity ofzoiets
        {
            SetPlayer(player);

            SourceTexture = GuiTextures.FontTexture;

            HealthBarHealthSource = GuiTextures.HealthBarHealthSource;
            HealthBarDamageSource = GuiTextures.HealthBarDamageSource;

            HealthBarHealthDestination = new Rectangle(WindowSettings.WindowWidth / 2, 10, HealthBarHealthSource.Width, HealthBarHealthSource.Height);
            HealthBarDamageDestination = new Rectangle(WindowSettings.WindowWidth / 2, 10, HealthBarDamageSource.Width, HealthBarDamageSource.Height);

            LocalScale = 2.0f;
        }

        public void SetPlayer(Player player)
        {
            Player = player;
        }

        public override void Update(GameTime gameTime)
        {
            if (Player.CurrentHealth > 0)
                HealthBarHealthDestination.Width = (int)(HealthBarHealthSource.Width * ((float)Player.CurrentHealth / Player.MaxHealth));
            else
                HealthBarHealthDestination.Width = 0;
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            Rectangle scaledHealthBarHealthDestination = HealthBarHealthDestination.Scale(LocalScale);
            Rectangle scaledHealthBarDamageDestination = HealthBarDamageDestination.Scale(LocalScale);

            spritebatch.Draw(
                SourceTexture,
                scaledHealthBarDamageDestination,
                HealthBarDamageSource,
                Color.White);

            spritebatch.Draw(
                SourceTexture,
                scaledHealthBarHealthDestination,
                HealthBarHealthSource,
                Color.White);
        }
    }
}
