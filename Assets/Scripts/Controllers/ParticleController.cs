using UnityEngine;

namespace WarOfTheParticles
{
    public sealed class ParticleController : IController<ParticleController>, IPoolable, ITickable
    {
        private ParticleModel _model;
        private ParticleView _view;
        private readonly ParticleSettings _particleSettings;
        private readonly ParticlesManager _particlesManager;
        private readonly UpdateManager _updateManager;
        private readonly CursorManager _cursorManager;

        public Vector3 Position
        {
            set => _model.Transform.position = value;
        }
        
        public float Speed
        {
            set => _model.Speed = value;
        }
        
        public ParticleController()
        {
            _particleSettings = SceneContext.Instance.ParticleSettings;
            _particlesManager = SceneContext.Instance.ParticlesManager;
            _cursorManager = SceneContext.Instance.CursorManager;
            _updateManager = SceneContext.Instance.UpdateManager;
            _updateManager.Add(this);
        }
        
        public void Dispose()
        {
            _updateManager.Remove(this);
        }

        public bool IsEnabled { get; set; }
        public void Initialize()
        {
            _model = new ParticleModel(_particleSettings)
            {
                Active = true
            };
            _view = _model.GameObject.GetComponent<ParticleView>();
            _view.Controller = this;
        }

        public void Enable()
        {
            if (_model.Active) return;
            
            _model.Active = true;
        }

        public void Disable()
        {
            if (!_model.Active) return;
            
            _model.Active = false;
        }

        public void DestroyCursor()
        {
            _cursorManager.Destroy();
        }

        public void Tick()
        {
            if (!_model.Active) return;
            
            var position = _model.Transform.position;
            var newPosition = Vector3.MoveTowards(position, _cursorManager.Position, _model.Speed);
            _model.Transform.position = newPosition;
        }
    }
}