using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace WarOfTheParticles
{
    public sealed class ParticlesManager
    {
        private readonly ObjectPool<ParticleController> _particlesPool;
        private readonly RoundManager _roundManager;
        private readonly BoundsManager _boundsManager;
        private readonly RoundSettings _roundSettings;
        private readonly Array _bounds;
        private readonly System.Random _intRandom;

        public delegate void ParticleDestroyedHandler();

        public event ParticleDestroyedHandler ParticleDestroyed;

        public ParticlesManager()
        {
            _bounds = Enum.GetValues(typeof(Bound));
            _intRandom = new System.Random();
            _particlesPool = new ObjectPool<ParticleController>();
            _roundSettings = SceneContext.Instance.RoundSettings;
            _roundManager = SceneContext.Instance.RoundManager;
            _roundManager.RoundStarted += OnRoundStarted;
            _roundManager.RoundEnded += OnRoundEndedOrGameOver;
            _roundManager.RoundGameOver += OnRoundEndedOrGameOver;
            _boundsManager = SceneContext.Instance.BoundsManager;
        }

        public void Dispose()
        {
            _roundManager.RoundStarted -= OnRoundStarted;
            _roundManager.RoundEnded -= OnRoundEndedOrGameOver;
            _roundManager.RoundGameOver -= OnRoundEndedOrGameOver;
        }
        
        private void OnRoundStarted()
        {
            var amount = _roundSettings.StartAmount + _roundSettings.PerRoundAmount * (_roundManager.Round - 1);
            
            Spawn(amount);
        }

        private void OnRoundEndedOrGameOver()
        {
            DestroyAll();
        }

        private void Spawn(int amount)
        {
            for (var i = 0; i < amount; i++)
            {
                var position = new Vector3();
                var bound = (Bound) _bounds.GetValue(_intRandom.Next(_bounds.Length));

                switch (bound)
                {
                    case Bound.Top:
                        position.x = Random.Range(_boundsManager.LeftBound, _boundsManager.RightBound);
                        position.y = _boundsManager.TopBound;
                        break;
                    case Bound.Bottom:
                        position.x = Random.Range(_boundsManager.LeftBound, _boundsManager.RightBound);
                        position.y = _boundsManager.BottomBound;
                        break;
                    case Bound.Left:
                        position.x = _boundsManager.LeftBound;
                        position.y = Random.Range(_boundsManager.BottomBound, _boundsManager.TopBound);
                        break;
                    case Bound.Right:
                        position.x = _boundsManager.RightBound;
                        position.y = Random.Range(_boundsManager.BottomBound, _boundsManager.TopBound);
                        break;
                }
                
                var controller = _particlesPool.Acquire();
                controller.Speed = Random.value;
                controller.Position = position;
            }
        }

        public void Destroy(ParticleController controller)
        {
            _particlesPool.Release(controller);

            if (_roundManager.GameState == GameState.Started)
                ParticleDestroyed?.Invoke();
        }

        private void DestroyAll()
        {
            foreach (var controller in _particlesPool.All)
                if (controller.IsEnabled)
                    _particlesPool.Release(controller);
        }
    }
}