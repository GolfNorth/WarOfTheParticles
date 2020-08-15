using System.Collections;
using UnityEngine;

namespace WarOfTheParticles
{
    public sealed class RoundManager
    {
        private int _round;
        private float _countdown;
        private GameState _gameState;
        private readonly RoundSettings _roundSettings;
        private readonly UpdateManager _updateManager;
        
        public delegate void RoundHandler();

        public event RoundHandler RoundCountdown;
        public event RoundHandler RoundStarted;
        public event RoundHandler RoundEnded;
        public event RoundHandler RoundGameOver;
        
        public RoundManager()
        {
            _roundSettings = SceneContext.Instance.RoundSettings;
            _updateManager = SceneContext.Instance.UpdateManager;
            
            Start();
        }

        public int Round => _round;
        
        public int Countdown => Mathf.CeilToInt(_countdown);

        public GameState GameState => _gameState;

        public void Start()
        {
            if (_gameState == GameState.Countdown || _gameState == GameState.Started) return;
            
            _countdown = _roundSettings.Countdown;

            _updateManager.StartCoroutine(StartCountdown());
        }

        public void End()
        {
            if (_gameState == GameState.Ended) return;
            
            _gameState = GameState.Ended;
            
            RoundEnded?.Invoke();
            
            Start();
        }

        public void GameOver()
        {
            if (_gameState == GameState.GameOver) return;
            
            _round = 0;
            _gameState = GameState.GameOver;
            
            RoundGameOver?.Invoke();
        }

        private IEnumerator StartCountdown()
        {
            _round++;
            _gameState = GameState.Countdown;
            
            RoundCountdown?.Invoke();

            while (_countdown > 0)
            {
                _countdown -= Time.deltaTime;

                yield return null;
            }

            _gameState = GameState.Started;
            
            RoundStarted?.Invoke();
        }
    }
}