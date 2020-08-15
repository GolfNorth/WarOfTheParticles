using System;
using UnityEngine;

namespace WarOfTheParticles
{
    public sealed class BoundsManager : IDisposable
    {
        private Camera _camera;
        private readonly ParticleSettings _particleSettings;
        private readonly RoundManager _roundManager;
        private float _topBound;
        private float _bottomBound;
        private float _leftBound;
        private float _rightBound;

        public BoundsManager()
        {
            _camera = SceneContext.Instance.Camera;
            _particleSettings = SceneContext.Instance.ParticleSettings;
            _roundManager = SceneContext.Instance.RoundManager;
            _roundManager.RoundCountdown += RecalculateBounds;

            RecalculateBounds();
        }

        public void Dispose()
        {
            _roundManager.RoundCountdown -= RecalculateBounds;
        }

        public float TopBound => _topBound;

        public float BottomBound => _bottomBound;

        public float LeftBound => _leftBound;

        public float RightBound => _rightBound;

        private void RecalculateBounds()
        {
            var z = _camera.gameObject.transform.position.z;
            var topRight = _camera.ViewportToWorldPoint(new Vector3(1, 1, -z));
            var bottomLeft = _camera.ViewportToWorldPoint(new Vector3(0, 0, -z));

            _topBound = topRight.y + _particleSettings.MaxRadius;
            _bottomBound = bottomLeft.y - _particleSettings.MaxRadius;
            _leftBound = bottomLeft.x - _particleSettings.MaxRadius;
            _rightBound = topRight.x + _particleSettings.MaxRadius;
        }
    }
}