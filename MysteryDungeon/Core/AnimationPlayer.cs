using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MysteryDungeon.Core
{
    class AnimationPlayer
    {
        private Animation _animation;
        private Rectangle _destinationRectangle;
        
        public bool IsPlaying { get { return _isPlaying; } }
        private bool _isPlaying;

        public AnimationPlayer()
        {
            _isPlaying = false;
        }

        public void PlayAnimation(Animation animation)
        {
            if (animation == _animation)
                return;

            _animation = animation;
            _isPlaying = true;
        }

        public void Resume()
        {
            _isPlaying = true;
        }

        public void Pause()
        {
            _isPlaying = false;
        }

        public void togglePlay()
        {
            _isPlaying = !_isPlaying;
        }

        public void NextFrame()
        {
            _animation.NextFrame();
        }

        public void PreviousFrame()
        {
            _animation.PreviousFrame();
        }

        public void Update(GameTime gameTime)
        {
            if (!_isPlaying)
                return;

            _animation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            if (_animation == null)
                throw new NullReferenceException("No animation has been set.");

            _destinationRectangle = new Rectangle((int)position.X, (int)position.Y, _animation.TextureWidth, _animation.TextureHeight);
            spriteBatch.Draw(_animation.Texture, _destinationRectangle, _animation.SourceRectangle, Color.White);
        }
    }
}
