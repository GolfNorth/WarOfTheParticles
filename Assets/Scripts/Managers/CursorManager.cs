using System;
using UnityEngine;

namespace WarOfTheParticles
{
    public sealed class CursorManager : IDisposable, ITickable
    {
        private readonly UpdateManager _updateManager;
        private readonly RoundManager _roundManager;
        private readonly Camera _camera;
        private Vector3 _position;

        public Vector3 Position => _position;

        public CursorManager()
        {
            _camera = SceneContext.Instance.Camera;
            _updateManager = SceneContext.Instance.UpdateManager;
            _updateManager.Add(this);
            _roundManager = SceneContext.Instance.RoundManager;
            _roundManager.RoundCountdown += OnRoundCountdown;
            _roundManager.RoundGameOver += OnRoundGameOver;
        }

        private void OnRoundGameOver()
        {
            Cursor.visible = false;
        }

        private void OnRoundCountdown()
        {
            Cursor.visible = true;
        }

        public void Dispose()
        {
            _updateManager.Remove(this);
        }

        public void Tick()
        {
            _position = _camera.ScreenToWorldPoint(Input.mousePosition);
            _position.z = 0;
        }

        public void Destroy()
        {
            _roundManager.GameOver();;
        }
    }
}