using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace MysteryDungeon.Core
{
    class Camera
    {
        public Matrix TransformMatrix { get; private set; }
        private Matrix _position;
        private Matrix _offset;
        private Matrix _zoom;

        private Sprite _target;

        public void Follow(Sprite target)
        {
            _target = target;
        }

        public void Update()
        {
            _position = Matrix.CreateTranslation(
                -_target.Transform.Position.X - _target.BoundingRectangle.Width / 2,
                -_target.Transform.Position.Y - _target.BoundingRectangle.Height / 2,
                0.0f);

            _offset = Matrix.CreateTranslation(
                MysteryDungeon._windowWidth / 2,
                MysteryDungeon._windowHeight / 2,
                0.0f);

            _zoom = Matrix.CreateScale(0.1f);

            TransformMatrix = _position * _zoom * _offset;
        }
    }
}
