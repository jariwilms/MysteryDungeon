using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MysteryDungeon.Core.Characters;
using MysteryDungeon.Core.Data;
using MysteryDungeon.Core.Extensions;

namespace MysteryDungeon.Core.GUI
{
    class HealthBarWidget : Widget
    {
        protected Player _player;

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

            HealthBarHealthDestination = new Rectangle(_windowWidth / 2, 10, HealthBarHealthSource.Width, HealthBarHealthSource.Height);
            HealthBarDamageDestination = new Rectangle(_windowWidth / 2, 10, HealthBarDamageSource.Width, HealthBarDamageSource.Height);

            _scale = 10.0f;
        }

        public void SetPlayer(Player player)
        {
            _player = player;
        }

        public override void Update(GameTime gameTime)
        {
            if (_player.CurrentHealth > 0)
                HealthBarHealthDestination.Width = (int)(HealthBarHealthSource.Width * ((float)_player.CurrentHealth / _player.MaxHealth));
            else
                HealthBarHealthDestination.Width = 0;
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            Rectangle scaledHealthBarHealthDestination = HealthBarHealthDestination.Scale(_scale);
            Rectangle scaledHealthBarDamageDestination = HealthBarDamageDestination.Scale(_scale);

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
