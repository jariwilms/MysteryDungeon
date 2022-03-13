using MysteryDungeon.Core.Characters;
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
            spritebatch.Draw(
                SourceTexture,
                HealthBarDamageDestination,
                HealthBarDamageSource,
                Color.White);

            spritebatch.Draw(
                SourceTexture,
                HealthBarHealthDestination,
                HealthBarHealthSource,
                Color.White);
        }
    }
}
