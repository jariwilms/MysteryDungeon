﻿
using Microsoft.Xna.Framework;
using MysteryDungeon.Core.Characters;
using MysteryDungeon.Core.Input;

namespace MysteryDungeon.Core
{
    internal class Camera
    {
        public Matrix TransformMatrix { get; private set; }

        private Matrix _position;
        private Matrix _offset;
        private Matrix _zoom;

        public float ZoomValue { get; private set; }
        private float _minZoom;
        private float _maxZoom;
        private float _zoomStep;

        private int _windowWidth;
        private int _windowHeight;

        private Actor _target;

        public Camera(int windowWidth, int windowHeight)
        {
            TransformMatrix = new Matrix();

            _position = new Matrix();
            _offset = new Matrix();
            _zoom = new Matrix();

            ZoomValue = 3.0f;
            _minZoom = 0.2f;
            _maxZoom = 4.0f;
            _zoomStep = 0.2f;

            _windowWidth = windowWidth;
            _windowHeight = windowHeight;
        }

        public void Follow(Actor target)
        {
            _target = target;
        }

        public void ZoomIn() //If current zoom + zoomstep greater than maxzoom => zoom = maxzoom
        {
            ZoomValue = ZoomValue + _zoomStep > _maxZoom ? _maxZoom : ZoomValue + +_zoomStep;
        }

        public void ZoomOut() //Vice versa
        {
            ZoomValue = ZoomValue - _zoomStep < _minZoom ? _minZoom : ZoomValue - _zoomStep;
        }

        public void SetZoom(float zoom)
        {
            _zoom = Matrix.CreateScale(zoom);
        }

        public void Update()
        {
            if (InputEventHandler.Instance.MouseScrollUp())
                ZoomIn();

            if (InputEventHandler.Instance.MouseScrollDown())
                ZoomOut();

            _position = Matrix.CreateTranslation(
                (int)(-_target.Transform.Position.X - _target.Sprite.BoundingRectangle.Width / 2),
                (int)(-_target.Transform.Position.Y - _target.Sprite.BoundingRectangle.Height / 2),
                0.0f);

            _offset = Matrix.CreateTranslation(
                _windowWidth / 2,
                _windowHeight / 2,
                0.0f);

            _zoom = Matrix.CreateScale(ZoomValue);

            TransformMatrix = _position * _zoom * _offset;
        }
    }
}
