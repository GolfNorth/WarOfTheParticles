namespace WarOfTheParticles
{
    public class ParticleView : BaseView<ParticleController>
    {
        private void OnMouseOver()
        {
            controller.DestroyCursor();
        }
    }
}