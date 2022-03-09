using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MysteryDungeon.Core
{
    class AnimationPlayer
    {
        private Animation _animation;
        private Dictionary<string, Animation> _animationDictionary; //verander naar enum met animationstates?

        private bool _isPlaying;

        public AnimationPlayer()
        {
            _animationDictionary = new Dictionary<string, Animation>();
            _isPlaying = true;
        }

        public void AddAnimation(string identifier, Animation animation)
        {
            if (_animationDictionary.ContainsKey(identifier))
                throw new Exception(String.Format("An animation with identifier {0} already exists", identifier));

            _animationDictionary.Add(identifier, animation);
        }

        public void PlayAnimation(string identifier)
        {
            bool found = _animationDictionary.TryGetValue(identifier, out Animation animation);

            if (!found)
                throw new Exception(String.Format("An animation with identifier {0} does not exist", identifier));

            _animation = animation;
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
                new Rectangle(destinationRectangle.X + leftDiff, destinationRectangle.Y + bottomDiff - 4, _animation.SourceRectangle.Width, _animation.SourceRectangle.Height),
                _animation.SourceRectangle,
                Color.White,
                0.0f,
                Vector2.Zero,
                effects: _animation.SpriteEffects,
                0.0f);
        }
    }
}
