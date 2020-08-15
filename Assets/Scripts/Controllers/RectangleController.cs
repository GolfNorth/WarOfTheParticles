using UnityEngine;

namespace WarOfTheParticles
{
    public sealed class RectangleController : IController<RectangleController>
    {
        private readonly RoundManager _roundManager;
        private readonly CursorManager _cursorManager;
        private readonly ParticlesManager _particlesManager;
        
        public RectangleController(RectangleView view)
        {
            _cursorManager = SceneContext.Instance.CursorManager;
            _particlesManager = SceneContext.Instance.ParticlesManager;
            _roundManager = SceneContext.Instance.RoundManager;
            
            var rectangleSettings = SceneContext.Instance.RectangleSettings;
            var particleSettings = SceneContext.Instance.ParticleSettings;
            var transform = view.transform;
            transform.localScale = new Vector3(
                rectangleSettings.Width, 
                rectangleSettings.Height,
                particleSettings.MaxRadius + 1
            );
        }

        public void Dispose()
        {
        }

        public void DestroyCursor()
        {
            if (_roundManager.GameState != GameState.Started) return;
            
            _cursorManager.Destroy();
        }
        
        public void DestroyParticle(GameObject gameObject)
        {
            if (_roundManager.GameState != GameState.Started) return;

            var particleView = gameObject.GetComponent<ParticleView>();
            
            if (particleView == null) return;
            
            _particlesManager.Destroy(particleView.Controller);
        }
    }
}