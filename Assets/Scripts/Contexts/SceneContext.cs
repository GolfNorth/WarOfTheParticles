using UnityEngine;

namespace WarOfTheParticles
{
    public sealed class SceneContext : Singleton<SceneContext>
    {
        [SerializeField] private Camera camera;
        [SerializeField] private RoundSettings roundSettings;
        [SerializeField] private ParticleSettings particleSettings;
        [SerializeField] private RectangleSettings rectangleSettings;
        private UpdateManager _updateManager;
        private RoundManager _roundManager;
        private BoundsManager _boundsManager;
        private CursorManager _cursorManager;
        private ParticlesManager _particlesManager;
        private ScoreManager _scoreManager;

        public Camera Camera => camera;

        public RoundSettings RoundSettings => roundSettings;

        public ParticleSettings ParticleSettings => particleSettings;

        public RectangleSettings RectangleSettings => rectangleSettings;

        public UpdateManager UpdateManager => _updateManager;

        public RoundManager RoundManager => _roundManager;

        public BoundsManager BoundsManager => _boundsManager;

        public CursorManager CursorManager => _cursorManager;

        public ParticlesManager ParticlesManager => _particlesManager;

        public ScoreManager ScoreManager => _scoreManager;

        private void Awake()
        {
            _updateManager = new UpdateManager();
            _roundManager = new RoundManager();
            _boundsManager = new BoundsManager();
            _cursorManager = new CursorManager();
            _particlesManager = new ParticlesManager();
            _scoreManager = new ScoreManager();
        }
    }
}