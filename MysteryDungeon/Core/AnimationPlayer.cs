using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MysteryDungeon.Core
{
    class AnimationPlayer
    {
        private Animation _animation;

        public bool IsPlaying { get { return _isPlaying; } private set { _isPlaying = value; } }
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

        public void TogglePlay()
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

        public void Draw(SpriteBatch spriteBatch, Rectangle destinationRectangle)
        {
            if (_animation == null)
                throw new NullReferenceException("No animation has been set.");

            int leftDiff = (24 - _animation.SourceRectangle.Width) / 2;
            int bottomDiff = 24 - _animation.SourceRectangle.Height;

            spriteBatch.Draw(
                _animation.SourceTexture, 
                new Rectangle(destinationRectangle.X + leftDiff, destinationRectangle.Y + bottomDiff, _animation.SourceRectangle.Width, _animation.SourceRectangle.Height), 
                _animation.SourceRectangle, 
                Color.White);
        }
    }
}
