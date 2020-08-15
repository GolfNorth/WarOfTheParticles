using System;

namespace WarOfTheParticles
{
    public sealed class ScoreManager : IDisposable
    {
        private int _score;
        private readonly RoundManager _roundManager;
        private readonly ParticlesManager _particlesManager;

        public int Score => _score;

        public ScoreManager()
        {
            _roundManager = SceneContext.Instance.RoundManager;
            _roundManager.RoundCountdown += OnRoundCountdown;
            _particlesManager = SceneContext.Instance.ParticlesManager;
            _particlesManager.ParticleDestroyed += OnParticleDestroyed;
        }

        private void OnParticleDestroyed()
        {
            if (_roundManager.GameState != GameState.Started) return;

            _score++;
        }

        private void OnRoundCountdown()
        {
            if (_roundManager.Round == 1) _score = 0;
        }

        public void Dispose()
        {
            _roundManager.RoundCountdown -= OnRoundCountdown;
            _particlesManager.ParticleDestroyed -= OnParticleDestroyed;
        }
    }
}