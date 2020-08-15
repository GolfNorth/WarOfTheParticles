namespace WarOfTheParticles
{
    public interface IPoolable
    {
        bool IsEnabled { get; set; }
        void Initialize();
        void Enable();
        void Disable();
    }
}