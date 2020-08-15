using UnityEngine;

namespace WarOfTheParticles
{
    public sealed class ParticleModel
    {
        private readonly GameObject _gameObject;
        private readonly Transform _transform;
        private readonly CircleCollider2D _collider;
        private readonly MeshRenderer _meshRenderer;
        private readonly ParticleSystem _particleSystem;
        private readonly ParticleSettings _particleSettings;
        private readonly TrailRenderer _trailRenderer;
        private bool _active;
        private float _speed;
        private float _radius;

        public GameObject GameObject => _gameObject;

        public Transform Transform => _transform;
        
        public bool Active
        {
            get => _active;
            set
            {
                _active = value;
                _meshRenderer.enabled = value;
                _trailRenderer.enabled = value;
                if (!value) _particleSystem.Play();
            }
        }

        public float Speed
        {
            get => _speed;
            set
            {
                _radius = _particleSettings.MinRadius +
                          (_particleSettings.MaxRadius - _particleSettings.MinRadius) * (1 - value);
                _speed = _particleSettings.MinSpeed +
                               (_particleSettings.MaxSpeed - _particleSettings.MinSpeed) * value;
                _collider.radius = _radius;

                var scale = _radius * 2;
                _transform.localScale = new Vector3(scale, scale, scale);
                
                var widthCurve = new AnimationCurve();
                widthCurve.AddKey(0, scale);
                widthCurve.AddKey(1, 0);

                _trailRenderer.widthCurve = widthCurve;
            }
        }

        public float Radius
        {
            get => _radius;
            set => _radius = value;
        }

        public ParticleModel(ParticleSettings particleSettings)
        {
            _particleSettings = particleSettings;
            _gameObject = GameObject.Instantiate(particleSettings.Prefab);
            _transform = _gameObject.transform;
            _collider = _gameObject.GetComponent<CircleCollider2D>();
            _meshRenderer = _gameObject.GetComponent<MeshRenderer>();
            _particleSystem = _gameObject.GetComponent<ParticleSystem>();
            _trailRenderer = _gameObject.GetComponent<TrailRenderer>();
        }
    }
}