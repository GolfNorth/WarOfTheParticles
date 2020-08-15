using UnityEngine;
using UnityEngine.UI;

namespace WarOfTheParticles
{
    public sealed class UIController : IController<UIController>, ITickable
    {
        private readonly Text _textComponent;
        private readonly Text _scoreComponent;
        private readonly UpdateManager _updateManager;
        private readonly RoundManager _roundManager;
        private readonly ScoreManager _scoreManager;
        
        public UIController(Text textComponent, Text scoreComponent)
        {
            _textComponent = textComponent;
            _scoreComponent = scoreComponent;
            _scoreComponent.gameObject.SetActive(false);
            _updateManager = SceneContext.Instance.UpdateManager;
            _updateManager.Add(this);
            _roundManager = SceneContext.Instance.RoundManager;
            _roundManager.RoundCountdown += OnRoundCountdown;
            _roundManager.RoundStarted += OnRoundStarted;
            _roundManager.RoundGameOver += OnRoundGameOver;
            _scoreManager = SceneContext.Instance.ScoreManager;
        }
        
        public void Dispose()
        {
            _updateManager.Remove(this);
            _roundManager.RoundCountdown -= OnRoundCountdown;
            _roundManager.RoundStarted -= OnRoundStarted;
            _roundManager.RoundGameOver -= OnRoundGameOver;
        }

        private void OnRoundGameOver()
        {
            _textComponent.text = $"Game Over!\nYour Score: {_scoreManager.Score}";
            _textComponent.gameObject.SetActive(true);
            _scoreComponent.gameObject.SetActive(false);
        }

        private void OnRoundStarted()
        {
            _textComponent.gameObject.SetActive(false);
            _scoreComponent.gameObject.SetActive(true);
        }

        private void OnRoundCountdown()
        {
            _textComponent.gameObject.SetActive(true);
            _scoreComponent.gameObject.SetActive(false);
        }

        public void Tick()
        {
            switch (_roundManager.GameState)
            {
                case GameState.Countdown:
                    _textComponent.text = $"{_roundManager.Countdown}\nRound {_roundManager.Round}";
                    break;
                case GameState.GameOver when Input.anyKey:
                    _roundManager.Start();
                    break;
                case GameState.Started:
                    _scoreComponent.text = $"Score: {_scoreManager.Score}";
                    break;
            }
        }
    }
}