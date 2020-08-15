using UnityEngine;

namespace WarOfTheParticles
{
    [CreateAssetMenu(fileName = "RoundSettings", menuName = "War of the Particles/Round Settings", order = 0)]
    public sealed class RoundSettings : ScriptableObject
    {
        [SerializeField] private int startAmount;
        [SerializeField] private int perRoundAmount;
        [SerializeField] private float countdown;

        public int StartAmount => startAmount;

        public int PerRoundAmount => perRoundAmount;

        public float Countdown => countdown;
    }
}