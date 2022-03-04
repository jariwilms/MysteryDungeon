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
        private float _zoomValue;

        private Sprite _target;

        public Camera()
        {
            TransformMatrix = new Matrix();
            _position = new Matrix();
            _offset = new Matrix();
            _zoom = new Matrix();
            _zoomValue = 0.1f;
        }

        public void Follow(Sprite target)
        {
            _target = target;
        }

        public void zoomIn()
        {
            _zoomValue = _zoomValue + 0.1f > 1.5f ? 1.5f : _zoomValue + 0.1f;
        }

        public void zoomOut()
        {
            _zoomValue = _zoomValue - 0.1f < 0.1f ? 0.1f : _zoomValue - 0.1f;
        }

        public void setZoom(float zoom)
        {
            _zoom = Matrix.CreateScale(zoom);
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

            _zoom = Matrix.CreateScale(_zoomValue);

            TransformMatrix = _position * _zoom * _offset;
        }
    }
}
