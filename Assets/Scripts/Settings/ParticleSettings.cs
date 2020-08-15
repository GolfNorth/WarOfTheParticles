using UnityEngine;

namespace WarOfTheParticles
{
    [CreateAssetMenu(fileName = "ParticleSettings", menuName = "War of the Particles/Particle Settings", order = 1)]
    public sealed class ParticleSettings : ScriptableObject
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private float minRadius;
        [SerializeField] private float maxRadius;
        [SerializeField] private float minSpeed;
        [SerializeField] private float maxSpeed;

        public GameObject Prefab => prefab;

        public float MinRadius => minRadius;

        public float MaxRadius => maxRadius;

        public float MinSpeed => minSpeed;

        public float MaxSpeed => maxSpeed;
    }
}