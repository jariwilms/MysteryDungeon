
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
        private float _minZoom;
        private float _maxZoom;
        private float _zoomStep;

        private Sprite _target;

        public Camera()
        {
            TransformMatrix = new Matrix();
            _position = new Matrix();
            _offset = new Matrix();
            _zoom = new Matrix();
            _zoomValue = 4.0f;
            _minZoom = 0.1f;
            _maxZoom = 5.0f;
            _zoomStep = 0.1f;
    }

        public void Follow(Sprite target)
        {
            _target = target;
        }

        public void ZoomIn() //If current zoom + zoomstep greater than maxzoom => zoom = maxzoom
        {
            _zoomValue = _zoomValue + _zoomStep > _maxZoom ? _maxZoom : _zoomValue + + _zoomStep;
        }

        public void ZoomOut() //Vice versa
        {
            _zoomValue = _zoomValue - _zoomStep < _minZoom ? _minZoom : _zoomValue - _zoomStep;
        }

        public void SetZoom(float zoom)
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
