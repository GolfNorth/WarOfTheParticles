using UnityEngine;

namespace WarOfTheParticles
{
    [CreateAssetMenu(fileName = "RectangleSettings", menuName = "War of the Particles/Rectangle Settings", order = 2)]
    public sealed class RectangleSettings : ScriptableObject
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private float width;
        [SerializeField] private float height;

        public GameObject Prefab => prefab;

        public float Width => width;

        public float Height => height;
    }
}